using System;
using System.Drawing;
using System.Windows.Forms;

namespace Accounting.Extension
{
    internal static class DataGridViewExtension
    {
        public static void Init(this DataGridView dataGridView)
        {
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
            GC.Collect();
            dataGridView.AllowDrop = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToOrderColumns = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.DefaultCellStyle.Font = new Font("微軟正黑體", (float)9, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", (float)9, FontStyle.Bold);
        }
    }
}
