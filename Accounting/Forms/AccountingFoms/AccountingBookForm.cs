using Accounting.Components;
using Accounting.Extenstion;
using Accounting.Models;
using Accounting.SingletonUtils;
using CSV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Accounting.Forms.AccountingFoms
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

        private void Button_Select_Click(object sender, EventArgs e)
        {
            this.SetDebounceTime(Start, 200);
        }

        private void Start()
        {
            this.Invoke(new Action(() =>
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

                DataGridView_AccountingInfo.Columns.Clear();
                DataGridView_AccountingInfo.DataSource = null;

                GC.Collect();

                DataGridView_AccountingInfo.AllowDrop = false;
                DataGridView_AccountingInfo.AllowUserToAddRows = false;
                DataGridView_AccountingInfo.AllowUserToDeleteRows = false;
                DataGridView_AccountingInfo.AllowUserToOrderColumns = false;
                DataGridView_AccountingInfo.AllowUserToResizeColumns = false;
                DataGridView_AccountingInfo.AllowUserToResizeRows = false;
                DataGridView_AccountingInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridView_AccountingInfo.RowHeadersVisible = false;
                DataGridView_AccountingInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                DataGridView_AccountingInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DataGridView_AccountingInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                DataGridView_AccountingInfo.DefaultCellStyle.Font = new Font("微軟正黑體", (float)9, FontStyle.Bold);
                DataGridView_AccountingInfo.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", (float)9, FontStyle.Bold);
                //DataGridView_AccountingInfo.ReadOnly = true;

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
                List<AddAccountingInfo> addAccountingInfos = dataGridView.DataSource as List<AddAccountingInfo>;
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

        private void DataGridView_AccountingInfo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.CurrentCell.OwningColumn.Name == "comboBoxColumnType" && e.Control is ComboBox typeComboBox)
            {
                typeComboBox.Tag = dataGridView.CurrentCell;
                typeComboBox.SelectedIndexChanged += TypeComboBox_SelectedIndexChanged;
            }
            if (dataGridView.CurrentCell.OwningColumn.Name == "comboBoxColumnPurpose" && e.Control is ComboBox purposeComboBox)
            {
                purposeComboBox.Tag = dataGridView.CurrentCell;
                purposeComboBox.SelectedIndexChanged += PurposeComboBox_SelectedIndexChanged;
            }
            if (dataGridView.CurrentCell.OwningColumn.Name == "comboBoxColumnCompanion" && e.Control is ComboBox companionComboBox)
            {
                companionComboBox.Tag = dataGridView.CurrentCell;
                companionComboBox.SelectedIndexChanged += CompanionComboBox_SelectedIndexChanged;
            }
            if (dataGridView.CurrentCell.OwningColumn.Name == "comboBoxColumnPayment" && e.Control is ComboBox paymentComboBox)
            {
                paymentComboBox.Tag = dataGridView.CurrentCell;
                paymentComboBox.SelectedIndexChanged += PaymentComboBox_SelectedIndexChanged;
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!(comboBox.Tag is DataGridViewCell currentCell))
                return;
            DataGridView dataGridView = currentCell.DataGridView;
            int rowIndex = currentCell.RowIndex;
            if (!(dataGridView.Rows[rowIndex].Cells["comboBoxColumnPurpose"] is DataGridViewComboBoxCell purposeComboBoxCell))
                return;
            if (!SelectItemInfo.Types.TryGetValue(comboBox.Text, out List<string> values))
                return;
            purposeComboBoxCell.Value = null;
            purposeComboBoxCell.Items.Clear();
            foreach (var value in values)
                purposeComboBoxCell.Items.Add(value);
            purposeComboBoxCell.Value = purposeComboBoxCell.Items[0];
            if (dataGridView.Rows[rowIndex].Cells["Purpose"] is DataGridViewTextBoxCell purposeTextBoxCell)
                purposeTextBoxCell.Value = purposeComboBoxCell.Value;
            if (dataGridView.Rows[rowIndex].Cells["Type"] is DataGridViewTextBoxCell textBoxCell)
                textBoxCell.Value = comboBox.Text;
        }

        private void PurposeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!(comboBox.Tag is DataGridViewCell currentCell))
                return;
            DataGridView dataGridView = currentCell.DataGridView;
            int rowIndex = currentCell.RowIndex;
            if (dataGridView.Rows[rowIndex].Cells["Purpose"] is DataGridViewTextBoxCell textBoxCell)
                textBoxCell.Value = comboBox.Text;
        }

        private void CompanionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!(comboBox.Tag is DataGridViewCell currentCell))
                return;
            DataGridView dataGridView = currentCell.DataGridView;
            int rowIndex = currentCell.RowIndex;
            if (dataGridView.Rows[rowIndex].Cells["Companion"] is DataGridViewTextBoxCell textBoxCell)
                textBoxCell.Value = comboBox.Text;
        }

        private void PaymentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!(comboBox.Tag is DataGridViewCell currentCell))
                return;
            DataGridView dataGridView = currentCell.DataGridView;
            int rowIndex = currentCell.RowIndex;
            if (dataGridView.Rows[rowIndex].Cells["Payment"] is DataGridViewTextBoxCell textBoxCell)
                textBoxCell.Value = comboBox.Text;
        }

        private void DataGridView_AccountingInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (!(dataGridView.DataSource is List<AddAccountingInfo> addAccountingInfos))
                return;
            String time = dataGridView.Rows[e.RowIndex].Cells["Time"].Value.ToString();
            addAccountingInfos = addAccountingInfos.Where(x => x.Time == time).ToList();
            String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            if (File.Exists($"{directoryPath}\\Data.csv"))
                File.Delete($"{directoryPath}\\Data.csv");
            CSVHelper.Write($"{directoryPath}\\Data.csv", addAccountingInfos);
        }
    }
}
