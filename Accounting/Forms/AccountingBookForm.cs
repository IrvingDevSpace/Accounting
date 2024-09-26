using Accounting.Components;
using Accounting.Model;
using Accounting.SingletonUtils;
using CSV;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            List<AddAccountingInfo> addAccountingInfos = CSVHelper.Read<AddAccountingInfo>($"{fileServerPath}Data.csv");
            dataGridView1.DataSource = addAccountingInfos;
            dataGridView1.Columns["ImagePath1"].Visible = false;
            dataGridView1.Columns["ImagePath2"].Visible = false;

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
                if (System.IO.File.Exists(addAccountingInfos[i].ImagePath1))
                {
                    Image img = Image.FromFile(addAccountingInfos[i].ImagePath1);
                    dataGridView1.Rows[i].Cells["ImageColumnPath1"].Value = img;
                }
                else
                    dataGridView1.Rows[i].Cells["ImageColumnPath1"].Value = null;

                if (System.IO.File.Exists(addAccountingInfos[i].ImagePath2))
                {
                    Image img = Image.FromFile(addAccountingInfos[i].ImagePath2);
                    dataGridView1.Rows[i].Cells["ImageColumnPath2"].Value = img;
                }
                else
                    dataGridView1.Rows[i].Cells["ImageColumnPath2"].Value = null;
            }
        }


        // 準備10張4K壁紙 20M 越清楚越好
    }
}
