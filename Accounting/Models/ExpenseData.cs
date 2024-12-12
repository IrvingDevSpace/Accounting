using System;
using System.Collections.Generic;

namespace Accounting.Models
{
    internal class ExpenseData
    {
        public static Dictionary<string, List<string>> Datas { get; set; }

        private static Dictionary<string, List<string>> categoryToPurpose = new Dictionary<string, List<string>>();
        static ExpenseData()
        {
            // 先初始化所有選項
            Datas = new Dictionary<string, List<string>>
            {
                { "類型", new List<string> { "交通", "飲食", "娛樂" } },
                { "對象", new List<string> { "自己", "家人", "朋友" } },
                { "付款方式", new List<string> { "現金", "信用卡", "電子支付" } }
            };

            // 動態建立類型和目的的關聯
            categoryToPurpose.Add("交通", new List<string> { "油費", "火車", "捷運" });
            categoryToPurpose.Add("飲食", new List<string> { "早餐", "午餐", "晚餐" });
            categoryToPurpose.Add("娛樂", new List<string> { "唱歌", "購物", "運動" });
        }

        public static Dictionary<string, List<string>> Types { get; set; } = new Dictionary<string, List<string>>
        {
            { "交通", new List<string> { "油費", "火車", "捷運" } },
            { "飲食", new List<string> { "早餐", "午餐", "晚餐" } },
            { "娛樂", new List<string> { "唱歌", "購物", "運動" } }
        };

        public static List<String> Companions { get; set; } = new List<String>() { "自己", "家人", "朋友" };

        public static List<String> Payments { get; set; } = new List<String>() { "現金", "信用卡", "電子支付" };

        public static Dictionary<string, bool> OrderBys { get; set; } = new Dictionary<string, bool>
        {
            { "類型", false },
            { "目的", false },
            { "對象", false },
            { "付款方式", false },
        };
    }
}
