using Accounting.Models;
using Accounting.Repositories.Interfaces;
using CSV;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Accounting.Repositories.Implementations
{
    internal class AccountingInfoRepository : IAccountingInfoRepository
    {
        private readonly string fileServerPath = ConfigurationManager.AppSettings["FileServerPath"];

        public List<AccountingInfo> GetAccountingInfos(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments)
        {
            if (searchDate.StartTime > searchDate.EndTime)
                return null;
            int diffDays = (searchDate.EndTime - searchDate.StartTime).Days + 1;
            HashSet<string> directorySet = new HashSet<string>(Directory.GetDirectories(fileServerPath));
            List<AccountingInfo> accountingInfos = new List<AccountingInfo>();
            for (int i = 0; i < diffDays; i++)
            {
                string directoryName = Path.Combine(fileServerPath, searchDate.StartTime.AddDays(i).ToString("yyyy-MM-dd"));
                if (directorySet.Contains(directoryName))
                    accountingInfos.AddRange(CSVHelper.Read<AccountingInfo>(Path.Combine(directoryName, "Data.csv")));
            }

            if (purposes.Count > 0)
                accountingInfos = accountingInfos.Where(x => purposes.Contains(x.Purpose)).ToList();
            if (companions.Count > 0)
                accountingInfos = accountingInfos.Where(x => companions.Contains(x.Companion)).ToList();
            if (payments.Count > 0)
                accountingInfos = accountingInfos.Where(x => payments.Contains(x.Payment)).ToList();

            return accountingInfos;
        }

        public List<GroupByAmount> GetGroupByAmounts(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys)
        {
            var accountingInfos = GetAccountingInfos(searchDate, purposes, companions, payments);
            var groupByAmount = accountingInfos.GroupBy(x => new
            {
                Type = ExpenseData.OrderBys["類型"] ? x.Type : null,
                Purpose = ExpenseData.OrderBys["目的"] ? x.Purpose : null,
                Companion = ExpenseData.OrderBys["對象"] ? x.Companion : null,
                Payment = ExpenseData.OrderBys["付款方式"] ? x.Payment : null,
            }).Select(g => new GroupByAmount
            {
                GroupKey = string.Join(", ", new[] { g.Key.Type, g.Key.Purpose, g.Key.Companion, g.Key.Payment }
                                .Where(value => !string.IsNullOrEmpty(value))), // 過濾掉 null 或空字串
                Amount = g.Sum(x => Convert.ToInt32(x.Amount))
            }).OrderBy(x => x.GroupKey).ToList();
            return groupByAmount;
        }
    }
}
