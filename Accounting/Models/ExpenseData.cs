using System.Collections.Generic;

namespace Accounting.Models
{
    internal class ExpenseData
    {
        public static Dictionary<string, List<string>> Datas { get; set; }

        public static Dictionary<string, List<string>> CategoryToPurpose = new Dictionary<string, List<string>>();
        static ExpenseData()
        {
            // 先初始化所有選項
            Datas = new Dictionary<string, List<string>>
            {
                { "類型", new List<string> { "交通", "飲食", "娛樂" } }
            };

            // 動態建立類型和目的的關聯
            CategoryToPurpose.Add("交通", new List<string> { "油費", "火車", "捷運" });
            CategoryToPurpose.Add("飲食", new List<string> { "早餐", "午餐", "晚餐" });
            CategoryToPurpose.Add("娛樂", new List<string> { "唱歌", "購物", "運動" });

            List<string> purposes = new List<string>();
            foreach (var item in Datas["類型"])
                purposes.AddRange(CategoryToPurpose[item]);
            Datas.Add("目的", purposes);
            Datas.Add("對象", new List<string> { "自己", "家人", "朋友" });
            Datas.Add("付款方式", new List<string> { "現金", "信用卡", "電子支付" });
        }

        public static Dictionary<string, List<string>> CategoryLists = new Dictionary<string, List<string>>
        {
            { "類型", new List<string>() },
            { "目的", new List<string>() },
            { "對象", new List<string>() },
            { "付款方式", new List<string>() }
        };

        public static Dictionary<string, bool> OrderBys { get; set; } = new Dictionary<string, bool>
        {
            { "時間", false },
            { "類型", false },
            { "目的", false },
            { "對象", false },
            { "付款方式", false },
        };
    }
}
