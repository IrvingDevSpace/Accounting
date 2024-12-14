using Accounting.Attributes;
using Accounting.Components;
using Accounting.Extension;
using Accounting.Models;
using Accounting.Presenter;
using CSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("帳戶")]
    public partial class AccountForm : Form, IAccountingDataView
    {
        private Navbar navbar;
        private IAccountingDataPresenter _accountingDataPresenter = null;

        public AccountForm()
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
            this.Load += AccountForm_Load;
            this.FormClosed += AccountForm_FormClosed;
        }

        private void AccountForm_Load(object sender, System.EventArgs e)
        {
            navbar = new Navbar
            {
                Location = new System.Drawing.Point(623, 363),
                Name = "Navbar_ChangeForm",
                Size = new System.Drawing.Size(550, 70)
            };
            this.Controls.Add(navbar);
            this.SetFormsNavbarButton();
        }

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Select_Click(object sender, EventArgs e)
        {
            this.SetDebounceTime(Start, 200);
        }


        private void DataGridView_AccountingInfo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (!(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewImageCell))
                return;
            String filePath = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
            if (String.IsNullOrEmpty(filePath))
                return;
            if (!File.Exists(filePath))
                return;
            ShowImgForm showImgForm = new ShowImgForm(filePath);
            showImgForm.ShowDialog();
        }

        String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";

        private void DataGridView_AccountingInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell btn)
            {
                String time = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                List<AccountingInfo> AccountingInfos = dataGridView.DataSource as List<AccountingInfo>;
                dataGridView.Init();
                AccountingInfos.RemoveAt(e.RowIndex);
                AccountingInfos = AccountingInfos.Where(x => x.Time == time).ToList();
                String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                if (File.Exists($"{directoryPath}\\Data.csv"))
                    File.Delete($"{directoryPath}\\Data.csv");
                CSVHelper.Write($"{directoryPath}\\Data.csv", AccountingInfos);
                Button_Select.PerformClick();
            }
        }

        private void DataGridView_AccountingInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView dataGridView = sender as DataGridView;
            //if (e.RowIndex < 0 || e.ColumnIndex < 0)
            //    return;
            //if (!(dataGridView.DataSource is List<AccountingInfo> AccountingInfos))
            //    return;
            //string columnName = dataGridView.Columns[e.ColumnIndex].Name;
            //if (columnName == "comboBoxColumnType")
            //{
            //    if (dataGridView.Rows[e.RowIndex].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell comboBoxPurposeCell)
            //    {
            //        comboBoxPurposeCell.Value = null;
            //        comboBoxPurposeCell.Items.Clear();
            //        if (ExpenseData.Types.TryGetValue(dataGridView.Rows[e.RowIndex].Cells[columnName].Value.ToString(), out List<string> values))
            //        {
            //            comboBoxPurposeCell.Items.Clear();
            //            foreach (var value in values)
            //                comboBoxPurposeCell.Items.Add(value);
            //            comboBoxPurposeCell.Value = comboBoxPurposeCell.Items[0];
            //            dataGridView.Rows[e.RowIndex].Cells["Purpose"].Value = comboBoxPurposeCell.Value;
            //        }
            //    }
            //}

            //PropertyInfo[] propertyInfos = typeof(AccountingInfo).GetProperties();
            //for (int i = 0; i < propertyInfos.Length; i++)
            //{
            //    String description = propertyInfos[i].GetCustomAttribute<DescriptionAttribute>()?.Description;
            //    if (description != null)
            //    {
            //        if (description == columnName)
            //        {
            //            dataGridView.Rows[e.RowIndex].Cells[propertyInfos[i].Name].Value = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //            break;
            //        }
            //    }
            //}

            //String time = dataGridView.Rows[e.RowIndex].Cells["Time"].Value.ToString();
            //AccountingInfos = AccountingInfos.Where(x => x.Time == time).ToList();
            //String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
            //if (!Directory.Exists(directoryPath))
            //    Directory.CreateDirectory(directoryPath);
            //if (File.Exists($"{directoryPath}\\Data.csv"))
            //    File.Delete($"{directoryPath}\\Data.csv");
            //CSVHelper.Write($"{directoryPath}\\Data.csv", AccountingInfos);
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
            }));
        }

        void IAccountingDataView.RenderAccountingInfos(List<AccountingInfo> AccountingInfos)
        {

        }

        List<Expression<Func<AccountingInfo, bool>>> conditions = new List<Expression<Func<AccountingInfo, bool>>>();
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CheckBoxOrderby_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string text = checkBox.Text;
            ExpenseData.OrderBys[text] = checkBox.Checked;
        }

        void IAccountingDataView.RenderGroupByAmounts(List<GroupByAmount> groupByAmounts)
        {
            DataGridView_AccountingInfo.Init();
            if (groupByAmounts == null)
                return;
            DataGridView_AccountingInfo.DataSource = groupByAmounts;
        }
    }
}
