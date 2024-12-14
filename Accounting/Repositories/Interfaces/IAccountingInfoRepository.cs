using Accounting.Models;
using System.Collections.Generic;

namespace Accounting.Repositories.Interfaces
{
    internal interface IAccountingInfoRepository
    {
        List<AccountingInfo> GetAccountingInfos(SearchDate searchDate);
        List<GroupByAmount> GetGroupByAmounts(SearchDate searchDate);
    }
}
