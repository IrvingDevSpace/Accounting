using Accounting.Components;
using Accounting.Model;
using Accounting.SingletonUtils;
using CSV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Accounting.Forms
{
    [Navbar("記一筆")]
    public partial class AddAccountingForm : Form
    {
        private Navbar navbar;

        public AddAccountingForm()
        {
            InitializeComponent();
            this.Text = this.GetFormTitle();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddEvent();
        }

        private void AddEvent()
        {
            this.Load += AddAccountingForm_Load;
            this.FormClosed += AddAccountingForm_FormClosed;
            this.pictureBox1.Click += pictureBox_Click;
            this.pictureBox2.Click += pictureBox_Click;
        }

        private void AddAccountingForm_Load(object sender, System.EventArgs e)
        {
            navbar = new Navbar
            {
                Location = new System.Drawing.Point(43, 361),
                Name = "Navbar_ChangeForm",
                Size = new System.Drawing.Size(550, 70)
            };
            this.Controls.Add(navbar);
            this.SetFormsNavbarButton();


            ComboBox_Type.Items.Add("交通");
            ComboBox_Type.Items.Add("飲食");
            ComboBox_Type.Items.Add("娛樂");
            ComboBox_Who.Items.Add("自己");
            ComboBox_Who.Items.Add("家人");
            ComboBox_Who.Items.Add("朋友");
            ComboBox_Payment.Items.Add("現金");
            ComboBox_Payment.Items.Add("信用卡");
            ComboBox_Payment.Items.Add("電子支付");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void AddAccountingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ComboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            String typeName = comboBox.Text;
            ComboBox_Purpose.Items.Clear();
            switch (typeName)
            {
                case "交通":
                    ComboBox_Purpose.Items.Add("油費");
                    ComboBox_Purpose.Items.Add("火車");
                    ComboBox_Purpose.Items.Add("捷運");
                    break;
                case "飲食":
                    ComboBox_Purpose.Items.Add("早餐");
                    ComboBox_Purpose.Items.Add("午餐");
                    ComboBox_Purpose.Items.Add("晚餐");
                    break;
                case "娛樂":
                    ComboBox_Purpose.Items.Add("唱歌");
                    ComboBox_Purpose.Items.Add("購物");
                    ComboBox_Purpose.Items.Add("運動");
                    break;
                default:
                    break;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 取得選擇的檔案路徑
                String filePath = openFileDialog.FileName;
                Image image = Image.FromFile(filePath);
                pictureBox.Image = image;
            }
        }
        String fileServerPath = "C:\\Users\\IRVING\\Program Course\\Code\\Accounting\\FileServer\\";
        private void button1_Click(object sender, EventArgs e)
        {
            String time = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String amount = textBox1.Text;
            String type = ComboBox_Type.Text;
            String purpose = ComboBox_Purpose.Text;
            String who = ComboBox_Who.Text;
            String payment = ComboBox_Payment.Text;

            String imgPath1 = $"{fileServerPath}{Guid.NewGuid().ToString()}_imgPath1.png";
            String imgPath2 = $"{fileServerPath}{Guid.NewGuid().ToString()}_imgPath2.png";
            pictureBox1.Image.Save(imgPath1);
            pictureBox2.Image.Save(imgPath2);

            AddAccountingInfo addAccountingInfo = new AddAccountingInfo
            {
                Time = time,
                Amount = amount,
                Type = type,
                Purpose = purpose,
                Who = who,
                Payment = payment,
                ImagePath1 = imgPath1,
                ImagePath2 = imgPath2
            };

            CSVHelper.Write($"{fileServerPath}Data.csv", addAccountingInfo);
        }
    }
}
