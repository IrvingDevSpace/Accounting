using Accounting.Enums;
using Accounting.Models;
using Accounting.Strategies.ChartStrategies.Interfaces;
using Accounting.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Utility.Implementation
{
    internal class ChartBuilder : IChartBuilder
    {
        private Chart _chart;

        public ChartBuilder()
        {
            _chart = new Chart();
        }

        public IChartBuilder SetDockStyle(DockStyle dockStyle)
        {
            _chart.Dock = dockStyle;
            return this;
        }

        //public IChartBuilder SetPieChartArea()
        //{
        //    ChartArea chartArea = new ChartArea("PieArea");
        //    _chart.ChartAreas.Add(chartArea);
        //    return this;
        //}

        //public IChartBuilder SetStackedChartArea()
        //{
        //    ChartArea chartArea = new ChartArea("StackedArea");
        //    chartArea.AxisX.Title = "分類";
        //    chartArea.AxisY.Title = "百分比 (%)";
        //    chartArea.AxisY.Minimum = 0;
        //    chartArea.AxisY.Maximum = 100;
        //    _chart.ChartAreas.Add(chartArea);
        //    return this;
        //}

        //public IChartBuilder SetLineChartArea()
        //{
        //    ChartArea chartArea = new ChartArea("LineArea");
        //    chartArea.AxisX.Title = "時間";
        //    chartArea.AxisX.IntervalType = DateTimeIntervalType.Days;
        //    chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //    _chart.ChartAreas.Add(chartArea);
        //    return this;
        //}

        public IChartBuilder SetLegendDocking(Docking docking)
        {
            Legend legend = new Legend
            {
                Docking = docking
            };
            _chart.Legends.Add(legend);
            return this;
        }

        public Chart Build()
        {
            return _chart;
        }

        public IChartBuilder Reset()
        {
            _chart = new Chart();
            return this;
        }

        //public IChartBuilder SetPieValue(List<GroupByAmount> groupByAmounts)
        //{
        //    Series series = new Series
        //    {
        //        ChartType = SeriesChartType.Pie,
        //        Name = "DataSeries"
        //    };
        //    _chart.Series.Add(series);

        //    foreach (GroupByAmount amount in groupByAmounts)
        //        series.Points.AddXY(amount.GroupKey, amount.Amount);

        //    // 設定標籤
        //    // #PERCENT	顯示該數據點的百分比，例如 50%。
        //    // #VAL	顯示該數據點的值，例如 40。
        //    // #VALX	顯示該數據點的 X 軸值（類別名稱）。
        //    // #SERIESNAME	顯示系列名稱（Series 的 Name）。
        //    // #LEGENDTEXT	顯示對應圖例的文字（通常是 X 軸值）。
        //    // #INDEX	顯示數據點的索引，例如 0, 1, 2 等。

        //    series.Label = "#VAL / #PERCENT{P0}"; // 百分比格式
        //    //series.Label = "#VAL"; 
        //    series.LegendText = "#VALX";
        //    series.LabelForeColor = System.Drawing.Color.Black;
        //    series["PieLabelStyle"] = "Outside";
        //    series["PieLineColor"] = "Blue";

        //    return this;
        //}

        //public IChartBuilder SetStackedValue(List<GroupByAmount> groupByAmounts)
        //{
        //    List<Series> seriesList = new List<Series>();
        //    float amount = 0;
        //    int total = 0;
        //    for (int i = 0; i < groupByAmounts.Count; i++)
        //    {
        //        Series series = new Series
        //        {
        //            ChartType = SeriesChartType.StackedColumn100,
        //            Name = $"{groupByAmounts[i].GroupKey}",
        //            Color = ColorUtils.GetRandomColor()
        //        };
        //        series.Points.AddXY(groupByAmounts[i].GroupKey, groupByAmounts[i].Amount);
        //        series.LabelForeColor = Color.White;
        //        amount += groupByAmounts[i].Amount;
        //        total += groupByAmounts[i].Amount;
        //        _chart.Series.Add(series);
        //    }

        //    foreach (var series in _chart.Series)
        //    {
        //        for (int i = 0; i < series.Points.Count; i++)
        //        {
        //            double percentage = series.Points[i].YValues[0] / amount;
        //            series.Points[i].Label = percentage.ToString("P");
        //        }
        //    }

        //    return this;
        //}

        //public IChartBuilder SetLineValue(List<GroupByAmount> groupByAmounts)
        //{
        //    Dictionary<string, Series> seriesDict = new Dictionary<string, Series>();
        //    foreach (var item in groupByAmounts)
        //    {
        //        var groupKeys = item.GroupKey.Split(',');
        //        if (DateTime.TryParse(groupKeys[0], out DateTime date))
        //        {
        //            string name = string.Join(", ", item.GroupKey.Split(',').Skip(1).ToList());
        //            if (!seriesDict.TryGetValue(name, out Series series))
        //            {
        //                series = new Series
        //                {
        //                    ChartType = SeriesChartType.Line,
        //                    Name = name,
        //                    BorderWidth = 2,
        //                    Color = ColorUtils.GetRandomColor()
        //                };
        //                seriesDict.Add(name, series);
        //                _chart.Series.Add(series);
        //            }
        //            series.Points.AddXY(date, item.Amount);
        //        }
        //        else
        //        {
        //            string name = "Line";
        //            if (!seriesDict.TryGetValue(name, out Series series))
        //            {
        //                series = new Series
        //                {
        //                    ChartType = SeriesChartType.Line,
        //                    Name = name,
        //                    BorderWidth = 2,
        //                    Color = ColorUtils.GetRandomColor()
        //                };
        //                seriesDict.Add(name, series);
        //                _chart.Series.Add(series);
        //            }
        //            series.Points.AddXY(item.GroupKey, item.Amount);
        //        }
        //    }
        //    return this;
        //}

        public IChartBuilder SetChartAreaAndValue(ChartTag chartTag, List<GroupByAmount> groupByAmounts)
        {
            Type type = Type.GetType($"Accounting.Strategies.ChartStrategies.Implemention.Create{chartTag.ToString()}Strategy");
            ICreateChartStrategy createChartStrategy = (ICreateChartStrategy)Activator.CreateInstance(type);
            createChartStrategy.SetChartArea(_chart);
            createChartStrategy.SetValue(_chart, groupByAmounts);
            return this;
        }
    }
}
