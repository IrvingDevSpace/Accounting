using Accounting.Forms;
using Accounting.SingletonUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Accounting.Components
{
    public partial class Navbar : UserControl
    {
        public Navbar()
        {
            InitializeComponent();
            AddEvent();
        }

        private void AddEvent()
        {
            AddAccountingButton.Click += OpenForm_Click;
            AccountButton.Click += OpenForm_Click;
            AccountingBookButton.Click += OpenForm_Click;
            ChartButton.Click += OpenForm_Click;
        }

        private List<Type> GetFormTypes()
        {
            String namespaceName = "Accounting.Forms";
            List<Type> formTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == namespaceName && t.IsSubclassOf(typeof(Form))).ToList();
            return formTypes;
        }

        private void OpenForm_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            String formName = button.Name.Replace("Button", "Form");
            Enum.TryParse(formName, out FormsTag formsTag);
            Singleton.GetForm(formsTag).Show();
        }

        public void SetButton(FormsTag formsTag)
        {
            Button btn = this.Controls.Find(formsTag.ToString().Replace("Form", "Button"), true)[0] as Button;
            btn.Enabled = false;
        }
    }
}
