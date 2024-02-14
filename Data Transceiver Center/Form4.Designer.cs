
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
            this.components = new System.ComponentModel.Container();
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
            this.button3 = new System.Windows.Forms.Button();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.button4 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Location = new System.Drawing.Point(21, 76);
            this.label1_5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(64, 20);
            this.label1_5.TabIndex = 59;
            this.label1_5.Text = "打印码";
            // 
            // prtCode_txtBox
            // 
            this.prtCode_txtBox.Location = new System.Drawing.Point(20, 102);
            this.prtCode_txtBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prtCode_txtBox.Name = "prtCode_txtBox";
            this.prtCode_txtBox.Size = new System.Drawing.Size(200, 28);
            this.prtCode_txtBox.TabIndex = 58;
            this.prtCode_txtBox.TextChanged += new System.EventHandler(this.prtCode_txtBox_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(21, 140);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.label4_2.Location = new System.Drawing.Point(296, 140);
            this.label4_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4_2.Name = "label4_2";
            this.label4_2.Size = new System.Drawing.Size(82, 20);
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
            this.fileWatcher_chkbox.Location = new System.Drawing.Point(296, 76);
            this.fileWatcher_chkbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fileWatcher_chkbox.Name = "fileWatcher_chkbox";
            this.fileWatcher_chkbox.Size = new System.Drawing.Size(124, 22);
            this.fileWatcher_chkbox.TabIndex = 68;
            this.fileWatcher_chkbox.Text = "文件夹监控";
            this.fileWatcher_chkbox.UseVisualStyleBackColor = true;
            this.fileWatcher_chkbox.CheckedChanged += new System.EventHandler(this.fileWatcher_chkbox_CheckedChanged);
            // 
            // label4_3
            // 
            this.label4_3.AutoSize = true;
            this.label4_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4_3.Location = new System.Drawing.Point(296, 190);
            this.label4_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4_3.Name = "label4_3";
            this.label4_3.Size = new System.Drawing.Size(82, 20);
            this.label4_3.TabIndex = 69;
            this.label4_3.Text = "监控状态";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 387);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(200, 234);
            this.textBox1.TabIndex = 70;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(296, 387);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 28);
            this.textBox2.TabIndex = 71;
            // 
            // tcpPort_txtBox
            // 
            this.tcpPort_txtBox.Location = new System.Drawing.Point(576, 219);
            this.tcpPort_txtBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcpPort_txtBox.Name = "tcpPort_txtBox";
            this.tcpPort_txtBox.Size = new System.Drawing.Size(157, 28);
            this.tcpPort_txtBox.TabIndex = 72;
            // 
            // client_radBtn
            // 
            this.client_radBtn.AutoSize = true;
            this.client_radBtn.Location = new System.Drawing.Point(8, 4);
            this.client_radBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.client_radBtn.Name = "client_radBtn";
            this.client_radBtn.Size = new System.Drawing.Size(87, 22);
            this.client_radBtn.TabIndex = 74;
            this.client_radBtn.TabStop = true;
            this.client_radBtn.Text = "客户端";
            this.client_radBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.server_radBtn);
            this.panel1.Controls.Add(this.client_radBtn);
            this.panel1.Location = new System.Drawing.Point(576, 124);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 36);
            this.panel1.TabIndex = 75;
            this.panel1.Visible = false;
            // 
            // server_radBtn
            // 
            this.server_radBtn.AutoSize = true;
            this.server_radBtn.Location = new System.Drawing.Point(172, 4);
            this.server_radBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.server_radBtn.Name = "server_radBtn";
            this.server_radBtn.Size = new System.Drawing.Size(87, 22);
            this.server_radBtn.TabIndex = 75;
            this.server_radBtn.TabStop = true;
            this.server_radBtn.Text = "服务端";
            this.server_radBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(524, 183);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 18);
            this.label1.TabIndex = 76;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(524, 224);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 77;
            this.label2.Text = "端口";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(744, 216);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 34);
            this.button1.TabIndex = 78;
            this.button1.Text = "开启TCP接收";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(744, 172);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 34);
            this.button2.TabIndex = 79;
            this.button2.Text = "获取本地IP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(584, 278);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 28);
            this.textBox3.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(495, 282);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 81;
            this.label3.Text = "接收数据";
            // 
            // IPAddr_cobBox
            // 
            this.IPAddr_cobBox.FormattingEnabled = true;
            this.IPAddr_cobBox.Location = new System.Drawing.Point(576, 172);
            this.IPAddr_cobBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IPAddr_cobBox.Name = "IPAddr_cobBox";
            this.IPAddr_cobBox.Size = new System.Drawing.Size(157, 26);
            this.IPAddr_cobBox.TabIndex = 84;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(296, 507);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 40);
            this.button3.TabIndex = 86;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(296, 573);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(225, 40);
            this.pBar1.TabIndex = 85;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(576, 507);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(225, 40);
            this.button4.TabIndex = 87;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 826);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pBar1);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer timer1;
    }
}