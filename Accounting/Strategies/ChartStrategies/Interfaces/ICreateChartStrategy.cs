using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Strategies.ChartStrategies.Interfaces
{
    internal interface ICreateChartStrategy
    {
        void SetChartArea(Chart chart);
        void SetValue(Chart chart, List<GroupByAmount> groupByAmounts);
    }
}
