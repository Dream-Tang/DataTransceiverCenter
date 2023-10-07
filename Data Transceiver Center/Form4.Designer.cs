
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tcpPort_txtBox = new System.Windows.Forms.TextBox();
            this.client_radBtn = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.server_radBtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IPAddr_cobBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.panel1.SuspendLayout();
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 258);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(135, 157);
            this.textBox1.TabIndex = 70;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(197, 258);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(135, 21);
            this.textBox2.TabIndex = 71;
            // 
            // tcpPort_txtBox
            // 
            this.tcpPort_txtBox.Location = new System.Drawing.Point(384, 146);
            this.tcpPort_txtBox.Name = "tcpPort_txtBox";
            this.tcpPort_txtBox.Size = new System.Drawing.Size(106, 21);
            this.tcpPort_txtBox.TabIndex = 72;
            // 
            // client_radBtn
            // 
            this.client_radBtn.AutoSize = true;
            this.client_radBtn.Location = new System.Drawing.Point(5, 3);
            this.client_radBtn.Name = "client_radBtn";
            this.client_radBtn.Size = new System.Drawing.Size(59, 16);
            this.client_radBtn.TabIndex = 74;
            this.client_radBtn.TabStop = true;
            this.client_radBtn.Text = "客户端";
            this.client_radBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.server_radBtn);
            this.panel1.Controls.Add(this.client_radBtn);
            this.panel1.Location = new System.Drawing.Point(384, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 24);
            this.panel1.TabIndex = 75;
            this.panel1.Visible = false;
            // 
            // server_radBtn
            // 
            this.server_radBtn.AutoSize = true;
            this.server_radBtn.Location = new System.Drawing.Point(115, 3);
            this.server_radBtn.Name = "server_radBtn";
            this.server_radBtn.Size = new System.Drawing.Size(59, 16);
            this.server_radBtn.TabIndex = 75;
            this.server_radBtn.TabStop = true;
            this.server_radBtn.Text = "服务端";
            this.server_radBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 76;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 77;
            this.label2.Text = "端口";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(496, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 78;
            this.button1.Text = "开启TCP接收";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(496, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 79;
            this.button2.Text = "获取本地IP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(389, 185);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(135, 21);
            this.textBox3.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 81;
            this.label3.Text = "接收数据";
            // 
            // IPAddr_cobBox
            // 
            this.IPAddr_cobBox.FormattingEnabled = true;
            this.IPAddr_cobBox.Location = new System.Drawing.Point(384, 115);
            this.IPAddr_cobBox.Name = "IPAddr_cobBox";
            this.IPAddr_cobBox.Size = new System.Drawing.Size(106, 20);
            this.IPAddr_cobBox.TabIndex = 84;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 551);
            this.Controls.Add(this.IPAddr_cobBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tcpPort_txtBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton server_radBtn;
        private System.Windows.Forms.RadioButton client_radBtn;
        private System.Windows.Forms.TextBox tcpPort_txtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox IPAddr_cobBox;
    }
}