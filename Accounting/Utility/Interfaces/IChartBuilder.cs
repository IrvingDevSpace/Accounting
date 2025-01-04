using Accounting.Enums;
using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Utility.Interfaces
{
    internal interface IChartBuilder
    {
        IChartBuilder SetDockStyle(DockStyle dockStyle);
        //IChartBuilder SetPieChartArea();
        //IChartBuilder SetStackedChartArea();
        //IChartBuilder SetLineChartArea();
        IChartBuilder SetLegendDocking(Docking docking);
        //IChartBuilder SetPieValue(List<GroupByAmount> groupByAmounts);
        //IChartBuilder SetStackedValue(List<GroupByAmount> groupByAmounts);
        //IChartBuilder SetLineValue(List<GroupByAmount> groupByAmounts);
        IChartBuilder SetChartAreaAndValue(ChartTag chartTag, List<GroupByAmount> groupByAmounts);
        IChartBuilder Reset();
        Chart Build();
    }
}
