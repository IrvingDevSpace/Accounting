using Accounting.Components;
using Accounting.SingletonUtils;
using System;
using System.Windows.Forms;

namespace Accounting.Forms
{
    [Navbar("記一筆")]
    public partial class AddAccountingForm : Form
    {
        private Navbar navbar;

        public AddAccountingForm()
        {
            InitializeComponent();
            this.Text = this.GetFormTitle();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddEvent();
        }

        private void AddEvent()
        {
            this.Load += AddAccountingForm_Load;
            this.FormClosed += AddAccountingForm_FormClosed;
        }

        private void AddAccountingForm_Load(object sender, System.EventArgs e)
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

        private void AddAccountingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
