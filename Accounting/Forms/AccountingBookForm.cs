﻿using Accounting.Components;
using Accounting.SingletonUtils;
using System;
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
    }
}
