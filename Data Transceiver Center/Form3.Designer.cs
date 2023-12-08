
namespace Data_Transceiver_Center
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.radioBtn_IgnorePlc = new System.Windows.Forms.RadioButton();
            this.radioBtn_ConnectPlc = new System.Windows.Forms.RadioButton();
            this.btn_Form3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.needCheck_checkBox = new System.Windows.Forms.CheckBox();
            this.btn_Form1 = new System.Windows.Forms.Button();
            this.btn_Form2 = new System.Windows.Forms.Button();
            this.btn_SaveIni = new System.Windows.Forms.Button();
            this.btn_LoadIni = new System.Windows.Forms.Button();
            this.autoRun_btn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trigger1_checkBox = new System.Windows.Forms.CheckBox();
            this.ignorePlc_checkBox = new System.Windows.Forms.CheckBox();
            this.tcpServer_checkBox = new System.Windows.Forms.CheckBox();
            this.connectPlc_checkBox = new System.Windows.Forms.CheckBox();
            this.lable_PlcConnectStatus = new System.Windows.Forms.Label();
            this.btn_RetryRead = new System.Windows.Forms.Button();
            this.btn_RetryChk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btn_Form3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.needCheck_checkBox);
            this.panel1.Location = new System.Drawing.Point(128, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 625);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.radioBtn_IgnorePlc);
            this.panel2.Controls.Add(this.radioBtn_ConnectPlc);
            this.panel2.Location = new System.Drawing.Point(155, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 68);
            this.panel2.TabIndex = 13;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlText;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(68, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 35);
            this.label2.TabIndex = 13;
            this.label2.Text = "PLC状态";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioBtn_IgnorePlc
            // 
            this.radioBtn_IgnorePlc.AutoSize = true;
            this.radioBtn_IgnorePlc.Location = new System.Drawing.Point(3, 15);
            this.radioBtn_IgnorePlc.Name = "radioBtn_IgnorePlc";
            this.radioBtn_IgnorePlc.Size = new System.Drawing.Size(65, 16);
            this.radioBtn_IgnorePlc.TabIndex = 11;
            this.radioBtn_IgnorePlc.Text = "屏蔽PLC";
            this.radioBtn_IgnorePlc.UseVisualStyleBackColor = true;
            // 
            // radioBtn_ConnectPlc
            // 
            this.radioBtn_ConnectPlc.AutoSize = true;
            this.radioBtn_ConnectPlc.Location = new System.Drawing.Point(3, 37);
            this.radioBtn_ConnectPlc.Name = "radioBtn_ConnectPlc";
            this.radioBtn_ConnectPlc.Size = new System.Drawing.Size(65, 16);
            this.radioBtn_ConnectPlc.TabIndex = 12;
            this.radioBtn_ConnectPlc.Text = "连接PLC";
            this.radioBtn_ConnectPlc.UseVisualStyleBackColor = true;
            // 
            // btn_Form3
            // 
            this.btn_Form3.Location = new System.Drawing.Point(331, 72);
            this.btn_Form3.Name = "btn_Form3";
            this.btn_Form3.Size = new System.Drawing.Size(150, 27);
            this.btn_Form3.TabIndex = 3;
            this.btn_Form3.Text = "页面3";
            this.btn_Form3.UseVisualStyleBackColor = true;
            this.btn_Form3.Visible = false;
            this.btn_Form3.Click += new System.EventHandler(this.btn_form3Open);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "广告位招租";
            // 
            // needCheck_checkBox
            // 
            this.needCheck_checkBox.AutoSize = true;
            this.needCheck_checkBox.Location = new System.Drawing.Point(391, 12);
            this.needCheck_checkBox.Name = "needCheck_checkBox";
            this.needCheck_checkBox.Size = new System.Drawing.Size(72, 16);
            this.needCheck_checkBox.TabIndex = 10;
            this.needCheck_checkBox.Text = "需要校验";
            this.needCheck_checkBox.UseVisualStyleBackColor = true;
            this.needCheck_checkBox.Visible = false;
            this.needCheck_checkBox.CheckedChanged += new System.EventHandler(this.t5CheckTask);
            // 
            // btn_Form1
            // 
            this.btn_Form1.Location = new System.Drawing.Point(27, 25);
            this.btn_Form1.Name = "btn_Form1";
            this.btn_Form1.Size = new System.Drawing.Size(81, 57);
            this.btn_Form1.TabIndex = 1;
            this.btn_Form1.Text = "页面1";
            this.btn_Form1.UseVisualStyleBackColor = true;
            this.btn_Form1.Click += new System.EventHandler(this.btn_form1Open);
            // 
            // btn_Form2
            // 
            this.btn_Form2.Location = new System.Drawing.Point(27, 113);
            this.btn_Form2.Name = "btn_Form2";
            this.btn_Form2.Size = new System.Drawing.Size(81, 57);
            this.btn_Form2.TabIndex = 2;
            this.btn_Form2.Text = "页面2";
            this.btn_Form2.UseVisualStyleBackColor = true;
            this.btn_Form2.Click += new System.EventHandler(this.btn_form2Open);
            // 
            // btn_SaveIni
            // 
            this.btn_SaveIni.Location = new System.Drawing.Point(8, 592);
            this.btn_SaveIni.Name = "btn_SaveIni";
            this.btn_SaveIni.Size = new System.Drawing.Size(54, 39);
            this.btn_SaveIni.TabIndex = 4;
            this.btn_SaveIni.Text = "保存配置";
            this.btn_SaveIni.UseVisualStyleBackColor = true;
            this.btn_SaveIni.Click += new System.EventHandler(this.btnSaveIni_Click);
            // 
            // btn_LoadIni
            // 
            this.btn_LoadIni.Location = new System.Drawing.Point(68, 592);
            this.btn_LoadIni.Name = "btn_LoadIni";
            this.btn_LoadIni.Size = new System.Drawing.Size(54, 39);
            this.btn_LoadIni.TabIndex = 5;
            this.btn_LoadIni.Text = "加载配置";
            this.btn_LoadIni.UseVisualStyleBackColor = true;
            this.btn_LoadIni.Click += new System.EventHandler(this.btnLoadIni_Click);
            // 
            // autoRun_btn
            // 
            this.autoRun_btn.Location = new System.Drawing.Point(27, 250);
            this.autoRun_btn.Name = "autoRun_btn";
            this.autoRun_btn.Size = new System.Drawing.Size(81, 57);
            this.autoRun_btn.TabIndex = 6;
            this.autoRun_btn.Text = "自动一次";
            this.autoRun_btn.UseVisualStyleBackColor = true;
            this.autoRun_btn.Click += new System.EventHandler(this.autoRun_btn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.autoRun_btn_Click);
            // 
            // trigger1_checkBox
            // 
            this.trigger1_checkBox.AutoSize = true;
            this.trigger1_checkBox.Location = new System.Drawing.Point(8, 532);
            this.trigger1_checkBox.Name = "trigger1_checkBox";
            this.trigger1_checkBox.Size = new System.Drawing.Size(108, 16);
            this.trigger1_checkBox.TabIndex = 7;
            this.trigger1_checkBox.Text = "二维码输入触发";
            this.trigger1_checkBox.UseVisualStyleBackColor = true;
            this.trigger1_checkBox.CheckedChanged += new System.EventHandler(this.trigger1_CheckBox_CheckedChanged);
            // 
            // ignorePlc_checkBox
            // 
            this.ignorePlc_checkBox.AutoSize = true;
            this.ignorePlc_checkBox.Location = new System.Drawing.Point(8, 427);
            this.ignorePlc_checkBox.Name = "ignorePlc_checkBox";
            this.ignorePlc_checkBox.Size = new System.Drawing.Size(66, 16);
            this.ignorePlc_checkBox.TabIndex = 8;
            this.ignorePlc_checkBox.Text = "屏蔽PLC";
            this.ignorePlc_checkBox.UseVisualStyleBackColor = true;
            // 
            // tcpServer_checkBox
            // 
            this.tcpServer_checkBox.AutoSize = true;
            this.tcpServer_checkBox.Location = new System.Drawing.Point(8, 554);
            this.tcpServer_checkBox.Name = "tcpServer_checkBox";
            this.tcpServer_checkBox.Size = new System.Drawing.Size(66, 16);
            this.tcpServer_checkBox.TabIndex = 9;
            this.tcpServer_checkBox.Text = "TCP接收";
            this.tcpServer_checkBox.UseVisualStyleBackColor = true;
            this.tcpServer_checkBox.CheckedChanged += new System.EventHandler(this.tcpServer_checkBox_CheckedChanged);
            // 
            // connectPlc_checkBox
            // 
            this.connectPlc_checkBox.AutoSize = true;
            this.connectPlc_checkBox.Location = new System.Drawing.Point(8, 449);
            this.connectPlc_checkBox.Name = "connectPlc_checkBox";
            this.connectPlc_checkBox.Size = new System.Drawing.Size(66, 16);
            this.connectPlc_checkBox.TabIndex = 11;
            this.connectPlc_checkBox.Text = "连接PLC";
            this.connectPlc_checkBox.UseVisualStyleBackColor = true;
            this.connectPlc_checkBox.CheckStateChanged += new System.EventHandler(this.connectPlc_checkBox_CheckStateChanged);
            // 
            // lable_PlcConnectStatus
            // 
            this.lable_PlcConnectStatus.BackColor = System.Drawing.SystemColors.ControlText;
            this.lable_PlcConnectStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.lable_PlcConnectStatus.Location = new System.Drawing.Point(6, 468);
            this.lable_PlcConnectStatus.Name = "lable_PlcConnectStatus";
            this.lable_PlcConnectStatus.Size = new System.Drawing.Size(85, 15);
            this.lable_PlcConnectStatus.TabIndex = 14;
            this.lable_PlcConnectStatus.Text = "PLC状态";
            this.lable_PlcConnectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_RetryRead
            // 
            this.btn_RetryRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RetryRead.Location = new System.Drawing.Point(141, 308);
            this.btn_RetryRead.Name = "btn_RetryRead";
            this.btn_RetryRead.Size = new System.Drawing.Size(150, 27);
            this.btn_RetryRead.TabIndex = 15;
            this.btn_RetryRead.Text = "手动读码";
            this.btn_RetryRead.UseVisualStyleBackColor = true;
            this.btn_RetryRead.Visible = false;
            this.btn_RetryRead.Click += new System.EventHandler(this.btn_Retry_fog_Click);
            // 
            // btn_RetryChk
            // 
            this.btn_RetryChk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RetryChk.Location = new System.Drawing.Point(617, 302);
            this.btn_RetryChk.Name = "btn_RetryChk";
            this.btn_RetryChk.Size = new System.Drawing.Size(150, 27);
            this.btn_RetryChk.TabIndex = 16;
            this.btn_RetryChk.Text = "手动验码";
            this.btn_RetryChk.UseVisualStyleBackColor = true;
            this.btn_RetryChk.Visible = false;
            this.btn_RetryChk.Click += new System.EventHandler(this.btn_RetryChk_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 643);
            this.Controls.Add(this.btn_RetryChk);
            this.Controls.Add(this.btn_RetryRead);
            this.Controls.Add(this.lable_PlcConnectStatus);
            this.Controls.Add(this.connectPlc_checkBox);
            this.Controls.Add(this.tcpServer_checkBox);
            this.Controls.Add(this.ignorePlc_checkBox);
            this.Controls.Add(this.trigger1_checkBox);
            this.Controls.Add(this.autoRun_btn);
            this.Controls.Add(this.btn_LoadIni);
            this.Controls.Add(this.btn_SaveIni);
            this.Controls.Add(this.btn_Form2);
            this.Controls.Add(this.btn_Form1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Form1;
        private System.Windows.Forms.Button btn_Form2;
        private System.Windows.Forms.Button btn_Form3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SaveIni;
        private System.Windows.Forms.Button btn_LoadIni;
        private System.Windows.Forms.Button autoRun_btn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox trigger1_checkBox;
        private System.Windows.Forms.CheckBox ignorePlc_checkBox;
        private System.Windows.Forms.CheckBox tcpServer_checkBox;
        private System.Windows.Forms.CheckBox needCheck_checkBox;
        private System.Windows.Forms.CheckBox connectPlc_checkBox;
        private System.Windows.Forms.RadioButton radioBtn_IgnorePlc;
        private System.Windows.Forms.RadioButton radioBtn_ConnectPlc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lable_PlcConnectStatus;
        private System.Windows.Forms.Button btn_RetryRead;
        private System.Windows.Forms.Button btn_RetryChk;
    }
}