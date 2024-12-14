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
            void GetAccountingInfos(SearchDate searchDate);
            void GetGroupByAmounts(SearchDate searchDate);
            void GetTwoGroupByAmounts(SearchDate searchDate);
        }
    }
}
