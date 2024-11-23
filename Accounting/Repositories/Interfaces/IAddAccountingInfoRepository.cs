using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Accounting.Repositories.Interfaces
{
    internal interface IAddAccountingInfoRepository
    {
        List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate);
        List<AddAccountingInfo> GetAddAccountingInfos(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments);
        List<GroupByAmount> GetGroupByAmounts(SearchDate searchDate, List<string> purpose, List<string> companions, List<string> payments, Dictionary<string, bool> orderBys);
    }
}
