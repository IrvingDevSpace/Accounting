using Accounting.Components;
using Accounting.SingletonUtils;
using System;
using System.Windows.Forms;

namespace Accounting.Forms.AccountingFoms
{
    [Navbar("圖表分析")]
    public partial class ChartForm : Form
    {
        private Navbar navbar;

        public ChartForm()
        {
            InitializeComponent();
            this.Text = this.GetFormTitle();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddEvent();
        }

        private void AddEvent()
        {
            this.Load += ChartForm_Load;
            this.FormClosed += ChartForm_FormClosed;
        }

        private void ChartForm_Load(object sender, System.EventArgs e)
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

        private void ChartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
