using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Extension
{
    internal static class ChartExtension
    {
        public static void ChartCreate(this Chart chart, List<ChartParam> chartParams, String chartName, Font font, String xTitle, String yTitle, Double intervalX, Double intervalY)
        {
            // Chart
            ChartCreate(chart, chartName);
            // Legend
            LegendCreate(chart, font);
            // Series
            SeriesCreate(chart, chartParams, font);
            // ChartArea
            ChartArea chartArea = new ChartArea();
            //// StripLine
            //StripLineCreate(chartArea, stripLines);
            ChartAreaCreate(chart, chartArea, font, xTitle, yTitle, intervalX, intervalY);
        }

        public static void ChartCreate(this Chart chart, List<ChartParam> chartParams, String chartName, Font font, String xTitle, String yTitle, List<StripLine> stripLines, Double intervalX, Double intervalY)
        {
            // Chart
            ChartCreate(chart, chartName);
            // Legend
            LegendCreate(chart, font);
            // Series
            SeriesCreate(chart, chartParams, font);
            // ChartArea
            ChartArea chartArea = new ChartArea();
            // StripLine
            StripLineCreate(chartArea, stripLines);
            ChartAreaCreate(chart, chartArea, font, xTitle, yTitle, intervalX, intervalY);
        }

        private static void ChartCreate(Chart chart, String chartName)
        {
            chart.Name = chartName;
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.AliceBlue;
        }

        private static void LegendCreate(Chart chart, Font font)
        {
            Legend legend = new Legend
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = font,
                BackColor = Color.Transparent
            };
            chart.Legends.Add(legend);
        }

        private static void SeriesCreate(Chart chart, List<ChartParam> chartParams, Font font)
        {
            for (int i = 0; i < chartParams.Count; i++)
            {
                Series series = new Series
                {
                    //IsValueShownAsLabel = true, // 顯示每個點
                    //Font = new Font("Microsoft JhengHei", 8, FontStyle.Bold), // 每個點的字體
                    Name = chartParams[i].Name,
                    Label = "#VAL",
                    LegendText = "#VALX",
                    ChartType = chartParams[i].ChartType,
                    BorderDashStyle = chartParams[i].ChartDashStyle,
                    BorderWidth = chartParams[i].BorderWidth,
                    Color = chartParams[i].BorderColor,
                    XValueType = chartParams[i].XValueType,
                    YValueType = chartParams[i].YValueType,
                    ToolTip = $"#VALX {chartParams[i].Name} Value : #VAL"
                };
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Blue";
                chart.Series.Add(series);
            }
        }

        private static void StripLineCreate(ChartArea chartArea, List<StripLine> stripLines)
        {
            for (int i = 0; i < stripLines.Count; i++)
                chartArea.AxisY.StripLines.Add(stripLines[i]);
        }

        private static void ChartAreaCreate(Chart chart, ChartArea chartArea, Font font, String xTitle, String yTitle, Double intervalX, Double intervalY)
        {
            //chartArea.AxisX.IsMarginVisible = false;
            //chartArea.AxisX.IntervalType = DateTimeIntervalType.Seconds;
            //chartArea.AxisX.Interval = intervalX;
            //chartArea.AxisY.Interval = intervalY;
            //chartArea.AxisY.Minimum = 0;
            //chartArea.AxisY.Maximum = 500;
            //chartArea.AxisX.LabelStyle.Format = "HH:mm:ss";
            //chartArea.AxisX.LabelStyle.Font = font; // Label字體大小無法設定
            //chartArea.AxisY.LabelStyle.Font = font; // Label字體大小無法設定
            //chartArea.AxisX.Title = xTitle;
            //chartArea.AxisY.Title = yTitle;
            //chartArea.AxisX.TitleFont = font;
            //chartArea.AxisY.TitleFont = font;
            //chartArea.AxisX.LineColor = Color.LightGray; // x軸顏色
            //chartArea.AxisY.LineColor = Color.LightGray; // y軸顏色
            //chartArea.AxisX.MajorGrid.LineColor = Color.LightGray; // x網格線顏色
            //chartArea.AxisY.MajorGrid.LineColor = Color.LightGray; // y網格線顏色
            //chartArea.AxisX.MajorTickMark.LineColor = Color.LightGray; // x軸刻度顏色
            //chartArea.AxisY.MajorTickMark.LineColor = Color.LightGray; // y軸刻度顏色
            //chartArea.BackColor = Color.AliceBlue;
            //chartArea.BackGradientStyle = GradientStyle.HorizontalCenter; // 漸層方式
            //chartArea.BorderDashStyle = ChartDashStyle.Solid; // 邊框線Solid--實線
            //chartArea.BorderColor = Color.LightGray; // 邊框顏色
            //chartArea.ShadowColor = Color.Gray; // 陰影
            //chartArea.ShadowOffset = 2; // 陰影偏移量
            //chartArea.Position.Auto = false;
            //chartArea.Position.X = 2; // X 軸的起始位置（以百分比計）
            //chartArea.Position.Y = 0; // Y 軸的起始位置（以百分比計）
            //chartArea.Position.Width = 92; // ChartArea 的寬度（以百分比計）
            //chartArea.Position.Height = 92; // ChartArea 的高度增加（以百分比計）
            chart.ChartAreas.Add(chartArea);
        }

        public static void RemoveOldPoints(this Series series, int timeLimitInSeconds)
        {
            DateTime threshold = DateTime.Now.AddSeconds(-timeLimitInSeconds);
            while (series.Points.Count > 0 && series.Points[0].XValue < threshold.ToOADate())
                series.Points.RemoveAt(0);
        }
    }
}
