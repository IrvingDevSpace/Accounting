namespace Accounting.Forms.AccountingFoms
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FLP_OrderBy = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_Where = new System.Windows.Forms.FlowLayoutPanel();
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_Select = new System.Windows.Forms.Button();
            this.DataGridView_AccountingInfo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_AccountingInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(551, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 455);
            this.panel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(746, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(197, 26);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FLP_OrderBy
            // 
            this.FLP_OrderBy.Location = new System.Drawing.Point(12, 331);
            this.FLP_OrderBy.Name = "FLP_OrderBy";
            this.FLP_OrderBy.Size = new System.Drawing.Size(468, 225);
            this.FLP_OrderBy.TabIndex = 17;
            // 
            // FLP_Where
            // 
            this.FLP_Where.Location = new System.Drawing.Point(12, 101);
            this.FLP_Where.Name = "FLP_Where";
            this.FLP_Where.Size = new System.Drawing.Size(468, 225);
            this.FLP_Where.TabIndex = 18;
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Location = new System.Drawing.Point(326, 37);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(198, 29);
            this.DateTimePicker_End.TabIndex = 16;
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Location = new System.Drawing.Point(107, 37);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(198, 29);
            this.DateTimePicker_Start.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "日期";
            // 
            // Button_Select
            // 
            this.Button_Select.Location = new System.Drawing.Point(551, 28);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(165, 50);
            this.Button_Select.TabIndex = 13;
            this.Button_Select.Text = "查詢";
            this.Button_Select.UseVisualStyleBackColor = true;
            this.Button_Select.Click += new System.EventHandler(this.Button_Select_Click);
            // 
            // DataGridView_AccountingInfo
            // 
            this.DataGridView_AccountingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_AccountingInfo.Location = new System.Drawing.Point(1289, 101);
            this.DataGridView_AccountingInfo.Name = "DataGridView_AccountingInfo";
            this.DataGridView_AccountingInfo.RowHeadersWidth = 62;
            this.DataGridView_AccountingInfo.RowTemplate.Height = 31;
            this.DataGridView_AccountingInfo.Size = new System.Drawing.Size(611, 456);
            this.DataGridView_AccountingInfo.TabIndex = 19;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2016, 711);
            this.Controls.Add(this.DataGridView_AccountingInfo);
            this.Controls.Add(this.FLP_OrderBy);
            this.Controls.Add(this.FLP_Where);
            this.Controls.Add(this.DateTimePicker_End);
            this.Controls.Add(this.DateTimePicker_Start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_Select);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_AccountingInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.FlowLayoutPanel FLP_OrderBy;
        private System.Windows.Forms.FlowLayoutPanel FLP_Where;
        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_Select;
        private System.Windows.Forms.DataGridView DataGridView_AccountingInfo;
    }
}