using Accounting.SingletonUtils;
using System;
using System.Windows.Forms;

namespace Accounting
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Singleton.GetForm("AddAccountingForm"));
        }
    }
}
