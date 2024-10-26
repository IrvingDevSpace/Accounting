using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Models
{
    internal class SelectItemInfo
    {
        public static Dictionary<string, List<string>> Types { get; set; } = new Dictionary<string, List<string>>
        {
            { "交通", new List<string> { "油費", "火車", "捷運" } },
            { "飲食", new List<string> { "早餐", "午餐", "晚餐" } },
            { "娛樂", new List<string> { "唱歌", "購物", "運動" } }
        };

        public static List<String> Companions { get; set; } = new List<String>() { "自己", "家人", "朋友" };

        public static List<String> Payments { get; set; } = new List<String>() { "現金", "信用卡", "電子支付" };
    }
}
