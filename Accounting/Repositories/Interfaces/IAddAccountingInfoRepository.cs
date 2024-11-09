using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repositories.Interfaces
{
    internal interface IAddAccountingInfoRepository
    {
        List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate);
    }
}
