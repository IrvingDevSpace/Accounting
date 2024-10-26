using Accounting.Components;
using Accounting.SingletonUtils;
using System;
using System.Windows.Forms;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("帳戶")]
    public partial class AccountForm : Form
    {
        private Navbar navbar;

        public AccountForm()
        {
            InitializeComponent();
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
                Location = new System.Drawing.Point(43, 361),
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
    }
}
