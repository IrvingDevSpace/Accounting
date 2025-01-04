using Accounting.Models;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Contract
{
    internal class AccountingDataContract
    {
        public interface IAccountingDataView
        {
            void RenderAccountingInfos(List<AccountingInfo> AccountingInfos);
            void RenderGroupByAmounts(List<GroupByAmount> groupByAmounts);
            void RenderChart(Chart chart);
        }


        public interface IAccountingDataPresenter
        {
            void GetAccountingInfos(SearchDate searchDate);
            void GetGroupByAmounts(SearchDate searchDate);
            void GetTwoGroupByAmounts(SearchDate searchDate);
            void GetChart(SearchDate searchDate, string chartType);
        }
    }
}
