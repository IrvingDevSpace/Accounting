using Accounting.Components;
using Accounting.Model;
using Accounting.SingletonUtils;
using CSV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Accounting.Forms
{
    [Navbar("記帳本")]
    public partial class AccountingBookForm : Form
    {
        private Navbar navbar;

        public AccountingBookForm()
        {
            InitializeComponent();
            this.Text = this.GetFormTitle();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddEvent();
        }

        private void AddEvent()
        {
            this.Load += AccountingBookForm_Load;
            this.FormClosed += AccountingBookForm_FormClosed;
        }

        private void AccountingBookForm_Load(object sender, System.EventArgs e)
        {
            navbar = new Navbar
            {
                Location = new System.Drawing.Point(43, 361),
                Name = "Navbar_ChangeForm",
                Size = new System.Drawing.Size(550, 70)
            };
            this.Controls.Add(navbar);
            this.SetFormsNavbarButton();
        }

        private void AccountingBookForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
            int diffDays = (DateTimePicker_End.Value - DateTimePicker_Start.Value).Days + 1;
            if (diffDays < 1)
                return;
            List<String> directories = Directory.GetDirectories(fileServerPath).ToList();
            List<String> containDirectories = new List<String>();
            List<AddAccountingInfo> addAccountingInfos = new List<AddAccountingInfo>();
            for (int i = 0; i < diffDays; i++)
            {
                String directoryName = $"{fileServerPath}{DateTimePicker_Start.Value.AddDays(i).ToString("yyyy-MM-dd")}";
                foreach (var directory in directories)
                {
                    if (directory == directoryName)
                    {
                        addAccountingInfos.AddRange(CSVHelper.Read<AddAccountingInfo>($"{directoryName}\\Data.csv"));
                        break;
                    }
                }
            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;

            GC.Collect();
            dataGridView1.DataSource = addAccountingInfos;
            dataGridView1.Columns["ImagePath1"].Visible = false;
            dataGridView1.Columns["ImagePath2"].Visible = false;
            dataGridView1.Columns["ImagePathCompression1"].Visible = false;
            dataGridView1.Columns["ImagePathCompression2"].Visible = false;

            DataGridViewImageColumn imgCol1 = new DataGridViewImageColumn();
            imgCol1.Name = "ImageColumnPath1";
            imgCol1.HeaderText = "發票圖檔1";
            imgCol1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            imgCol1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(imgCol1);
            DataGridViewImageColumn imgCol2 = new DataGridViewImageColumn();
            imgCol2.Name = "ImageColumnPath2";
            imgCol2.HeaderText = "發票圖檔2";
            imgCol2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            imgCol2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(imgCol2);

            for (int i = 0; i < addAccountingInfos.Count; i++)
            {
                if (File.Exists(addAccountingInfos[i].ImagePath1))
                {
                    Image img = Image.FromFile(addAccountingInfos[i].ImagePath1);
                    dataGridView1.Rows[i].Cells["ImageColumnPath1"].Value = img;
                    dataGridView1.Rows[i].Cells["ImageColumnPath1"].Tag = addAccountingInfos[i].ImagePathCompression1;
                }
                else
                    dataGridView1.Rows[i].Cells["ImageColumnPath1"].Value = null;

                if (File.Exists(addAccountingInfos[i].ImagePath2))
                {
                    Image img = Image.FromFile(addAccountingInfos[i].ImagePath2);
                    dataGridView1.Rows[i].Cells["ImageColumnPath2"].Value = img;
                    dataGridView1.Rows[i].Cells["ImageColumnPath2"].Tag = addAccountingInfos[i].ImagePathCompression2;
                }
                else
                    dataGridView1.Rows[i].Cells["ImageColumnPath2"].Value = null;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            String filePath = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
            if (String.IsNullOrEmpty(filePath))
                return;
            if (!File.Exists(filePath))
                return;
            ShowImgForm showImgForm = new ShowImgForm(filePath);
            showImgForm.ShowDialog();
        }
    }
}
