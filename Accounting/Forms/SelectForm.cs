using Accounting.Components;
using Accounting.SingletonUtils;
using System;
using System.Windows.Forms;

namespace Accounting.Forms
{
    [Navbar("查詢")]
    public partial class SelectForm : Form
    {
        private Navbar navbar;

        public SelectForm()
        {
            InitializeComponent();
            this.Text = this.GetFormTitle();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddEvent();
        }

        private void AddEvent()
        {
            this.Load += SelectForm_Load;
            this.FormClosed += SelectForm_FormClosed;
        }

        private void SelectForm_Load(object sender, System.EventArgs e)
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

        private void SelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
