using Accounting.Components;
using Accounting.Extension;
using Accounting.Models;
using Accounting.Presenter;
using Accounting.SingletonUtils;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Accounting.Contract.AccountingDataContract;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            CreateCheckBoxes(FLP_Where);
            CreateCheckBoxes2(FLP_OrderBy);
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
                if (SelectItemInfo.OrderBys.Values.Any(x => x))
                {

                    if (comboBox1.Text == "Line")
                        _accountingDataPresenter.GetTwoGroupByAmounts(searchDate, purpose, companions, payments, SelectItemInfo.OrderBys);
                    else
                        _accountingDataPresenter.GetGroupByAmounts(searchDate, purpose, companions, payments, SelectItemInfo.OrderBys);
                }
                else
                    _accountingDataPresenter.GetAddAccountingInfos(searchDate, purpose, companions, payments);
            }));
        }

        private void CreateCheckBoxes(FlowLayoutPanel f)
        {

            foreach (var type in SelectItemInfo.Types)
            {
                List<CheckBox> checkBoxes = new List<CheckBox>();
                FlowLayoutPanel panel = new FlowLayoutPanel { Size = new Size(300, 21) };
                CheckBox keyCheckBox = new CheckBox { Text = type.Key, AutoSize = true };
                keyCheckBox.CheckedChanged += CheckBox_AllCheckedChanged;
                checkBoxes.Add(keyCheckBox);
                foreach (var item in type.Value)
                {
                    CheckBox valCheckBox = new CheckBox { Text = item, AutoSize = true, Tag = type.Key };
                    valCheckBox.CheckedChanged += CheckBox_PurposeCheckedChanged;
                    checkBoxes.Add(valCheckBox);
                }
                panel.Controls.AddRange(checkBoxes.ToArray());
                f.Controls.Add(panel);
            }

            List<CheckBox> checkBoxes1 = new List<CheckBox>();
            CheckBox c1 = new CheckBox { Text = "對象", AutoSize = true, Tag = "對象" };
            c1.CheckedChanged += CheckBox_AllCheckedChanged;
            checkBoxes1.Add(c1);
            foreach (var companion in SelectItemInfo.Companions)
            {
                CheckBox checkBox = new CheckBox { Text = companion, AutoSize = true, Tag = "對象" };
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBoxes1.Add(checkBox);
            }
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel { Size = new Size(300, 21) };
            flowLayoutPanel.Controls.AddRange(checkBoxes1.ToArray());
            f.Controls.Add(flowLayoutPanel);

            List<CheckBox> checkBoxes2 = new List<CheckBox>();
            CheckBox c2 = new CheckBox { Text = "付款方式", AutoSize = true, Tag = "付款方式" };
            c2.CheckedChanged += CheckBox_AllCheckedChanged;
            checkBoxes2.Add(c2);
            foreach (var payment in SelectItemInfo.Payments)
            {
                CheckBox checkBox = new CheckBox { Text = payment, AutoSize = true, Tag = "付款方式" };
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBoxes2.Add(checkBox);
            }
            flowLayoutPanel = new FlowLayoutPanel { Size = new Size(300, 21) };
            flowLayoutPanel.Controls.AddRange(checkBoxes2.ToArray());
            f.Controls.Add(flowLayoutPanel);
        }

        private void CreateCheckBoxes2(FlowLayoutPanel f)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (var orderBy in SelectItemInfo.OrderBys)
            {
                checkBoxes = new List<CheckBox>();
                FlowLayoutPanel panel = new FlowLayoutPanel { Size = new Size(300, 21) };
                CheckBox keyCheckBox = new CheckBox { Text = orderBy.Key, AutoSize = true };
                keyCheckBox.CheckedChanged += CheckBoxOrderby_CheckedChanged;
                checkBoxes.Add(keyCheckBox);
                panel.Controls.AddRange(checkBoxes.ToArray());
                f.Controls.Add(panel);
            }
        }

        List<Expression<Func<AddAccountingInfo, bool>>> conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();

        private void CheckBox_AllCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel panel = (FlowLayoutPanel)checkBox.Parent;
            panel.Controls.OfType<CheckBox>().ForEach(x =>
            {
                x.Checked = checkBox.Checked;
            });
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
            SelectItemInfo.OrderBys[text] = checkBox.Checked;
        }

        void IAccountingDataView.RenderAddAccountingInfos(List<AddAccountingInfo> addAccountingInfos)
        {
            DataGridView_AccountingInfo.Init();
            if (addAccountingInfos == null)
                return;
            DataGridView_AccountingInfo.DataSource = addAccountingInfos;
            DataGridView_AccountingInfo.Columns["Time"].ReadOnly = true;
            DataGridView_AccountingInfo.Columns["Type"].Visible = false;
            DataGridView_AccountingInfo.Columns["Purpose"].Visible = false;
            DataGridView_AccountingInfo.Columns["Companion"].Visible = false;
            DataGridView_AccountingInfo.Columns["Payment"].Visible = false;
            DataGridView_AccountingInfo.Columns["ImagePath1"].Visible = false;
            DataGridView_AccountingInfo.Columns["ImagePath2"].Visible = false;
            DataGridView_AccountingInfo.Columns["ImagePathCompression1"].Visible = false;
            DataGridView_AccountingInfo.Columns["ImagePathCompression2"].Visible = false;

            DataGridViewComboBoxColumn comboBoxColType = new DataGridViewComboBoxColumn();
            comboBoxColType.Name = "comboBoxColumnType";
            comboBoxColType.HeaderText = "類型";
            comboBoxColType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_AccountingInfo.Columns.Add(comboBoxColType);

            DataGridViewComboBoxColumn comboBoxColPurpose = new DataGridViewComboBoxColumn();
            comboBoxColPurpose.Name = "comboBoxColumnPurpose";
            comboBoxColPurpose.HeaderText = "目的";
            comboBoxColPurpose.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_AccountingInfo.Columns.Add(comboBoxColPurpose);

            DataGridViewComboBoxColumn comboBoxColCompanion = new DataGridViewComboBoxColumn();
            comboBoxColCompanion.Name = "comboBoxColumnCompanion";
            comboBoxColCompanion.HeaderText = "對象";
            comboBoxColCompanion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_AccountingInfo.Columns.Add(comboBoxColCompanion);

            DataGridViewComboBoxColumn comboBoxColPayment = new DataGridViewComboBoxColumn();
            comboBoxColPayment.Name = "comboBoxColumnPayment";
            comboBoxColPayment.HeaderText = "付款方式";
            comboBoxColPayment.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_AccountingInfo.Columns.Add(comboBoxColPayment);

            DataGridViewImageColumn imgCol1 = new DataGridViewImageColumn();
            imgCol1.Name = "ImageColumnPath1";
            imgCol1.HeaderText = "發票圖檔1";
            imgCol1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            imgCol1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imgCol1.ReadOnly = true;
            DataGridView_AccountingInfo.Columns.Add(imgCol1);

            DataGridViewImageColumn imgCol2 = new DataGridViewImageColumn();
            imgCol2.Name = "ImageColumnPath2";
            imgCol2.HeaderText = "發票圖檔2";
            imgCol2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            imgCol2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imgCol2.ReadOnly = true;
            DataGridView_AccountingInfo.Columns.Add(imgCol2);

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "Delete";
            btnCol.HeaderText = "刪除";
            btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            DataGridView_AccountingInfo.Columns.Add(btnCol);

            for (int i = 0; i < addAccountingInfos.Count; i++)
            {
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnType"] is DataGridViewComboBoxCell comboBoxTypeCell)
                {
                    comboBoxTypeCell.Items.Clear();
                    foreach (var type in SelectItemInfo.Types.Keys)
                        comboBoxTypeCell.Items.Add(type);
                    comboBoxTypeCell.Value = addAccountingInfos[i].Type;
                }
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell comboBoxPurposeCell)
                {
                    comboBoxPurposeCell.Items.Clear();
                    if (SelectItemInfo.Types.TryGetValue(addAccountingInfos[i].Type, out List<string> values))
                    {
                        comboBoxPurposeCell.Items.Clear();
                        foreach (var value in values)
                            comboBoxPurposeCell.Items.Add(value);
                        comboBoxPurposeCell.Value = addAccountingInfos[i].Purpose;
                    }
                }
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnCompanion"] is DataGridViewComboBoxCell comboBoxCompanionCell)
                {
                    comboBoxCompanionCell.Items.Clear();
                    foreach (var companion in SelectItemInfo.Companions)
                        comboBoxCompanionCell.Items.Add(companion);
                    comboBoxCompanionCell.Value = addAccountingInfos[i].Companion;
                }
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnPayment"] is DataGridViewComboBoxCell comboBoxPaymentCell)
                {
                    comboBoxPaymentCell.Items.Clear();
                    foreach (var payment in SelectItemInfo.Payments)
                        comboBoxPaymentCell.Items.Add(payment);
                    comboBoxPaymentCell.Value = addAccountingInfos[i].Payment;
                }
                if (File.Exists(addAccountingInfos[i].ImagePath1))
                {
                    Image img = Image.FromFile(addAccountingInfos[i].ImagePath1);
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Value = img;
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Tag = addAccountingInfos[i].ImagePathCompression1;
                }
                else
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Value = null;

                if (File.Exists(addAccountingInfos[i].ImagePath2))
                {
                    Image img = Image.FromFile(addAccountingInfos[i].ImagePath2);
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Value = img;
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Tag = addAccountingInfos[i].ImagePathCompression2;
                }
                else
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Value = null;
                if (DataGridView_AccountingInfo.Rows[i].Cells["Delete"] is DataGridViewButtonCell buttonDeleteCell)
                    buttonDeleteCell.Value = "刪除";
            }
            DataGridView_AccountingInfo.ClearSelection();
            Console.WriteLine(1);
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
