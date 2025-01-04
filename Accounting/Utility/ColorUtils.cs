using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Utility
{
    internal class ColorUtils
    {
        private static Random _random = new Random();

        public static Color GetRandomColor()
        {
            int r = _random.Next(0, 256); // 0 到 255 的隨機值
            int g = _random.Next(0, 256);
            int b = _random.Next(0, 256);

            return Color.FromArgb(r, g, b);
        }
    }
}
