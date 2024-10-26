using System;
using System.ComponentModel;

namespace Accounting.Models
{
    public class AddAccountingInfo
    {
        [DisplayName("時間")]
        public String Time { get; set; }

        [DisplayName("金額")]
        public String Amount { get; set; }

        [DisplayName("類型")]
        public String Type { get; set; }

        [DisplayName("目的")]
        public String Purpose { get; set; }

        [DisplayName("對象")]
        public String Companion { get; set; }

        [DisplayName("付款方式")]
        public String Payment { get; set; }

        [DisplayName("發票圖檔1")]
        public String ImagePath1 { get; set; }

        [DisplayName("發票圖檔2")]
        public String ImagePath2 { get; set; }

        public String ImagePathCompression1 { get; set; }

        public String ImagePathCompression2 { get; set; }
    }
}
