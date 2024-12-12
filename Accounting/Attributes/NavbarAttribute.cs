using System;

namespace Accounting.Attributes
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
