using Accounting.Models;
using Accounting.Repositories.Implementations;
using Accounting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //public void GetAddAccountingInfos(SearchDate searchDate)
        //{
        //    List<AddAccountingInfo> addAccountingInfos = _addAccountingInfoRepository.GetAddAccountingInfos(searchDate);
        //    _accountingDataView.RenderAddAccountingInfos(addAccountingInfos);
        //}
    }
}
