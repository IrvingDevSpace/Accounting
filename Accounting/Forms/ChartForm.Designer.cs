namespace Accounting.Forms
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.navbar1 = new Accounting.Components.Navbar();
            this.SuspendLayout();
            // 
            // navbar1
            // 
            this.navbar1.Location = new System.Drawing.Point(29, 562);
            this.navbar1.Name = "navbar1";
            this.navbar1.Size = new System.Drawing.Size(620, 70);
            this.navbar1.TabIndex = 0;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 644);
            this.Controls.Add(this.navbar1);
            this.Name = "ChartForm";
            this.Text = "圖表分析";
            this.ResumeLayout(false);

        }

        #endregion

        private Components.Navbar navbar1;
    }
}