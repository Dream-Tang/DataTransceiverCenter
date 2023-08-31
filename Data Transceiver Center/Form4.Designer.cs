
namespace Data_Transceiver_Center
{
    partial class Form4
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
            this.label1_5 = new System.Windows.Forms.Label();
            this.prtCode_txtBox = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4_2 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.fileWatcher_chkbox = new System.Windows.Forms.CheckBox();
            this.label4_3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Location = new System.Drawing.Point(14, 51);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(43, 14);
            this.label1_5.TabIndex = 59;
            this.label1_5.Text = "打印码";
            // 
            // prtCode_txtBox
            // 
            this.prtCode_txtBox.Location = new System.Drawing.Point(13, 68);
            this.prtCode_txtBox.Name = "prtCode_txtBox";
            this.prtCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.prtCode_txtBox.TabIndex = 58;
            this.prtCode_txtBox.TextChanged += new System.EventHandler(this.prtCode_txtBox_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(14, 93);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(134, 59);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // label4_2
            // 
            this.label4_2.AutoSize = true;
            this.label4_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4_2.Location = new System.Drawing.Point(197, 93);
            this.label4_2.Name = "label4_2";
            this.label4_2.Size = new System.Drawing.Size(55, 14);
            this.label4_2.TabIndex = 67;
            this.label4_2.Text = "监控位置";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // fileWatcher_chkbox
            // 
            this.fileWatcher_chkbox.AutoSize = true;
            this.fileWatcher_chkbox.Location = new System.Drawing.Point(197, 51);
            this.fileWatcher_chkbox.Name = "fileWatcher_chkbox";
            this.fileWatcher_chkbox.Size = new System.Drawing.Size(84, 16);
            this.fileWatcher_chkbox.TabIndex = 68;
            this.fileWatcher_chkbox.Text = "文件夹监控";
            this.fileWatcher_chkbox.UseVisualStyleBackColor = true;
            this.fileWatcher_chkbox.CheckedChanged += new System.EventHandler(this.fileWatcher_chkbox_CheckedChanged);
            // 
            // label4_3
            // 
            this.label4_3.AutoSize = true;
            this.label4_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4_3.Location = new System.Drawing.Point(197, 127);
            this.label4_3.Name = "label4_3";
            this.label4_3.Size = new System.Drawing.Size(55, 14);
            this.label4_3.TabIndex = 69;
            this.label4_3.Text = "监控状态";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 551);
            this.Controls.Add(this.label4_3);
            this.Controls.Add(this.fileWatcher_chkbox);
            this.Controls.Add(this.label4_2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1_5);
            this.Controls.Add(this.prtCode_txtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1_5;
        private System.Windows.Forms.TextBox prtCode_txtBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4_2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label label4_3;
        private System.Windows.Forms.CheckBox fileWatcher_chkbox;
    }
}