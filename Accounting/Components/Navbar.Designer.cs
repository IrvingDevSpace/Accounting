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
            this.FlowLayoutPanel_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FlowLayoutPanel_Buttons
            // 
            this.FlowLayoutPanel_Buttons.AutoScroll = true;
            this.FlowLayoutPanel_Buttons.Location = new System.Drawing.Point(2, 2);
            this.FlowLayoutPanel_Buttons.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FlowLayoutPanel_Buttons.Name = "FlowLayoutPanel_Buttons";
            this.FlowLayoutPanel_Buttons.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FlowLayoutPanel_Buttons.Size = new System.Drawing.Size(363, 53);
            this.FlowLayoutPanel_Buttons.TabIndex = 0;
            // 
            // Navbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FlowLayoutPanel_Buttons);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Navbar";
            this.Size = new System.Drawing.Size(367, 57);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Buttons;
    }
}
