using Accounting.Attributes;
using Accounting.Components;
using Accounting.Extension;
using Accounting.Models;
using Accounting.Presenter;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("圖表分析")]
    public partial class ChartForm : Form, IAccountingDataView
    {
        private Navbar navbar;
        private IAccountingDataPresenter _accountingDataPresenter = null;
        List<GroupByAmount> _groupByAmounts = new List<GroupByAmount>();

        public ChartForm()
        {
            InitializeComponent();
            _accountingDataPresenter = new AccountingDataPresenter(this);
            this.Text = this.GetFormTitle();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddEvent();
        }

        private void AddEvent()
        {
            this.Load += ChartForm_Load;
            this.FormClosed += ChartForm_FormClosed;
        }

        private void ChartForm_Load(object sender, System.EventArgs e)
        {
            navbar = new Navbar
            {
                Location = new System.Drawing.Point(43, 400),
                Name = "Navbar_ChangeForm",
                Size = new System.Drawing.Size(550, 70)
            };
            this.Controls.Add(navbar);
            this.SetFormsNavbarButton();
            List<string> strings = new List<string>
            {
                "Pie", "Stack", "Line"
            };

            //Chart chart = CreatePieChart();
            //Chart chart = CreateStackedChart();
            //Chart chart = CreateLineChart();
            chartDictionary.Add("Pie", CreatePieChart);
            chartDictionary.Add("Stack", CreateStackedChart);
            chartDictionary.Add("Line", CreateLineChart);
            comboBox1.Items.AddRange(strings.ToArray());
            comboBox1.SelectedIndex = 0;
        }

        private void ChartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private Chart CreatePieChart(List<GroupByAmount> groupByAmounts)
        {
            Chart pieChart = new Chart();
            pieChart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea();
            pieChart.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                ChartType = SeriesChartType.Pie,
                Name = "DataSeries"
            };
            pieChart.Series.Add(series);

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

            Legend legend = new Legend
            {
                Docking = Docking.Right
            };
            pieChart.Legends.Add(legend);
            return pieChart;
        }

        private Chart CreateStackedChart(List<GroupByAmount> groupByAmounts)
        {
            List<float> floats = new List<float>
            {
                0, 0, 0
            };

            Chart stackedChart = new Chart();
            stackedChart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea("StackedArea");
            chartArea.AxisX.Title = "分類";
            chartArea.AxisY.Title = "百分比 (%)";
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;
            stackedChart.ChartAreas.Add(chartArea);

            List<Series> seriesList = new List<Series>();
            int total = 0;
            for (int i = 0; i < groupByAmounts.Count; i++)
            {
                Series series = new Series
                {
                    ChartType = SeriesChartType.StackedColumn100,
                    Name = $"{groupByAmounts[i].GroupKey}",
                    Color = GetRandomColor()
                };
                series.Points.AddXY(groupByAmounts[i].GroupKey, groupByAmounts[i].Amount);
                series.LabelForeColor = Color.White;
                floats[0] += groupByAmounts[i].Amount;
                total += groupByAmounts[i].Amount;
                stackedChart.Series.Add(series);
            }

            foreach (var series in stackedChart.Series)
            {
                for (int i = 0; i < series.Points.Count; i++)
                {
                    double percentage = series.Points[i].YValues[0] / floats[i];
                    series.Points[i].Label = percentage.ToString("P");
                }
            }

            Legend legend = new Legend
            {
                Docking = Docking.Top
            };
            stackedChart.Legends.Add(legend);
            return stackedChart;
        }

        private Chart CreateLineChart(List<GroupByAmount> groupByAmounts)
        {
            Chart lineChart = new Chart();
            lineChart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea("LineArea");
            chartArea.AxisX.Title = "時間";
            chartArea.AxisY.Title = "數值";
            lineChart.ChartAreas.Add(chartArea);

            int count = groupByAmounts.Count / 2;
            for (int i = 0; i < count; i++)
            {
                Series series = new Series
                {
                    ChartType = SeriesChartType.Line,
                    Name = groupByAmounts[i].GroupKey,
                    BorderWidth = 2,
                    Color = GetRandomColor()
                };
                lineChart.Series.Add(series);

                series.Points.AddXY(groupByAmounts[i].GroupKey, groupByAmounts[i].Amount);
            }

            for (int i = count; i < groupByAmounts.Count; i++)
            {
                bool hasSeries = lineChart.Series.ToList().Any(x => x.Name == groupByAmounts[i].GroupKey);
                if (!hasSeries)
                {
                    Series series = new Series
                    {
                        ChartType = SeriesChartType.Line,
                        Name = groupByAmounts[i].GroupKey,
                        BorderWidth = 2,
                        Color = GetRandomColor()
                    };
                    lineChart.Series.Add(series);
                }
                lineChart.Series[groupByAmounts[i].GroupKey].Points.AddXY(groupByAmounts[i].GroupKey, groupByAmounts[i].Amount);
            }


            Legend legend = new Legend
            {
                Docking = Docking.Top
            };
            lineChart.Legends.Add(legend);

            return lineChart;
        }

        //private Chart CreateStackedChart(List<GroupByAmount> groupByAmounts)
        //{
        //    List<float> floats = new List<float>
        //    {
        //        0, 0, 0
        //    };

        //    Chart stackedChart = new Chart();
        //    stackedChart.Dock = DockStyle.Fill;

        //    ChartArea chartArea = new ChartArea("StackedArea");
        //    chartArea.AxisX.Title = "分類";
        //    chartArea.AxisY.Title = "百分比 (%)";
        //    chartArea.AxisY.Minimum = 0;
        //    chartArea.AxisY.Maximum = 100;
        //    stackedChart.ChartAreas.Add(chartArea);

        //    foreach (var amount in groupByAmounts)
        //    {

        //    }

        //    Series series1 = new Series
        //    {
        //        ChartType = SeriesChartType.StackedColumn100,
        //        Name = "數據 1",
        //        Color = System.Drawing.Color.Blue
        //    };
        //    stackedChart.Series.Add(series1);

        //    series1.Points.AddXY("分類 A", 10);
        //    floats[0] += 10;
        //    series1.Points.AddXY("分類 B", 20);
        //    floats[1] += 20;
        //    series1.Points.AddXY("分類 C", 15);
        //    floats[2] += 15;

        //    series1.LabelForeColor = System.Drawing.Color.White;

        //    Series series2 = new Series
        //    {
        //        ChartType = SeriesChartType.StackedColumn100,
        //        Name = "數據集 2",
        //        Color = System.Drawing.Color.Red
        //    };
        //    stackedChart.Series.Add(series2);

        //    series2.Points.AddXY("分類 A", 5);
        //    floats[0] += 5;
        //    series2.Points.AddXY("分類 B", 10);
        //    floats[1] += 10;
        //    series2.Points.AddXY("分類 C", 20);
        //    floats[2] += 20;

        //    series2.LabelForeColor = System.Drawing.Color.White;

        //    Series series3 = new Series
        //    {
        //        ChartType = SeriesChartType.StackedColumn100,
        //        Name = "數據 3",
        //        Color = System.Drawing.Color.Green
        //    };
        //    stackedChart.Series.Add(series3);

        //    series3.Points.AddXY("分類 A", 15);
        //    floats[0] += 15;
        //    series3.Points.AddXY("分類 B", 5);
        //    floats[1] += 5;
        //    series3.Points.AddXY("分類 C", 10);
        //    floats[2] += 10;

        //    series3.LabelForeColor = System.Drawing.Color.White;

        //    foreach (var series in stackedChart.Series)
        //    {
        //        for (int i = 0; i < series.Points.Count; i++)
        //        {
        //            double percentage = series.Points[i].YValues[0] / floats[i];
        //            series.Points[i].Label = percentage.ToString("P");
        //        }
        //    }

        //    Legend legend = new Legend
        //    {
        //        Docking = Docking.Top
        //    };
        //    stackedChart.Legends.Add(legend);
        //    return stackedChart;
        //}

        Dictionary<string, Func<List<GroupByAmount>, Chart>> chartDictionary = new Dictionary<string, Func<List<GroupByAmount>, Chart>>();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(chartDictionary[comboBox1.Text](_groupByAmounts));
        }

        private void Button_Select_Click(object sender, EventArgs e)
        {
            this.SetDebounceTime(Start, 200);
        }

        private void Start()
        {
            this.Invoke(new Action(() =>
            {
                SearchDate searchDate = new SearchDate
                {
                    StartTime = DateTimePicker_Start.Value,
                    EndTime = DateTimePicker_End.Value
                };
                //if (ExpenseData.OrderBys.Values.Any(x => x))
                //{

                //    if (comboBox1.Text == "Line")
                //        _accountingDataPresenter.GetTwoGroupByAmounts(searchDate, purpose, companions, payments, ExpenseData.OrderBys);
                //    else
                //        _accountingDataPresenter.GetGroupByAmounts(searchDate, purpose, companions, payments, ExpenseData.OrderBys);
                //}
                //else
                //    _accountingDataPresenter.GetAccountingInfos(searchDate, purpose, companions, payments);
            }));
        }

        List<Expression<Func<AccountingInfo, bool>>> conditions = new List<Expression<Func<AccountingInfo, bool>>>();

        private void CheckBox_AllCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel panel = (FlowLayoutPanel)checkBox.Parent;
            foreach (var item in panel.Controls.OfType<CheckBox>())
                item.Checked = checkBox.Checked;
        }

        List<string> purpose = new List<string>();

        private void CheckBox_PurposeCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
                purpose.Add(checkBox.Text);
            else
                purpose.Remove(checkBox.Text);
        }

        List<string> companions = new List<string>();
        List<string> payments = new List<string>();

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                if (checkBox.Tag.ToString() == "對象")
                    companions.Add(checkBox.Text);
                if (checkBox.Tag.ToString() == "付款方式")
                    payments.Add(checkBox.Text);
            }
            else
            {
                if (checkBox.Tag.ToString() == "對象")
                    companions.Remove(checkBox.Text);
                if (checkBox.Tag.ToString() == "付款方式")
                    payments.Remove(checkBox.Text);
            }
        }

        private void CheckBoxOrderby_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string text = checkBox.Text;
            ExpenseData.OrderBys[text] = checkBox.Checked;
        }

        void IAccountingDataView.RenderAccountingInfos(List<AccountingInfo> AccountingInfos)
        {

        }

        void IAccountingDataView.RenderGroupByAmounts(List<GroupByAmount> groupByAmounts)
        {
            _groupByAmounts = groupByAmounts;

            panel1.Controls.Clear();
            panel1.Controls.Add(chartDictionary[comboBox1.Text](_groupByAmounts));
            DataGridView_AccountingInfo.Init();
            if (groupByAmounts == null)
                return;
            DataGridView_AccountingInfo.DataSource = groupByAmounts;
        }
        Random random = new Random();
        Color GetRandomColor()
        {

            int r = random.Next(0, 256); // 0 到 255 的隨機值
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);

            return Color.FromArgb(r, g, b);
        }
    }
}
