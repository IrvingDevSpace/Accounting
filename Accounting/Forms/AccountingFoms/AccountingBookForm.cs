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
using System.Windows.Forms;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("記帳本")]
    public partial class AccountingBookForm : Form, IAccountingDataView
    {
        private Navbar navbar;
        private IAccountingDataPresenter _accountingDataPresenter = null;

        public AccountingBookForm()
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

            }));
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
            //    DataGridView dataGridView = sender as DataGridView;
            //    if (e.RowIndex < 0 || e.ColumnIndex < 0)
            //        return;
            //    if (!(dataGridView.DataSource is List<AccountingInfo> AccountingInfos))
            //        return;
            //    string columnName = dataGridView.Columns[e.ColumnIndex].Name;
            //    if (columnName == "comboBoxColumnType")
            //    {
            //        if (dataGridView.Rows[e.RowIndex].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell comboBoxPurposeCell)
            //        {
            //            comboBoxPurposeCell.Value = null;
            //            comboBoxPurposeCell.Items.Clear();
            //            if (ExpenseData.Types.TryGetValue(dataGridView.Rows[e.RowIndex].Cells[columnName].Value.ToString(), out List<string> values))
            //            {
            //                comboBoxPurposeCell.Items.Clear();
            //                foreach (var value in values)
            //                    comboBoxPurposeCell.Items.Add(value);
            //                comboBoxPurposeCell.Value = comboBoxPurposeCell.Items[0];
            //                dataGridView.Rows[e.RowIndex].Cells["Purpose"].Value = comboBoxPurposeCell.Value;
            //            }
            //        }
            //    }

            //    PropertyInfo[] propertyInfos = typeof(AccountingInfo).GetProperties();
            //    for (int i = 0; i < propertyInfos.Length; i++)
            //    {
            //        String description = propertyInfos[i].GetCustomAttribute<DescriptionAttribute>()?.Description;
            //        if (description != null)
            //        {
            //            if (description == columnName)
            //            {
            //                dataGridView.Rows[e.RowIndex].Cells[propertyInfos[i].Name].Value = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //                break;
            //            }
            //        }
            //    }

            //    String time = dataGridView.Rows[e.RowIndex].Cells["Time"].Value.ToString();
            //    AccountingInfos = AccountingInfos.Where(x => x.Time == time).ToList();
            //    String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
            //    if (!Directory.Exists(directoryPath))
            //        Directory.CreateDirectory(directoryPath);
            //    if (File.Exists($"{directoryPath}\\Data.csv"))
            //        File.Delete($"{directoryPath}\\Data.csv");
            //    CSVHelper.Write($"{directoryPath}\\Data.csv", AccountingInfos);
            //}

            //void IAccountingDataView.RenderAccountingInfos(List<AccountingInfo> AccountingInfos)
            //{
            //    DataGridView_AccountingInfo.Init();
            //    if (AccountingInfos == null)
            //        return;
            //    DataGridView_AccountingInfo.DataSource = AccountingInfos;
            //    DataGridView_AccountingInfo.Columns["Time"].ReadOnly = true;
            //    DataGridView_AccountingInfo.Columns["Type"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["Purpose"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["Companion"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["Payment"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["ImagePath1"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["ImagePath2"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["ImagePathCompression1"].Visible = false;
            //    DataGridView_AccountingInfo.Columns["ImagePathCompression2"].Visible = false;

            //    DataGridViewComboBoxColumn comboBoxColType = new DataGridViewComboBoxColumn();
            //    comboBoxColType.Name = "comboBoxColumnType";
            //    comboBoxColType.HeaderText = "類型";
            //    comboBoxColType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    DataGridView_AccountingInfo.Columns.Add(comboBoxColType);

            //    DataGridViewComboBoxColumn comboBoxColPurpose = new DataGridViewComboBoxColumn();
            //    comboBoxColPurpose.Name = "comboBoxColumnPurpose";
            //    comboBoxColPurpose.HeaderText = "目的";
            //    comboBoxColPurpose.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    DataGridView_AccountingInfo.Columns.Add(comboBoxColPurpose);

            //    DataGridViewComboBoxColumn comboBoxColCompanion = new DataGridViewComboBoxColumn();
            //    comboBoxColCompanion.Name = "comboBoxColumnCompanion";
            //    comboBoxColCompanion.HeaderText = "對象";
            //    comboBoxColCompanion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    DataGridView_AccountingInfo.Columns.Add(comboBoxColCompanion);

            //    DataGridViewComboBoxColumn comboBoxColPayment = new DataGridViewComboBoxColumn();
            //    comboBoxColPayment.Name = "comboBoxColumnPayment";
            //    comboBoxColPayment.HeaderText = "付款方式";
            //    comboBoxColPayment.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    DataGridView_AccountingInfo.Columns.Add(comboBoxColPayment);

            //    DataGridViewImageColumn imgCol1 = new DataGridViewImageColumn();
            //    imgCol1.Name = "ImageColumnPath1";
            //    imgCol1.HeaderText = "發票圖檔1";
            //    imgCol1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            //    imgCol1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //    imgCol1.ReadOnly = true;
            //    DataGridView_AccountingInfo.Columns.Add(imgCol1);

            //    DataGridViewImageColumn imgCol2 = new DataGridViewImageColumn();
            //    imgCol2.Name = "ImageColumnPath2";
            //    imgCol2.HeaderText = "發票圖檔2";
            //    imgCol2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            //    imgCol2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //    imgCol2.ReadOnly = true;
            //    DataGridView_AccountingInfo.Columns.Add(imgCol2);

            //    DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            //    btnCol.Name = "Delete";
            //    btnCol.HeaderText = "刪除";
            //    btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            //    DataGridView_AccountingInfo.Columns.Add(btnCol);

            //    for (int i = 0; i < AccountingInfos.Count; i++)
            //    {
            //        if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnType"] is DataGridViewComboBoxCell comboBoxTypeCell)
            //        {
            //            comboBoxTypeCell.Items.Clear();
            //            foreach (var type in ExpenseData.Types.Keys)
            //                comboBoxTypeCell.Items.Add(type);
            //            comboBoxTypeCell.Value = AccountingInfos[i].Type;
            //        }
            //        if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell comboBoxPurposeCell)
            //        {
            //            comboBoxPurposeCell.Items.Clear();
            //            if (ExpenseData.Types.TryGetValue(AccountingInfos[i].Type, out List<string> values))
            //            {
            //                comboBoxPurposeCell.Items.Clear();
            //                foreach (var value in values)
            //                    comboBoxPurposeCell.Items.Add(value);
            //                comboBoxPurposeCell.Value = AccountingInfos[i].Purpose;
            //            }
            //        }
            //        if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnCompanion"] is DataGridViewComboBoxCell comboBoxCompanionCell)
            //        {
            //            comboBoxCompanionCell.Items.Clear();
            //            foreach (var companion in ExpenseData.Companions)
            //                comboBoxCompanionCell.Items.Add(companion);
            //            comboBoxCompanionCell.Value = AccountingInfos[i].Companion;
            //        }
            //        if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnPayment"] is DataGridViewComboBoxCell comboBoxPaymentCell)
            //        {
            //            comboBoxPaymentCell.Items.Clear();
            //            foreach (var payment in ExpenseData.Payments)
            //                comboBoxPaymentCell.Items.Add(payment);
            //            comboBoxPaymentCell.Value = AccountingInfos[i].Payment;
            //        }
            //        if (File.Exists(AccountingInfos[i].ImagePath1))
            //        {
            //            Image img = Image.FromFile(AccountingInfos[i].ImagePath1);
            //            DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Value = img;
            //            DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Tag = AccountingInfos[i].ImagePathCompression1;
            //        }
            //        else
            //            DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Value = null;

            //        if (File.Exists(AccountingInfos[i].ImagePath2))
            //        {
            //            Image img = Image.FromFile(AccountingInfos[i].ImagePath2);
            //            DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Value = img;
            //            DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Tag = AccountingInfos[i].ImagePathCompression2;
            //        }
            //        else
            //            DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Value = null;
            //        if (DataGridView_AccountingInfo.Rows[i].Cells["Delete"] is DataGridViewButtonCell buttonDeleteCell)
            //            buttonDeleteCell.Value = "刪除";
            //    }
            //    DataGridView_AccountingInfo.ClearSelection();
            //    Console.WriteLine(1);
        }

        void IAccountingDataView.RenderGroupByAmounts(List<GroupByAmount> groupByAmounts)
        {
            throw new NotImplementedException();
        }

        public void RenderAccountingInfos(List<AccountingInfo> AccountingInfos)
        {
            throw new NotImplementedException();
        }
    }
}
