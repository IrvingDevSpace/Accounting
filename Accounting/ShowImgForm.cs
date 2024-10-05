using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
    }
}
