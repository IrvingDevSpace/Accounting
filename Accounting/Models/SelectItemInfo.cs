using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public static List<Expression<Func<AddAccountingInfo, bool>>> A_Conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();
        public static List<Expression<Func<AddAccountingInfo, bool>>> B_Conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();
        public static List<Expression<Func<AddAccountingInfo, bool>>> C_Conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();
        public static List<Expression<Func<AddAccountingInfo, bool>>> D_Conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();
        public static List<Expression<Func<AddAccountingInfo, bool>>> E_Conditions = new List<Expression<Func<AddAccountingInfo, bool>>>();

        public static Dictionary<string, Expression<Func<AddAccountingInfo, bool>>> Funcs { get; set; } = new Dictionary<string, Expression<Func<AddAccountingInfo, bool>>>
        {
            { "交通", x => x.Type == "交通" },
            { "油費", x => x.Purpose == "油費" },
            { "火車", x => x.Purpose == "火車" },
            { "捷運", x => x.Purpose == "捷運" },
            { "飲食", x => x.Type == "飲食" },
            { "早餐", x => x.Purpose == "早餐" },
            { "午餐", x => x.Purpose == "午餐" },
            { "晚餐", x => x.Purpose == "晚餐" },
            { "娛樂", x => x.Type == "娛樂" },
            { "唱歌", x => x.Purpose == "唱歌" },
            { "購物", x => x.Purpose == "購物" },
            { "運動", x => x.Purpose == "運動" },
            { "對象", x => x.Companion == "自己" || x.Companion == "家人" || x.Companion == "朋友" },
            { "自己", x => x.Companion == "自己" },
            { "家人", x => x.Companion == "家人" },
            { "朋友", x => x.Companion == "朋友" },
            { "付款方式", x => x.Payment == "現金" || x.Payment == "信用卡" || x.Payment == "電子支付" },
            { "現金", x => x.Payment == "現金" },
            { "信用卡", x => x.Payment == "信用卡" },
            { "電子支付", x => x.Payment == "電子支付" },
        };

        public static Dictionary<string, bool> OrderBys { get; set; } = new Dictionary<string, bool>
        {
            { "類型", false },
            { "目的", false },
            { "對象", false },
            { "付款方式", false },
        };
    }
}
