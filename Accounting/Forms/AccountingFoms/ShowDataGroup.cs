using Accounting.Attributes;
using Accounting.Extension;
using Accounting.Models;
using Accounting.Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("顯示")]
    public partial class ShowDataGroup : Form, IAccountingDataView
    {
        private IAccountingDataPresenter _accountingDataPresenter = null;

        public ShowDataGroup()
        {
            InitializeComponent();
            _accountingDataPresenter = new AccountingDataPresenter(this);
        }

        private void ShowDataGroup_Load(object sender, EventArgs e)
        {
            CreateCheckBoxes(FLP_Where);
            CreateCheckBoxes2(FLP_OrderBy);
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
                    StartTime = DateTimePicker_Start.Value.Date,
                    EndTime = DateTimePicker_End.Value.Date
                };
                if (ExpenseData.OrderBys.Values.Any(x => x))
                    _accountingDataPresenter.GetGroupByAmounts(searchDate, purpose, companions, payments, ExpenseData.OrderBys);
                else
                    _accountingDataPresenter.GetAccountingInfos(searchDate, purpose, companions, payments);
            }));
        }

        void IAccountingDataView.RenderAccountingInfos(List<AccountingInfo> accountingInfos)
        {
            DataGridView_AccountingInfo.Init();
            if (accountingInfos == null)
                return;
            DataGridView_AccountingInfo.DataSource = accountingInfos;
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

            for (int i = 0; i < accountingInfos.Count; i++)
            {
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnType"] is DataGridViewComboBoxCell comboBoxTypeCell)
                {
                    comboBoxTypeCell.Items.Clear();
                    foreach (var type in ExpenseData.Types.Keys)
                        comboBoxTypeCell.Items.Add(type);
                    comboBoxTypeCell.Value = accountingInfos[i].Type;
                }
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell comboBoxPurposeCell)
                {
                    comboBoxPurposeCell.Items.Clear();
                    if (ExpenseData.Types.TryGetValue(accountingInfos[i].Type, out List<string> values))
                    {
                        comboBoxPurposeCell.Items.Clear();
                        foreach (var value in values)
                            comboBoxPurposeCell.Items.Add(value);
                        comboBoxPurposeCell.Value = accountingInfos[i].Purpose;
                    }
                }
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnCompanion"] is DataGridViewComboBoxCell comboBoxCompanionCell)
                {
                    comboBoxCompanionCell.Items.Clear();
                    foreach (var companion in ExpenseData.Companions)
                        comboBoxCompanionCell.Items.Add(companion);
                    comboBoxCompanionCell.Value = accountingInfos[i].Companion;
                }
                if (DataGridView_AccountingInfo.Rows[i].Cells["comboBoxColumnPayment"] is DataGridViewComboBoxCell comboBoxPaymentCell)
                {
                    comboBoxPaymentCell.Items.Clear();
                    foreach (var payment in ExpenseData.Payments)
                        comboBoxPaymentCell.Items.Add(payment);
                    comboBoxPaymentCell.Value = accountingInfos[i].Payment;
                }
                if (File.Exists(accountingInfos[i].ImagePath1))
                {
                    Image img = Image.FromFile(accountingInfos[i].ImagePath1);
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Value = img;
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Tag = accountingInfos[i].ImagePathCompression1;
                }
                else
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath1"].Value = null;

                if (File.Exists(accountingInfos[i].ImagePath2))
                {
                    Image img = Image.FromFile(accountingInfos[i].ImagePath2);
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Value = img;
                    DataGridView_AccountingInfo.Rows[i].Cells["ImageColumnPath2"].Tag = accountingInfos[i].ImagePathCompression2;
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
            DataGridView_AccountingInfo.Init();
            if (groupByAmounts == null)
                return;
            DataGridView_AccountingInfo.DataSource = groupByAmounts;
        }

        private void CreateCheckBoxes(FlowLayoutPanel f)
        {
            foreach (var type in ExpenseData.Types)
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
            foreach (var companion in ExpenseData.Companions)
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
            foreach (var payment in ExpenseData.Payments)
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
            foreach (var orderBy in ExpenseData.OrderBys)
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

        List<Expression<Func<AccountingInfo, bool>>> conditions = new List<Expression<Func<AccountingInfo, bool>>>();

        private void CheckBox_AllCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel panel = (FlowLayoutPanel)checkBox.Parent;
            foreach (var item in panel.Controls.OfType<CheckBox>())
                item.Checked = checkBox.Checked;
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
            ExpenseData.OrderBys[text] = checkBox.Checked;
        }
    }
}
