using Accounting.Models;
using Accounting.Strategies.ChartStrategies.Interfaces;
using Accounting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Strategies.ChartStrategies.Implemention
{
    internal class CreateLineStrategy : ICreateChartStrategy
    {
        public void SetChartArea(Chart chart)
        {
            ChartArea chartArea = new ChartArea("LineArea");
            chartArea.AxisX.Title = "時間";
            chartArea.AxisX.IntervalType = DateTimeIntervalType.Days;
            chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart.ChartAreas.Add(chartArea);
        }

        public void SetValue(Chart chart, List<GroupByAmount> groupByAmounts)
        {
            Dictionary<string, Series> seriesDict = new Dictionary<string, Series>();
            foreach (var item in groupByAmounts)
            {
                var groupKeys = item.GroupKey.Split(',');
                if (DateTime.TryParse(groupKeys[0], out DateTime date))
                {
                    string name = string.Join(", ", item.GroupKey.Split(',').Skip(1).ToList());
                    if (!seriesDict.TryGetValue(name, out Series series))
                    {
                        series = new Series
                        {
                            ChartType = SeriesChartType.Line,
                            Name = name,
                            BorderWidth = 2,
                            Color = ColorUtils.GetRandomColor()
                        };
                        seriesDict.Add(name, series);
                        chart.Series.Add(series);
                    }
                    series.Points.AddXY(date, item.Amount);
                }
                else
                {
                    string name = "Line";
                    if (!seriesDict.TryGetValue(name, out Series series))
                    {
                        series = new Series
                        {
                            ChartType = SeriesChartType.Line,
                            Name = name,
                            BorderWidth = 2,
                            Color = ColorUtils.GetRandomColor()
                        };
                        seriesDict.Add(name, series);
                        chart.Series.Add(series);
                    }
                    series.Points.AddXY(item.GroupKey, item.Amount);
                }
            }
        }
    }
}
