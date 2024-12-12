using Accounting.Models;
using System.Collections.Generic;

namespace Accounting.Repositories.Interfaces
{
    internal interface IAccountingInfoRepository
    {
        List<AccountingInfo> GetAccountingInfos(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments);
        List<GroupByAmount> GetGroupByAmounts(SearchDate searchDate, List<string> purposes, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys);
    }
}
