using Accounting.Models;
using Accounting.Strategies.ChartStrategies.Interfaces;
using Accounting.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Strategies.ChartStrategies.Implemention
{
    internal class CreateStackedStrategy : ICreateChartStrategy
    {
        public void SetChartArea(Chart chart)
        {
            ChartArea chartArea = new ChartArea("StackedArea");
            chartArea.AxisX.Title = "分類";
            chartArea.AxisY.Title = "百分比 (%)";
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;
            chart.ChartAreas.Add(chartArea);
        }

        public void SetValue(Chart chart, List<GroupByAmount> groupByAmounts)
        {
            List<Series> seriesList = new List<Series>();
            float amount = 0;
            int total = 0;
            for (int i = 0; i < groupByAmounts.Count; i++)
            {
                Series series = new Series
                {
                    ChartType = SeriesChartType.StackedColumn100,
                    Name = $"{groupByAmounts[i].GroupKey}",
                    Color = ColorUtils.GetRandomColor()
                };
                series.Points.AddXY(groupByAmounts[i].GroupKey, groupByAmounts[i].Amount);
                series.LabelForeColor = Color.White;
                amount += groupByAmounts[i].Amount;
                total += groupByAmounts[i].Amount;
                chart.Series.Add(series);
            }

            foreach (var series in chart.Series)
            {
                for (int i = 0; i < series.Points.Count; i++)
                {
                    double percentage = series.Points[i].YValues[0] / amount;
                    series.Points[i].Label = percentage.ToString("P");
                }
            }
        }
    }
}
