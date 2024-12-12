using Accounting.Models;
using System.Collections.Generic;

namespace Accounting.Contract
{
    internal class AccountingDataContract
    {
        public interface IAccountingDataView
        {
            void RenderAccountingInfos(List<AccountingInfo> AccountingInfos);
            void RenderGroupByAmounts(List<GroupByAmount> groupByAmounts);
        }


        public interface IAccountingDataPresenter
        {
            void GetAccountingInfos(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments);
            void GetGroupByAmounts(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys);
            void GetTwoGroupByAmounts(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys);
        }
    }
}
