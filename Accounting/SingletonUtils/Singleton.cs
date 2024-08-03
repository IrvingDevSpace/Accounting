using Accounting.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Accounting.SingletonUtils
{
    internal sealed class Singleton
    {
        private static Form openForm;

        private static Dictionary<FormsTag, Form> formDictionary;

        public static String FormName { get; private set; }

        static Singleton()
        {
            FormName = "";
            formDictionary = new Dictionary<FormsTag, Form>();
        }

        public static Form GetForm(FormsTag formsTag)
        {
            if (!(openForm == null || openForm.IsDisposed))
                openForm.Hide();

            FormName = formsTag.ToString();
            if (!formDictionary.TryGetValue(formsTag, out openForm))
            {
                Type type = Type.GetType("Accounting.Forms." + formsTag.ToString());
                formDictionary.Add(formsTag, (Form)Activator.CreateInstance(type));
                openForm = formDictionary[formsTag];
            }
            return openForm;
        }
    }
}
