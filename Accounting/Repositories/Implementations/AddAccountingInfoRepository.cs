using Accounting.Models;
using Accounting.Repositories.Interfaces;
using CSV;
using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Accounting.Repositories.Implementations
{
    internal class AddAccountingInfoRepository : IAddAccountingInfoRepository
    {
        //public List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate)
        //{
        //    String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
        //    searchDate.StartTime = searchDate.StartTime.Date;
        //    searchDate.EndTime = searchDate.EndTime.Date;
        //    if (searchDate.StartTime > searchDate.EndTime)
        //        return null;
        //    int diffDays = (searchDate.EndTime - searchDate.StartTime).Days + 1;
        //    List<String> directories = Directory.GetDirectories(fileServerPath).ToList();
        //    List<AddAccountingInfo> addAccountingInfos = new List<AddAccountingInfo>();
        //    for (int i = 0; i < diffDays; i++)
        //    {
        //        String directoryName = $"{fileServerPath}{searchDate.StartTime.AddDays(i).ToString("yyyy-MM-dd")}";
        //        foreach (var directory in directories)
        //        {
        //            if (directory == directoryName)
        //            {
        //                addAccountingInfos.AddRange(CSVHelper.Read<AddAccountingInfo>($"{directoryName}\\Data.csv"));
        //                break;
        //            }
        //        }
        //    }
        //    return addAccountingInfos;
        //}

        public List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate)
        {
            String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
            searchDate.StartTime = searchDate.StartTime.Date;
            searchDate.EndTime = searchDate.EndTime.Date;
            if (searchDate.StartTime > searchDate.EndTime)
                return null;
            int diffDays = (searchDate.EndTime - searchDate.StartTime).Days + 1;
            List<String> directories = Directory.GetDirectories(fileServerPath).ToList();
            List<AddAccountingInfo> addAccountingInfos = new List<AddAccountingInfo>();
            for (int i = 0; i < diffDays; i++)
            {
                String directoryName = $"{fileServerPath}{searchDate.StartTime.AddDays(i).ToString("yyyy-MM-dd")}";
                foreach (var directory in directories)
                {
                    if (directory == directoryName)
                    {
                        addAccountingInfos.AddRange(CSVHelper.Read<AddAccountingInfo>($"{directoryName}\\Data.csv"));
                        break;
                    }
                }
            }
            IQueryable<AddAccountingInfo> query = addAccountingInfos.AsQueryable();
            var predicate1 = PredicateBuilder.New<AddAccountingInfo>(true);
            var predicate2 = PredicateBuilder.New<AddAccountingInfo>(true);
            var predicate3 = PredicateBuilder.New<AddAccountingInfo>(true);
            var predicate4 = PredicateBuilder.New<AddAccountingInfo>(true);
            var predicate5 = PredicateBuilder.New<AddAccountingInfo>(true);

            foreach (var condition in SelectItemInfo.A_Conditions)
                predicate1 = predicate1.Or(condition);
            foreach (var condition in SelectItemInfo.B_Conditions)
                predicate2 = predicate2.Or(condition);
            foreach (var condition in SelectItemInfo.C_Conditions)
                predicate3 = predicate3.Or(condition);
            foreach (var condition in SelectItemInfo.D_Conditions)
                predicate4 = predicate4.Or(condition);
            foreach (var condition in SelectItemInfo.E_Conditions)
                predicate5 = predicate5.Or(condition);

            var finalPredicate = predicate1
                                 .And(predicate2)
                                 .And(predicate3)
                                 .And(predicate4)
                                 .And(predicate5);

            var queryList = query.Where(finalPredicate).ToList();
            return queryList;
        }

        public List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments)
        {
            String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
            searchDate.StartTime = searchDate.StartTime.Date;
            searchDate.EndTime = searchDate.EndTime.Date;
            if (searchDate.StartTime > searchDate.EndTime)
                return null;
            int diffDays = (searchDate.EndTime - searchDate.StartTime).Days + 1;
            List<String> directories = Directory.GetDirectories(fileServerPath).ToList();
            List<AddAccountingInfo> addAccountingInfos = new List<AddAccountingInfo>();
            for (int i = 0; i < diffDays; i++)
            {
                String directoryName = $"{fileServerPath}{searchDate.StartTime.AddDays(i).ToString("yyyy-MM-dd")}";
                foreach (var directory in directories)
                {
                    if (directory == directoryName)
                    {
                        addAccountingInfos.AddRange(CSVHelper.Read<AddAccountingInfo>($"{directoryName}\\Data.csv"));
                        break;
                    }
                }
            }

            if (purpose.Count > 0)
                addAccountingInfos = addAccountingInfos.Where(x => purpose.Contains(x.Purpose)).ToList();
            if (companions.Count > 0)
                addAccountingInfos = addAccountingInfos.Where(x => companions.Contains(x.Companion)).ToList();
            if (payments.Count > 0)
                addAccountingInfos = addAccountingInfos.Where(x => payments.Contains(x.Payment)).ToList();



            //IQueryable<AddAccountingInfo> query = addAccountingInfos.AsQueryable();
            //var predicate1 = PredicateBuilder.New<AddAccountingInfo>(true);
            //var predicate2 = PredicateBuilder.New<AddAccountingInfo>(true);
            //var predicate3 = PredicateBuilder.New<AddAccountingInfo>(true);
            //var predicate4 = PredicateBuilder.New<AddAccountingInfo>(true);
            //var predicate5 = PredicateBuilder.New<AddAccountingInfo>(true);

            //foreach (var condition in SelectItemInfo.A_Conditions)
            //    predicate1 = predicate1.Or(condition);
            //foreach (var condition in SelectItemInfo.B_Conditions)
            //    predicate2 = predicate2.Or(condition);
            //foreach (var condition in SelectItemInfo.C_Conditions)
            //    predicate3 = predicate3.Or(condition);
            //foreach (var condition in SelectItemInfo.D_Conditions)
            //    predicate4 = predicate4.Or(condition);
            //foreach (var condition in SelectItemInfo.E_Conditions)
            //    predicate5 = predicate5.Or(condition);

            //var finalPredicate = predicate1
            //                     .And(predicate2)
            //                     .And(predicate3)
            //                     .And(predicate4)
            //                     .And(predicate5);

            //var queryList = query.Where(finalPredicate).ToList();
            return addAccountingInfos;
        }

        public List<GroupByAmount> GetGroupByAmounts(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys)
        {
            String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
            searchDate.StartTime = searchDate.StartTime.Date;
            searchDate.EndTime = searchDate.EndTime.Date;
            if (searchDate.StartTime > searchDate.EndTime)
                return null;
            int diffDays = (searchDate.EndTime - searchDate.StartTime).Days + 1;
            List<String> directories = Directory.GetDirectories(fileServerPath).ToList();
            List<AddAccountingInfo> addAccountingInfos = new List<AddAccountingInfo>();
            for (int i = 0; i < diffDays; i++)
            {
                String directoryName = $"{fileServerPath}{searchDate.StartTime.AddDays(i).ToString("yyyy-MM-dd")}";
                foreach (var directory in directories)
                {
                    if (directory == directoryName)
                    {
                        addAccountingInfos.AddRange(CSVHelper.Read<AddAccountingInfo>($"{directoryName}\\Data.csv"));
                        break;
                    }
                }
            }
            if (purpose.Count > 0)
                addAccountingInfos = addAccountingInfos.Where(x => purpose.Contains(x.Purpose)).ToList();
            if (companions.Count > 0)
                addAccountingInfos = addAccountingInfos.Where(x => companions.Contains(x.Companion)).ToList();
            if (payments.Count > 0)
                addAccountingInfos = addAccountingInfos.Where(x => payments.Contains(x.Payment)).ToList();

            var groupByAmount = addAccountingInfos.GroupBy(x => new
            {
                Type = SelectItemInfo.OrderBys["類型"] ? x.Type : null,
                Purpose = SelectItemInfo.OrderBys["目的"] ? x.Purpose : null,
                Companion = SelectItemInfo.OrderBys["對象"] ? x.Companion : null,
                Payment = SelectItemInfo.OrderBys["付款方式"] ? x.Payment : null,
            }).Select(g => new GroupByAmount
            {
                GroupKey = g.Key.ToString(),
                Amount = g.Sum(x => Convert.ToInt32(x.Amount))
            })
            .ToList();
            return groupByAmount;
        }
    }
}
