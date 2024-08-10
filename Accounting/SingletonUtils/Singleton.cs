using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Accounting.SingletonUtils
{
    internal sealed class Singleton
    {
        private static Form openForm;

        private static Dictionary<String, Form> formDictionary;

        public static String FormName { get; private set; }

        static Singleton()
        {
            FormName = "";
            formDictionary = new Dictionary<String, Form>();
        }

        public static Form GetForm(String formName)
        {
            if (!(openForm == null || openForm.IsDisposed))
                openForm.Hide();

            FormName = formName;
            if (!formDictionary.TryGetValue(FormName, out openForm))
            {
                Type type = Type.GetType("Accounting.Forms." + FormName);
                formDictionary.Add(FormName, (Form)Activator.CreateInstance(type));
                openForm = formDictionary[FormName];
            }
            return openForm;
        }
    }
}
