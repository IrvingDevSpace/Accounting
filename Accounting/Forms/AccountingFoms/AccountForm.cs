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
using System.Windows.Forms.DataVisualization.Charting;
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
                Location = new System.Drawing.Point(11, 361),
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

        }

        private void DataGridView_AccountingInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DataGridView_AccountingInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Start()
        {
        }

        public void RenderAccountingInfos(List<AccountingInfo> AccountingInfos)
        {
            throw new NotImplementedException();
        }

        void IAccountingDataView.RenderGroupByAmounts(List<GroupByAmount> groupByAmounts)
        {
            throw new NotImplementedException();
        }

        public void RenderChart(Chart chart)
        {
            throw new NotImplementedException();
        }
    }
}
