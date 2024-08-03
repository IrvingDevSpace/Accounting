namespace Accounting.Components
{
    partial class Navbar
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.AddAccountingButton = new System.Windows.Forms.Button();
            this.AccountButton = new System.Windows.Forms.Button();
            this.AccountingBookButton = new System.Windows.Forms.Button();
            this.ChartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddAccountingButton
            // 
            this.AddAccountingButton.Location = new System.Drawing.Point(25, 10);
            this.AddAccountingButton.Name = "AddAccountingButton";
            this.AddAccountingButton.Size = new System.Drawing.Size(120, 50);
            this.AddAccountingButton.TabIndex = 0;
            this.AddAccountingButton.TabStop = false;
            this.AddAccountingButton.Text = "記一筆";
            this.AddAccountingButton.UseVisualStyleBackColor = true;
            // 
            // AccountButton
            // 
            this.AccountButton.Location = new System.Drawing.Point(175, 10);
            this.AccountButton.Name = "AccountButton";
            this.AccountButton.Size = new System.Drawing.Size(120, 50);
            this.AccountButton.TabIndex = 0;
            this.AccountButton.TabStop = false;
            this.AccountButton.Text = "帳戶";
            this.AccountButton.UseVisualStyleBackColor = true;
            // 
            // AccountingBookButton
            // 
            this.AccountingBookButton.Location = new System.Drawing.Point(325, 10);
            this.AccountingBookButton.Name = "AccountingBookButton";
            this.AccountingBookButton.Size = new System.Drawing.Size(120, 50);
            this.AccountingBookButton.TabIndex = 0;
            this.AccountingBookButton.TabStop = false;
            this.AccountingBookButton.Text = "記帳本";
            this.AccountingBookButton.UseVisualStyleBackColor = true;
            // 
            // ChartButton
            // 
            this.ChartButton.Location = new System.Drawing.Point(475, 10);
            this.ChartButton.Name = "ChartButton";
            this.ChartButton.Size = new System.Drawing.Size(120, 50);
            this.ChartButton.TabIndex = 0;
            this.ChartButton.TabStop = false;
            this.ChartButton.Text = "圖表分析";
            this.ChartButton.UseVisualStyleBackColor = true;
            // 
            // Navbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChartButton);
            this.Controls.Add(this.AccountingBookButton);
            this.Controls.Add(this.AccountButton);
            this.Controls.Add(this.AddAccountingButton);
            this.Name = "Navbar";
            this.Size = new System.Drawing.Size(620, 70);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddAccountingButton;
        private System.Windows.Forms.Button AccountButton;
        private System.Windows.Forms.Button AccountingBookButton;
        private System.Windows.Forms.Button ChartButton;
    }
}
