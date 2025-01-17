﻿using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting.Models
{
    internal class ChartParam
    {
        public String Name { get; set; }
        public String LegendName { get; set; }
        public SeriesChartType ChartType { get; set; }
        public ChartDashStyle ChartDashStyle { get; set; }
        public int BorderWidth { get; set; }
        public Color BorderColor { get; set; }
        public ChartValueType XValueType { get; set; }
        public ChartValueType YValueType { get; set; }
    }
}
