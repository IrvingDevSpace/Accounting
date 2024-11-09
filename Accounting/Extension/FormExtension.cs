using System;
using System.Threading;
using System.Windows.Forms;

namespace Accounting.Extension
{
    internal static class FormExtension
    {
        private static System.Threading.Timer timer;

        public static void SetDebounceTime(this Form form, Action action, int time)
        {
            if (timer != null)
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer = new System.Threading.Timer(TimerFunction, action, time, -1);
        }

        public static void TimerFunction(object state)
        {
            Action action = state as Action;
            action();
        }
    }
}
