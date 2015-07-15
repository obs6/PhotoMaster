namespace PhotoMaster
{
    partial class PhotoMaster
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrint = new System.Windows.Forms.Button();
            this.textBoxPicAdress = new System.Windows.Forms.TextBox();
            this.listViewPicSelect = new System.Windows.Forms.ListView();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panelPhotoFrame = new System.Windows.Forms.Panel();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.panelPhotoFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(43, 258);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 45);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "确认打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // textBoxPicAdress
            // 
            this.textBoxPicAdress.Location = new System.Drawing.Point(28, 28);
            this.textBoxPicAdress.Multiline = true;
            this.textBoxPicAdress.Name = "textBoxPicAdress";
            this.textBoxPicAdress.Size = new System.Drawing.Size(166, 179);
            this.textBoxPicAdress.TabIndex = 2;
            this.textBoxPicAdress.Text = "收到的图片网络地址";
            // 
            // listViewPicSelect
            // 
            this.listViewPicSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPicSelect.Location = new System.Drawing.Point(-1, 478);
            this.listViewPicSelect.Name = "listViewPicSelect";
            this.listViewPicSelect.Size = new System.Drawing.Size(1128, 200);
            this.listViewPicSelect.TabIndex = 3;
            this.listViewPicSelect.UseCompatibleStateImageBehavior = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panelPhotoFrame
            // 
            this.panelPhotoFrame.BackgroundImage = global::PhotoMaster.Properties.Resources.mainframe1;
            this.panelPhotoFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelPhotoFrame.Controls.Add(this.picBox);
            this.panelPhotoFrame.Location = new System.Drawing.Point(234, 28);
            this.panelPhotoFrame.Name = "panelPhotoFrame";
            this.panelPhotoFrame.Size = new System.Drawing.Size(406, 406);
            this.panelPhotoFrame.TabIndex = 1;
            // 
            // picBox
            // 
            this.picBox.Image = global::PhotoMaster.Properties.Resources.initpicframe;
            this.picBox.InitialImage = null;
            this.picBox.Location = new System.Drawing.Point(24, 25);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(250, 294);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
            // 
            // PhotoMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 678);
            this.Controls.Add(this.listViewPicSelect);
            this.Controls.Add(this.textBoxPicAdress);
            this.Controls.Add(this.panelPhotoFrame);
            this.Controls.Add(this.btnPrint);
            this.Name = "PhotoMaster";
            this.Text = "PhotoMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PhotoMaster_FormClosed);
            this.Load += new System.EventHandler(this.PhotoMaster_Load);
            this.panelPhotoFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panelPhotoFrame;
        private System.Windows.Forms.TextBox textBoxPicAdress;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ListView listViewPicSelect;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}

