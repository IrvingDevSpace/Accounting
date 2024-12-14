using Accounting.Models;
using Accounting.Repositories.Implementations;
using Accounting.Repositories.Interfaces;
using System.Collections.Generic;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Presenter
{
    internal class AccountingDataPresenter : IAccountingDataPresenter
    {
        private IAccountingDataView _accountingDataView;
        private IAccountingInfoRepository _accountingInfoRepository;

        public AccountingDataPresenter(IAccountingDataView accountingDataView)
        {
            _accountingDataView = accountingDataView;
            _accountingInfoRepository = new AccountingInfoRepository();
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
    }
}
