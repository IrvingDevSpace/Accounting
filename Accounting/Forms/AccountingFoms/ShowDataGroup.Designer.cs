namespace Accounting.Forms.AccountingFoms
{
    partial class ShowDataGroup
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
            this.FLP_OrderBy = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_Where = new System.Windows.Forms.FlowLayoutPanel();
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DataGridView_AccountingInfo = new System.Windows.Forms.DataGridView();
            this.Button_Select = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_AccountingInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // FLP_OrderBy
            // 
            this.FLP_OrderBy.Location = new System.Drawing.Point(17, 310);
            this.FLP_OrderBy.Name = "FLP_OrderBy";
            this.FLP_OrderBy.Size = new System.Drawing.Size(468, 225);
            this.FLP_OrderBy.TabIndex = 18;
            // 
            // FLP_Where
            // 
            this.FLP_Where.Location = new System.Drawing.Point(17, 80);
            this.FLP_Where.Name = "FLP_Where";
            this.FLP_Where.Size = new System.Drawing.Size(468, 225);
            this.FLP_Where.TabIndex = 19;
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Location = new System.Drawing.Point(506, 20);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(198, 29);
            this.DateTimePicker_End.TabIndex = 17;
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Location = new System.Drawing.Point(287, 20);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(198, 29);
            this.DateTimePicker_Start.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "日期";
            // 
            // DataGridView_AccountingInfo
            // 
            this.DataGridView_AccountingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_AccountingInfo.Location = new System.Drawing.Point(492, 80);
            this.DataGridView_AccountingInfo.Name = "DataGridView_AccountingInfo";
            this.DataGridView_AccountingInfo.RowHeadersWidth = 62;
            this.DataGridView_AccountingInfo.RowTemplate.Height = 31;
            this.DataGridView_AccountingInfo.Size = new System.Drawing.Size(988, 456);
            this.DataGridView_AccountingInfo.TabIndex = 14;
            // 
            // Button_Select
            // 
            this.Button_Select.Location = new System.Drawing.Point(905, 13);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(165, 50);
            this.Button_Select.TabIndex = 13;
            this.Button_Select.Text = "查詢";
            this.Button_Select.UseVisualStyleBackColor = true;
            this.Button_Select.Click += new System.EventHandler(this.Button_Select_Click);
            // 
            // ShowDataGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 608);
            this.Controls.Add(this.FLP_OrderBy);
            this.Controls.Add(this.FLP_Where);
            this.Controls.Add(this.DateTimePicker_End);
            this.Controls.Add(this.DateTimePicker_Start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataGridView_AccountingInfo);
            this.Controls.Add(this.Button_Select);
            this.Name = "ShowDataGroup";
            this.Text = "ShowDataGroup";
            this.Load += new System.EventHandler(this.ShowDataGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_AccountingInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FLP_OrderBy;
        private System.Windows.Forms.FlowLayoutPanel FLP_Where;
        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataGridView_AccountingInfo;
        private System.Windows.Forms.Button Button_Select;
    }
}