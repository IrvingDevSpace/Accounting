using Accounting.Models;
using Accounting.Repositories.Implementations;
using Accounting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Presenter
{
    internal class AccountingDataPresenter : IAccountingDataPresenter
    {
        private IAccountingDataView _accountingDataView = null;
        private IAddAccountingInfoRepository _addAccountingInfoRepository = null;

        public AccountingDataPresenter(IAccountingDataView accountingDataView)
        {
            _accountingDataView = accountingDataView;
            _addAccountingInfoRepository = new AddAccountingInfoRepository();
        }

        public void GetAddAccountingInfos(SearchDate searchDate)
        {
            List<AddAccountingInfo> addAccountingInfos = _addAccountingInfoRepository.GetAddAccountingInfos(searchDate);
            _accountingDataView.RenderAddAccountingInfos(addAccountingInfos);
        }

        public void GetAddAccountingInfos(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments)
        {
            List<AddAccountingInfo> addAccountingInfos = _addAccountingInfoRepository.GetAddAccountingInfos(searchDate, purpose, companions, payments);
            _accountingDataView.RenderAddAccountingInfos(addAccountingInfos);
        }

        public void GetGroupByAmounts(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys)
        {
            List<GroupByAmount> groupByAmounts = _addAccountingInfoRepository.GetGroupByAmounts(searchDate, purpose, companions, payments, orderBys);
            _accountingDataView.RenderGroupByAmounts(groupByAmounts);
        }

        public void GetTwoGroupByAmounts(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys)
        {
            List<GroupByAmount> groupByAmounts1 = _addAccountingInfoRepository.GetGroupByAmounts(searchDate, purpose, companions, payments, orderBys);
            searchDate.StartTime = searchDate.StartTime.AddMonths(1);
            searchDate.EndTime = searchDate.EndTime.AddMonths(1);
            List<GroupByAmount> groupByAmounts2 = _addAccountingInfoRepository.GetGroupByAmounts(searchDate, purpose, companions, payments, orderBys);
            groupByAmounts1.AddRange(groupByAmounts2);
            _accountingDataView.RenderGroupByAmounts(groupByAmounts1);
        }
    }
}
