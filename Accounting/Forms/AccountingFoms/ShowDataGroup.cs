using Accounting.Attributes;
using Accounting.Components;
using Accounting.Extension;
using Accounting.Models;
using Accounting.Presenter;
using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Accounting.Contract.AccountingDataContract;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("顯示")]
    public partial class ShowDataGroup : Form, IAccountingDataView
    {
        private Navbar navbar;
        private IAccountingDataPresenter _accountingDataPresenter = null;

        public ShowDataGroup()
        {
            InitializeComponent();
            AddEvent();
            _accountingDataPresenter = new AccountingDataPresenter(this);
        }

        private void AddEvent()
        {
            this.Load += ShowDataGroup_Load;
            this.FormClosed += ShowDataGroup_FormClosed;
        }

        private void ShowDataGroup_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            navbar = new Navbar
            {
                Location = new System.Drawing.Point(11, 361),
                Name = "Navbar_ChangeForm",
                Size = new System.Drawing.Size(550, 70)
            };
            this.Controls.Add(navbar);
            this.SetFormsNavbarButton();
            CreateWhereCheckBoxes(FLP_Where);
            CreateOrderByCheckBoxes(FLP_OrderBy);
        }

        private void ShowDataGroup_FormClosed(object sender, FormClosedEventArgs e)
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
                    StartTime = DateTimePicker_Start.Value.Date,
                    EndTime = DateTimePicker_End.Value.Date
                };
                if (ExpenseData.OrderBys.Values.Any(x => x))
                    _accountingDataPresenter.GetGroupByAmounts(searchDate);
                else
                    _accountingDataPresenter.GetAccountingInfos(searchDate);
            }));
        }

        void IAccountingDataView.RenderAccountingInfos(List<AccountingInfo> accountingInfos)
        {
            DataGridView_AccountingInfo.Init();
            if (accountingInfos == null)
                return;
            DataGridView_AccountingInfo.DataSource = accountingInfos;

            var type = typeof(AccountingInfo);
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                var columnName = prop.Name;
                var column = DataGridView_AccountingInfo.Columns[columnName];

                if (column == null) continue;

                // 檢查 GridColumn 屬性
                var gridColumnAttr = prop.GetCustomAttribute<GridColumnAttribute>();
                var displayNameColumnAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
                string headerText = displayNameColumnAttr?.DisplayName ?? prop.Name;
                if (gridColumnAttr != null)
                {
                    DataGridViewColumn newColumn;
                    column.ReadOnly = gridColumnAttr.ReadOnly;
                    column.Visible = gridColumnAttr.Visible;
                    if (gridColumnAttr.ColumnType == null) continue;
                    newColumn = (DataGridViewColumn)Activator.CreateInstance(gridColumnAttr.ColumnType);
                    newColumn.Name = gridColumnAttr.ColumnName;
                    newColumn.HeaderText = headerText;
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // 處理 ComboBox 的資料來源
                    if (newColumn is DataGridViewComboBoxColumn comboBoxColumn && !string.IsNullOrEmpty(headerText))
                    {
                        comboBoxColumn.Items.AddRange(ExpenseData.Datas[headerText].ToArray());
                    }
                    if (newColumn is DataGridViewImageColumn imageColumn)
                    {
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        imageColumn.ReadOnly = true; // 圖片列通常為只讀
                    }
                    DataGridView_AccountingInfo.Columns.Add(newColumn);
                }
            }

            DataGridViewButtonColumn btnDeleteCol = new DataGridViewButtonColumn();
            btnDeleteCol.Name = "Delete";
            btnDeleteCol.HeaderText = "刪除";
            btnDeleteCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //置中
            DataGridView_AccountingInfo.Columns.Add(btnDeleteCol);

            for (int i = 0; i < accountingInfos.Count; i++)
            {
                var row = DataGridView_AccountingInfo.Rows[i];
                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<GridColumnAttribute>();
                    if (attribute?.ColumnType == null)
                        continue;
                    var value = property.GetValue(accountingInfos[i]);
                    if (row.Cells[attribute.ColumnName] is DataGridViewComboBoxCell comboBoxCell)
                    {
                        string valueString = value.ToString();
                        comboBoxCell.Value = valueString;
                        if (!ExpenseData.CategoryToPurpose.ContainsKey(valueString))
                            continue;
                        if (row.Cells[attribute.ChildColumnName] is DataGridViewComboBoxCell comboBoxChildCell)
                        {
                            comboBoxChildCell.Value = null;
                            comboBoxChildCell.Items.Clear();
                            foreach (var item in ExpenseData.CategoryToPurpose[valueString])
                                comboBoxChildCell.Items.Add(item);
                            comboBoxChildCell.Value = comboBoxChildCell.Items[0];
                        }
                    }
                    else if (row.Cells[attribute.ColumnName] is DataGridViewImageCell imageCell)
                    {
                        if (!File.Exists(value?.ToString()))
                            continue;
                        imageCell.Value = Image.FromFile(value.ToString());
                        var compressionPath = property.GetCustomAttribute<GridColumnAttribute>()?.ImagePathCompression;
                        if (!string.IsNullOrEmpty(compressionPath))
                        {
                            var compressionValue = type.GetProperty(compressionPath)?.GetValue(accountingInfos[i]);
                            imageCell.Tag = compressionValue;
                        }
                    }
                    else
                        row.Cells[attribute.ColumnName].Value = value;
                }
                if (row.Cells["Delete"] is DataGridViewButtonCell buttonDeleteCell)
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

        private void CreateWhereCheckBoxes(FlowLayoutPanel flowLayoutPanel)
        {
            foreach (var key in ExpenseData.Datas.Keys)
            {
                List<CheckBox> checkBoxes = new List<CheckBox>();
                FlowLayoutPanel panel = new FlowLayoutPanel { AutoSize = true };
                CheckBox keyCheckBox = new CheckBox { Text = key, AutoSize = true };
                keyCheckBox.CheckedChanged += CheckBox_AllCheckedChanged;
                checkBoxes.Add(keyCheckBox);
                foreach (var item in ExpenseData.Datas[key])
                {
                    CheckBox valCheckBox = new CheckBox { Text = item, AutoSize = true, Tag = key };
                    valCheckBox.CheckedChanged += CheckBox_CheckedChanged;
                    checkBoxes.Add(valCheckBox);
                }
                panel.Controls.AddRange(checkBoxes.ToArray());
                flowLayoutPanel.Controls.Add(panel);
            }
        }

        private void CreateOrderByCheckBoxes(FlowLayoutPanel flowLayoutPanel)
        {
            FlowLayoutPanel panel = new FlowLayoutPanel { AutoSize = true };
            foreach (var orderBy in ExpenseData.OrderBys)
            {
                CheckBox keyCheckBox = new CheckBox { Text = orderBy.Key, AutoSize = true };
                keyCheckBox.CheckedChanged += CheckBoxOrderby_CheckedChanged;
                panel.Controls.Add(keyCheckBox);
            }
            flowLayoutPanel.Controls.Add(panel);
        }

        private void CheckBox_AllCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel panel = (FlowLayoutPanel)checkBox.Parent;
            foreach (var item in panel.Controls.OfType<CheckBox>())
                item.Checked = checkBox.Checked;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Tag is string tag && ExpenseData.CategoryLists.ContainsKey(tag))
            {
                List<string> list = ExpenseData.CategoryLists[tag];
                if (checkBox.Checked)
                    list.Add(checkBox.Text);
                else
                    list.Remove(checkBox.Text);
            }
        }

        private void CheckBoxOrderby_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string text = checkBox.Text;
            ExpenseData.OrderBys[text] = checkBox.Checked;
        }

        private void DataGridView_AccountingInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell btn)
            {
                String time = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                List<AccountingInfo> accountingInfos = dataGridView.DataSource as List<AccountingInfo>;
                dataGridView.Init();
                accountingInfos.RemoveAt(e.RowIndex);
                accountingInfos = accountingInfos.Where(x => x.Time == time).ToList();
                String directoryPath = Path.Combine(ConfigurationManager.AppSettings["FileServerPath"], time);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, "Data.csv");
                if (File.Exists(filePath))
                    File.Delete(filePath);
                CSVHelper.Write(filePath, accountingInfos);
                Button_Select.PerformClick();
            }
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

        private void DataGridView_AccountingInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (!(dataGridView.DataSource is List<AccountingInfo> accountingInfos))
                return;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            string columnName = dataGridView.Columns[e.ColumnIndex].Name;

            var attribute = typeof(AccountingInfo).GetProperties()
                .Select(p => p.GetCustomAttribute<GridColumnAttribute>())
                .FirstOrDefault(attr => attr?.ColumnName == columnName);

            if (attribute?.ColumnType != null)
            {
                if (row.Cells[attribute.ColumnName] is DataGridViewComboBoxCell comboBoxCell)
                {
                    string valueString = row.Cells[attribute.ColumnName].Value.ToString();
                    if (ExpenseData.CategoryToPurpose.ContainsKey(valueString))
                    {
                        if (row.Cells[attribute.ChildColumnName] is DataGridViewComboBoxCell comboBoxChildCell)
                        {
                            comboBoxChildCell.Value = null;
                            comboBoxChildCell.Items.Clear();
                            foreach (var item in ExpenseData.CategoryToPurpose[valueString])
                                comboBoxChildCell.Items.Add(item);
                            comboBoxChildCell.Value = comboBoxChildCell.Items[0];
                        }
                    }
                }
            }

            PropertyInfo[] propertyInfos = typeof(AccountingInfo).GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var attr = propertyInfos[i].GetCustomAttribute<GridColumnAttribute>();
                if (attr?.ColumnType == null)
                    continue;
                String colName = attr?.ColumnName;
                if (!string.IsNullOrEmpty(colName))
                    if (dataGridView.Rows[e.RowIndex].Cells[colName] is DataGridViewComboBoxCell)
                        dataGridView.Rows[e.RowIndex].Cells[propertyInfos[i].Name].Value = dataGridView.Rows[e.RowIndex].Cells[colName].Value;
            }

            String time = dataGridView.Rows[e.RowIndex].Cells["Time"].Value.ToString();
            accountingInfos = accountingInfos.Where(x => x.Time == time).ToList();
            String directoryPath = Path.Combine(ConfigurationManager.AppSettings["FileServerPath"], time);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "Data.csv");
            if (File.Exists(filePath))
                File.Delete(filePath);
            CSVHelper.Write(filePath, accountingInfos);
        }

        public void RenderChart(Chart chart)
        {
            throw new NotImplementedException();
        }
    }
}
