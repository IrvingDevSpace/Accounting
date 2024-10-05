using Accounting.Components;
using Accounting.Model;
using Accounting.SingletonUtils;
using Accounting.Utility;
using CSV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            String directoryPath = Path.GetDirectoryName($"{fileServerPath}\\{time}\\");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            String imgPathResize1 = $"{directoryPath}\\{Guid.NewGuid().ToString()}_imgPathResize1.jpg";
            String imgPathResize2 = $"{directoryPath}\\{Guid.NewGuid().ToString()}_imgPathResize2.jpg";
            String imgPathCompression1 = $"{directoryPath}\\{Guid.NewGuid().ToString()}_imgPathCompression1.jpg";
            String imgPathCompression2 = $"{directoryPath}\\{Guid.NewGuid().ToString()}_imgPathCompression2.jpg";

            ImageEncoder.ImageResizeAndSave((Bitmap)pictureBox1.Image.Clone(), imgPathResize1);
            ImageEncoder.ImageResizeAndSave((Bitmap)pictureBox2.Image.Clone(), imgPathResize2);
            ImageEncoder.CompressionAndSave((Bitmap)pictureBox1.Image.Clone(), imgPathCompression1, 1L);
            ImageEncoder.CompressionAndSave((Bitmap)pictureBox2.Image.Clone(), imgPathCompression2, 1L);

            AddAccountingInfo addAccountingInfo = new AddAccountingInfo
            {
                Time = time,
                Amount = amount,
                Type = type,
                Purpose = purpose,
                Who = who,
                Payment = payment,
                ImagePath1 = imgPathResize1,
                ImagePath2 = imgPathResize2,
                ImagePathCompression1 = imgPathCompression1,
                ImagePathCompression2 = imgPathCompression2
            };

            CSVHelper.Write($"{directoryPath}\\Data.csv", addAccountingInfo);
            MessageBox.Show("儲存成功", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
