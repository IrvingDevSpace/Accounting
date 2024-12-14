using Accounting.Attributes;
using Accounting.Components;
using Accounting.Models;
using Accounting.Utility;
using CSV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Accounting.Forms.AccountingFoms
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

            foreach (var type in ExpenseData.Datas["類型"])
                ComboBox_Type.Items.Add(type);
            foreach (var companion in ExpenseData.Datas["對象"])
                ComboBox_Companion.Items.Add(companion);
            foreach (var payment in ExpenseData.Datas["付款方式"])
                ComboBox_Payment.Items.Add(payment);
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
            if (!ExpenseData.CategoryToPurpose.TryGetValue(typeName, out List<string> values))
                return;
            ComboBox_Purpose.Items.Clear();
            foreach (var value in values)
                ComboBox_Purpose.Items.Add(value);
            ComboBox_Purpose.Text = ComboBox_Purpose.Items[0].ToString();
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
            String who = ComboBox_Companion.Text;
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

            AccountingInfo AccountingInfo = new AccountingInfo
            {
                Time = time,
                Amount = amount,
                Type = type,
                Purpose = purpose,
                Companion = who,
                Payment = payment,
                ImagePath1 = imgPathResize1,
                ImagePath2 = imgPathResize2,
                ImagePathCompression1 = imgPathCompression1,
                ImagePathCompression2 = imgPathCompression2
            };

            CSVHelper.Write($"{directoryPath}\\Data.csv", AccountingInfo);
            MessageBox.Show("儲存成功", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
