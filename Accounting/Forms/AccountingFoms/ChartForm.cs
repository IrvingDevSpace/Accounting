using Accounting.Components;
using Accounting.Extension;
using Accounting.Models;
using Accounting.SingletonUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("圖表分析")]
    public partial class ChartForm : Form
    {
        private Navbar navbar;

        public ChartForm()
        {
            InitializeComponent();
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
                Location = new System.Drawing.Point(43, 361),
                Name = "Navbar_ChangeForm",
                Size = new System.Drawing.Size(550, 70)
            };
            this.Controls.Add(navbar);
            this.SetFormsNavbarButton();

            List<ChartParam> chartParams = new List<ChartParam>();
            ChartParam chartParam = new ChartParam
            {
                Name = "Test",
                LegendName = "Legend",
                ChartType = SeriesChartType.Pie,
                ChartDashStyle = ChartDashStyle.NotSet,
                BorderWidth = 1,
                BorderColor = Color.Black,
                XValueType = ChartValueType.Auto,
                YValueType = ChartValueType.Auto,
            };
            chartParams.Add(chartParam);
            chart1.ChartCreate(chartParams, "TestChart", new Font("Microsoft JhengHei", 8, FontStyle.Bold), "Time", "Temp (℃)", Double.NaN, Double.NaN);
            chart1.Series["Test"].Points.AddXY(1, 2);
        }

        private void ChartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
