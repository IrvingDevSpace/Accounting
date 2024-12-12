using Accounting.Attributes;
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
            this.FlowLayoutPanel_Buttons.AutoScroll = true;
            this.FlowLayoutPanel_Buttons.WrapContents = false; // 防止自動換行
            AddButton();
            AddEvent();
        }

        private void AddEvent()
        {
        }

        private List<Type> GetFormTypes()
        {
            String namespaceName = "Accounting.Forms.AccountingFoms";
            //List<Type> formTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.FullName.Contains(namespaceName) && !x.FullName.Contains("Extension")).ToList();
            List<Type> formTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == namespaceName && t.IsSubclassOf(typeof(Form))).ToList();
            return formTypes;
        }

        private void OpenForm_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            String formName = button.Name.Replace("Button", "Form");
            Singleton.GetForm(formName).Show();
        }

        public void SetButton(String formName)
        {
            Control[] controls = this.Controls.Find(formName.Replace("Form", "Button"), true);
            if (!(controls.Length > 0))
                return;
            if (controls[0] is Button btn)
                btn.Enabled = false;
        }

        private void AddButton()
        {
            List<Type> formNames = GetFormTypes();
            for (int i = 0; i < formNames.Count; i++)
            {
                Type formType = Type.GetType("Accounting.Forms.AccountingFoms." + formNames[i].Name);
                NavbarAttribute attribute = (NavbarAttribute)Attribute.GetCustomAttribute(formType, typeof(NavbarAttribute));
                String formName = attribute?.FormName ?? throw new Exception($"{formType.Name} NavbarAttribute FormName not exist.");
                Button button = new Button
                {
                    Name = formNames[i].Name.Replace("Form", "Button"),
                    Size = new System.Drawing.Size(80, 30),
                    TabIndex = 0,
                    TabStop = false,
                    Text = formName,
                    UseVisualStyleBackColor = true
                };
                button.Click += OpenForm_Click;
                FlowLayoutPanel_Buttons.Controls.Add(button);
            }
        }
    }
}
