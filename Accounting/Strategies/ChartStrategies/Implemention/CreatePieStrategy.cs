using Accounting.Models;
using Accounting.Strategies.ChartStrategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Strategies.ChartStrategies.Implemention
{
    internal class CreatePieStrategy : ICreateChartStrategy
    {
        public void SetChartArea(Chart chart)
        {
            ChartArea chartArea = new ChartArea("PieArea");
            chart.ChartAreas.Add(chartArea);
        }

        public void SetValue(Chart chart, List<GroupByAmount> groupByAmounts)
        {
            Series series = new Series
            {
                ChartType = SeriesChartType.Pie,
                Name = "DataSeries"
            };
            chart.Series.Add(series);

            foreach (GroupByAmount amount in groupByAmounts)
                series.Points.AddXY(amount.GroupKey, amount.Amount);

            // 設定標籤
            // #PERCENT	顯示該數據點的百分比，例如 50%。
            // #VAL	顯示該數據點的值，例如 40。
            // #VALX	顯示該數據點的 X 軸值（類別名稱）。
            // #SERIESNAME	顯示系列名稱（Series 的 Name）。
            // #LEGENDTEXT	顯示對應圖例的文字（通常是 X 軸值）。
            // #INDEX	顯示數據點的索引，例如 0, 1, 2 等。

            series.Label = "#VAL / #PERCENT{P0}"; // 百分比格式
            //series.Label = "#VAL"; 
            series.LegendText = "#VALX";
            series.LabelForeColor = System.Drawing.Color.Black;
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Blue";
        }
    }
}
