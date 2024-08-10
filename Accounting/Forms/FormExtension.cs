using Accounting.SingletonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Accounting.Forms
{
    internal static class FormExtension
    {
        public static void SetFormsNavbarButton(this Form form)
        {
            List<FieldInfo> fieldInfos = form.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy).ToList();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.FieldType.Name == "Navbar")
                {
                    object fieldValue = fieldInfo.GetValue(form);
                    MethodInfo method = fieldValue.GetType().GetMethod("SetButton");
                    method.Invoke(fieldValue, new object[] { form.Name });
                    break;
                }
            }
        }

        public static String GetFormTitle(this Form form)
        {
            var attribute = (NavbarAttribute)Attribute.GetCustomAttribute(form.GetType(), typeof(NavbarAttribute));
            return attribute?.FormName ?? throw new Exception($"{form.Name} NavbarAttribute FormName not exist.");
        }
    }
}
