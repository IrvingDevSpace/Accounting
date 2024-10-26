using System;
using System.Drawing;
using System.Windows.Forms;

namespace Accounting
{
    public partial class ShowImgForm : Form
    {
        private String filePath;
        public ShowImgForm(String filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
        }

        private void ShowImgForm_Load(object sender, EventArgs e)
        {
            Image image = Image.FromFile(filePath);
            pictureBox1.Image = image;
        }

        private void ShowImgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Dispose();
            GC.Collect();
        }
    }
}
