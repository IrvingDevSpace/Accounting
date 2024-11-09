using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Contract
{
    internal class AccountingDataContract
    {
        public interface IAccountingDataView
        {
            void RenderAddAccountingInfos(List<AddAccountingInfo> addAccountingInfos);
        }


        public interface IAccountingDataPresenter
        {
            void GetAddAccountingInfos(SearchDate searchDate);
        }
    }
}
