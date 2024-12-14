using Accounting.Models;
using Accounting.Repositories.Interfaces;
using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Accounting.Repositories.Implementations
{
    internal class AccountingInfoRepository : IAccountingInfoRepository
    {
        private readonly string fileServerPath = ConfigurationManager.AppSettings["FileServerPath"];

        public List<AccountingInfo> GetAccountingInfos(SearchDate searchDate)
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

            var propertys = typeof(AccountingInfo)
            .GetProperties()
            .Where(prop => prop.GetCustomAttribute<DisplayNameAttribute>() != null)
            .ToDictionary(
                prop => prop.GetCustomAttribute<DisplayNameAttribute>().DisplayName,
                prop => prop
            );

            foreach (var category in ExpenseData.CategoryLists)
            {
                if (category.Value.Count > 0 && propertys.TryGetValue(category.Key, out var property))
                {
                    // 動態篩選資料
                    accountingInfos = accountingInfos
                        .Where(info => category.Value.Contains(property.GetValue(info)?.ToString()))
                        .ToList();
                }
            }
            return accountingInfos;
        }

        public List<GroupByAmount> GetGroupByAmounts(SearchDate searchDate)
        {
            var accountingInfos = GetAccountingInfos(searchDate);
            var groupByAmount = accountingInfos.GroupBy(x => new
            {
                Time = ExpenseData.OrderBys["時間"] ? x.Time : null,
                Type = ExpenseData.OrderBys["類型"] ? x.Type : null,
                Purpose = ExpenseData.OrderBys["目的"] ? x.Purpose : null,
                Companion = ExpenseData.OrderBys["對象"] ? x.Companion : null,
                Payment = ExpenseData.OrderBys["付款方式"] ? x.Payment : null,
            }).Select(g => new GroupByAmount
            {
                GroupKey = string.Join(", ", new[] { g.Key.Time, g.Key.Type, g.Key.Purpose, g.Key.Companion, g.Key.Payment }
                                .Where(value => !string.IsNullOrEmpty(value))), // 過濾掉 null 或空字串
                Amount = g.Sum(x => Convert.ToInt32(x.Amount))
            }).OrderBy(x => x.GroupKey).ToList();
            return groupByAmount;
        }
    }
}
