using Accounting.Attributes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Accounting.Models
{
    public class AccountingInfo
    {
        [DisplayName("時間")]
        [GridColumn(ReadOnly = true)]
        public String Time { get; set; }

        [GridColumn(ReadOnly = false)]
        [DisplayName("金額")]
        public String Amount { get; set; }

        [DisplayName("類型")]
        [GridColumn(ColumnType = typeof(DataGridViewComboBoxColumn), ColumnName = "comboBoxColumnType", ChildColumnName = "comboBoxColumnPurpose", Visible = false)]
        public String Type { get; set; }

        [DisplayName("目的")]
        [GridColumn(ColumnType = typeof(DataGridViewComboBoxColumn), ColumnName = "comboBoxColumnPurpose", Visible = false)]
        public String Purpose { get; set; }

        [DisplayName("對象")]
        [GridColumn(ColumnType = typeof(DataGridViewComboBoxColumn), ColumnName = "comboBoxColumnCompanion", Visible = false)]
        public String Companion { get; set; }

        [DisplayName("付款方式")]
        [GridColumn(ColumnType = typeof(DataGridViewComboBoxColumn), ColumnName = "comboBoxColumnPayment", Visible = false)]
        public String Payment { get; set; }

        [DisplayName("發票圖檔1")]
        [GridColumn(ColumnType = typeof(DataGridViewImageColumn), ColumnName = "imageColumnPath1", ImagePathCompression = "ImagePathCompression1", Visible = false)]
        public String ImagePath1 { get; set; }

        [DisplayName("發票圖檔2")]
        [GridColumn(ColumnType = typeof(DataGridViewImageColumn), ColumnName = "imageColumnPath2", ImagePathCompression = "ImagePathCompression2", Visible = false)]
        public String ImagePath2 { get; set; }

        [GridColumn(Visible = false)]
        public String ImagePathCompression1 { get; set; }

        [GridColumn(Visible = false)]
        public String ImagePathCompression2 { get; set; }
    }
}
