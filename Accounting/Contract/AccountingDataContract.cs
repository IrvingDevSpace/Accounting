using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Accounting.Contract
{
    internal class AccountingDataContract
    {
        public interface IAccountingDataView
        {
            void RenderAddAccountingInfos(List<AddAccountingInfo> addAccountingInfos);
            void RenderGroupByAmounts(List<GroupByAmount> groupByAmounts);
        }


        public interface IAccountingDataPresenter
        {
            void GetAddAccountingInfos(SearchDate searchDate);
            void GetAddAccountingInfos(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments);
            void GetGroupByAmounts(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys);
            void GetTwoGroupByAmounts(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys);
        }
    }
}
