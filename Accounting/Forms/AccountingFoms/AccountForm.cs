using Accounting.Components;
using Accounting.Extension;
using Accounting.Models;
using Accounting.Presenter;
using Accounting.SingletonUtils;
using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            CreateCheckBoxes(FLP_Where);
            CreateCheckBoxes2(FLP_OrderBy);
        }

        private void CreateCheckBoxes(FlowLayoutPanel f)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (var type in SelectItemInfo.Types)
            {
                checkBoxes = new List<CheckBox>();
                FlowLayoutPanel panel = new FlowLayoutPanel { Size = new Size(300, 21) };
                CheckBox keyCheckBox = new CheckBox { Text = type.Key, AutoSize = true, Tag = type.Key };
                keyCheckBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBoxes.Add(keyCheckBox);
                foreach (var item in type.Value)
                {
                    CheckBox valCheckBox = new CheckBox { Text = item, AutoSize = true, Tag = type.Key };
                    valCheckBox.CheckedChanged += CheckBox_CheckedChanged;
                    checkBoxes.Add(valCheckBox);
                }
                panel.Controls.AddRange(checkBoxes.ToArray());
                f.Controls.Add(panel);
            }
            checkBoxes = new List<CheckBox>();
            CheckBox c1 = new CheckBox { Text = "對象", AutoSize = true, Tag = "對象" };
            c1.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxes.Add(c1);
            foreach (var companion in SelectItemInfo.Companions)
            {
                CheckBox checkBox = new CheckBox { Text = companion, AutoSize = true, Tag = "對象" };
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBoxes.Add(checkBox);
            }
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel { Size = new Size(300, 21) };
            flowLayoutPanel.Controls.AddRange(checkBoxes.ToArray());
            f.Controls.Add(flowLayoutPanel);
            checkBoxes = new List<CheckBox>();
            CheckBox c2 = new CheckBox { Text = "付款方式", AutoSize = true, Tag = "付款方式" };
            c2.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxes.Add(c2);
            foreach (var payment in SelectItemInfo.Payments)
            {
                CheckBox checkBox = new CheckBox { Text = payment, AutoSize = true, Tag = "付款方式" };
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBoxes.Add(checkBox);
            }
            flowLayoutPanel = new FlowLayoutPanel { Size = new Size(300, 21) };
            flowLayoutPanel.Controls.AddRange(checkBoxes.ToArray());
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
                List<AddAccountingInfo> addAccountingInfos = dataGridView.DataSource as List<AddAccountingInfo>;
                dataGridView.Init();
                addAccountingInfos.RemoveAt(e.RowIndex);
                addAccountingInfos = addAccountingInfos.Where(x => x.Time == time).ToList();
                String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                if (File.Exists($"{directoryPath}\\Data.csv"))
                    File.Delete($"{directoryPath}\\Data.csv");
                CSVHelper.Write($"{directoryPath}\\Data.csv", addAccountingInfos);
                Button_Select.PerformClick();
            }
        }

        private void DataGridView_AccountingInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (!(dataGridView.DataSource is List<AddAccountingInfo> addAccountingInfos))
                return;
            string columnName = dataGridView.Columns[e.ColumnIndex].Name;
            if (columnName == "comboBoxColumnType")
            {
                if (dataGridView.Rows[e.RowIndex].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell comboBoxPurposeCell)
                {
                    comboBoxPurposeCell.Value = null;
                    comboBoxPurposeCell.Items.Clear();
                    if (SelectItemInfo.Types.TryGetValue(dataGridView.Rows[e.RowIndex].Cells[columnName].Value.ToString(), out List<string> values))
                    {
                        comboBoxPurposeCell.Items.Clear();
                        foreach (var value in values)
                            comboBoxPurposeCell.Items.Add(value);
                        comboBoxPurposeCell.Value = comboBoxPurposeCell.Items[0];
                        dataGridView.Rows[e.RowIndex].Cells["Purpose"].Value = comboBoxPurposeCell.Value;
                    }
                }
            }

            PropertyInfo[] propertyInfos = typeof(AddAccountingInfo).GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                String description = propertyInfos[i].GetCustomAttribute<DescriptionAttribute>()?.Description;
                if (description != null)
                {
                    if (description == columnName)
                    {
                        dataGridView.Rows[e.RowIndex].Cells[propertyInfos[i].Name].Value = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        break;
                    }
                }
            }

            String time = dataGridView.Rows[e.RowIndex].Cells["Time"].Value.ToString();
            addAccountingInfos = addAccountingInfos.Where(x => x.Time == time).ToList();
            String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            if (File.Exists($"{directoryPath}\\Data.csv"))
                File.Delete($"{directoryPath}\\Data.csv");
            CSVHelper.Write($"{directoryPath}\\Data.csv", addAccountingInfos);
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
                //if (SelectItemInfo.OrderBys.Values.Any(x => x))
                //    _accountingDataPresenter.GetGroupByAmounts(searchDate, SelectItemInfo.OrderBys);
                //else
                _accountingDataPresenter.GetAddAccountingInfos(searchDate);
            }));
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

        List<Expression<Func<AddAccountingInfo, bool>>> conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string text = checkBox.Text;
            if (checkBox.Checked)
            {
                if (checkBox.Tag.ToString() == "交通")
                    SelectItemInfo.A_Conditions.Add(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "飲食")
                    SelectItemInfo.B_Conditions.Add(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "娛樂")
                    SelectItemInfo.C_Conditions.Add(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "對象")
                    SelectItemInfo.D_Conditions.Add(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "付款方式")
                    SelectItemInfo.E_Conditions.Add(SelectItemInfo.Funcs[text]);
            }
            else
            {
                if (checkBox.Tag.ToString() == "交通")
                    SelectItemInfo.A_Conditions.Remove(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "飲食")
                    SelectItemInfo.B_Conditions.Remove(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "娛樂")
                    SelectItemInfo.C_Conditions.Remove(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "對象")
                    SelectItemInfo.D_Conditions.Remove(SelectItemInfo.Funcs[text]);
                if (checkBox.Tag.ToString() == "付款方式")
                    SelectItemInfo.E_Conditions.Remove(SelectItemInfo.Funcs[text]);
            }
        }

        private void CheckBoxOrderby_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string text = checkBox.Text;
            SelectItemInfo.OrderBys[text] = checkBox.Checked;
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
