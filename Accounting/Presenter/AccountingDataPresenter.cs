using Accounting.Enums;
using Accounting.Models;
using Accounting.Repositories.Implementations;
using Accounting.Repositories.Interfaces;
using Accounting.Utility.Implementation;
using Accounting.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Presenter
{
    internal class AccountingDataPresenter : IAccountingDataPresenter
    {
        private IAccountingDataView _accountingDataView;
        private IAccountingInfoRepository _accountingInfoRepository;
        private IChartBuilder _chartBuilder;

        public AccountingDataPresenter(IAccountingDataView accountingDataView)
        {
            _accountingDataView = accountingDataView;
            _accountingInfoRepository = new AccountingInfoRepository();
            _chartBuilder = new ChartBuilder();
        }

        public void GetAccountingInfos(SearchDate searchDate)
        {
            List<AccountingInfo> accountingInfos = _accountingInfoRepository.GetAccountingInfos(searchDate);
            _accountingDataView.RenderAccountingInfos(accountingInfos);
        }

        public void GetGroupByAmounts(SearchDate searchDate)
        {
            List<GroupByAmount> groupByAmounts = _accountingInfoRepository.GetGroupByAmounts(searchDate);
            _accountingDataView.RenderGroupByAmounts(groupByAmounts);
        }

        public void GetTwoGroupByAmounts(SearchDate searchDate)
        {
            List<GroupByAmount> groupByAmounts1 = _accountingInfoRepository.GetGroupByAmounts(searchDate);
            searchDate.StartTime = searchDate.StartTime.AddMonths(1);
            searchDate.EndTime = searchDate.EndTime.AddMonths(1);
            List<GroupByAmount> groupByAmounts2 = _accountingInfoRepository.GetGroupByAmounts(searchDate);
            groupByAmounts1.AddRange(groupByAmounts2);
            _accountingDataView.RenderGroupByAmounts(groupByAmounts1);
        }

        public void GetChart(SearchDate searchDate, string chartType)
        {
            List<GroupByAmount> groupByAmounts = _accountingInfoRepository.GetGroupByAmounts(searchDate);
            Chart chart = new Chart();
            if (!Enum.TryParse(chartType, out ChartTag chartTag))
                return;
            chart = CreateChart(chartTag, groupByAmounts);
            _accountingDataView.RenderChart(chart);
        }

        private Chart CreateChart(ChartTag chartTag, List<GroupByAmount> groupByAmounts)
        {
            Chart pieChart = _chartBuilder
                .Reset()
                .SetDockStyle(DockStyle.Fill)
                .SetLegendDocking(Docking.Right)
                .SetChartAreaAndValue(chartTag, groupByAmounts)
                .Build();

            return pieChart;
        }

        //private Chart CreateStackedChart(List<GroupByAmount> groupByAmounts)
        //{
        //    Chart stackedChart = _chartBuilder
        //      .Reset()
        //      .SetDockStyle(DockStyle.Fill)
        //      .SetStackedChartArea()
        //      .SetLegendDocking(Docking.Top)
        //      .SetStackedValue(groupByAmounts)
        //      .Build();

        //    return stackedChart;
        //}

        //private Chart CreateLineChart(List<GroupByAmount> groupByAmounts)
        //{
        //    Chart lineChart = _chartBuilder
        //     .Reset()
        //     .SetDockStyle(DockStyle.Fill)
        //     .SetLineChartArea()
        //     .SetLegendDocking(Docking.Top)
        //     .SetLineValue(groupByAmounts)
        //     .Build();

        //    return lineChart;
        //}
    }
}
