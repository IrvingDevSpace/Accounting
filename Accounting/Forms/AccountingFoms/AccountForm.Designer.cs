namespace Accounting.Forms.AccountingFoms
{
    partial class AccountForm
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
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DataGridView_AccountingInfo = new System.Windows.Forms.DataGridView();
            this.Button_Select = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_AccountingInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Location = new System.Drawing.Point(510, 23);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(197, 29);
            this.DateTimePicker_End.TabIndex = 11;
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Location = new System.Drawing.Point(291, 23);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(197, 29);
            this.DateTimePicker_Start.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "日期";
            // 
            // DataGridView_AccountingInfo
            // 
            this.DataGridView_AccountingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_AccountingInfo.Location = new System.Drawing.Point(35, 84);
            this.DataGridView_AccountingInfo.Name = "DataGridView_AccountingInfo";
            this.DataGridView_AccountingInfo.RowHeadersWidth = 62;
            this.DataGridView_AccountingInfo.RowTemplate.Height = 31;
            this.DataGridView_AccountingInfo.Size = new System.Drawing.Size(1051, 442);
            this.DataGridView_AccountingInfo.TabIndex = 8;
            this.DataGridView_AccountingInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_AccountingInfo_CellContentClick);
            this.DataGridView_AccountingInfo.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_AccountingInfo_CellContentDoubleClick);
            this.DataGridView_AccountingInfo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_AccountingInfo_CellEndEdit);
            // 
            // Button_Select
            // 
            this.Button_Select.Location = new System.Drawing.Point(909, 15);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(165, 49);
            this.Button_Select.TabIndex = 7;
            this.Button_Select.Text = "查詢";
            this.Button_Select.UseVisualStyleBackColor = true;
            this.Button_Select.Click += new System.EventHandler(this.Button_Select_Click);
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1413, 746);
            this.Controls.Add(this.DateTimePicker_End);
            this.Controls.Add(this.DateTimePicker_Start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataGridView_AccountingInfo);
            this.Controls.Add(this.Button_Select);
            this.Name = "AccountForm";
            this.Text = "AccountForm";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_AccountingInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataGridView_AccountingInfo;
        private System.Windows.Forms.Button Button_Select;
    }
}