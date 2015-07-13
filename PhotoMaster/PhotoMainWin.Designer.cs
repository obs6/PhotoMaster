namespace PhotoMaster
{
    partial class PhotoMasterV1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhotoMasterV1));
            this.btnPrint = new System.Windows.Forms.Button();
            this.panelPhotoFrame = new System.Windows.Forms.Panel();
            this.textBoxPicAdress = new System.Windows.Forms.TextBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.listViewPicSelect = new System.Windows.Forms.ListView();
            this.panelPhotoFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(836, 75);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 99);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "确认打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panelPhotoFrame
            // 
            this.panelPhotoFrame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelPhotoFrame.BackgroundImage")));
            this.panelPhotoFrame.Controls.Add(this.picBox);
            this.panelPhotoFrame.Location = new System.Drawing.Point(397, 28);
            this.panelPhotoFrame.Name = "panelPhotoFrame";
            this.panelPhotoFrame.Size = new System.Drawing.Size(299, 409);
            this.panelPhotoFrame.TabIndex = 1;
            // 
            // textBoxPicAdress
            // 
            this.textBoxPicAdress.Location = new System.Drawing.Point(28, 28);
            this.textBoxPicAdress.Multiline = true;
            this.textBoxPicAdress.Name = "textBoxPicAdress";
            this.textBoxPicAdress.Size = new System.Drawing.Size(264, 196);
            this.textBoxPicAdress.TabIndex = 2;
            this.textBoxPicAdress.Text = "收到的图片网络地址";
            // 
            // picBox
            // 
            this.picBox.Image = ((System.Drawing.Image)(resources.GetObject("picBox.Image")));
            this.picBox.InitialImage = null;
            this.picBox.Location = new System.Drawing.Point(23, 27);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(249, 296);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
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
            // PhotoMasterV1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 678);
            this.Controls.Add(this.listViewPicSelect);
            this.Controls.Add(this.textBoxPicAdress);
            this.Controls.Add(this.panelPhotoFrame);
            this.Controls.Add(this.btnPrint);
            this.Name = "PhotoMasterV1";
            this.Text = "PhotoMasterV1";
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
    }
}

