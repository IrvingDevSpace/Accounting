using System;

namespace Accounting.SingletonUtils
{
    internal class NavbarAttribute : Attribute
    {
        public String FormName { get; set; }
        public NavbarAttribute(String formName)
        {
            FormName = formName;
        }
    }
}
