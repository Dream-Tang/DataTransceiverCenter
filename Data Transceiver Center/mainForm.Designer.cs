
namespace Data_Transceiver_Center
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ignoreCheck_checkBox = new System.Windows.Forms.CheckBox();
            this.btn_Form3 = new System.Windows.Forms.Button();
            this.btn_Form1 = new System.Windows.Forms.Button();
            this.btn_Form2 = new System.Windows.Forms.Button();
            this.btn_SaveIni = new System.Windows.Forms.Button();
            this.btn_LoadConfig = new System.Windows.Forms.Button();
            this.autoRun_btn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.autoRun_checkBox = new System.Windows.Forms.CheckBox();
            this.ignorePlc_checkBox = new System.Windows.Forms.CheckBox();
            this.tcpServer_checkBox = new System.Windows.Forms.CheckBox();
            this.connectPlc_checkBox = new System.Windows.Forms.CheckBox();
            this.lable_PlcConnectStatus = new System.Windows.Forms.Label();
            this.ignoreCam_checkBox = new System.Windows.Forms.CheckBox();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.AutoSize = true;
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this.label1);
            this.panelContainer.Location = new System.Drawing.Point(128, 12);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(800, 650);
            this.panelContainer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "公司LOGO图片";
            // 
            // ignoreCheck_checkBox
            // 
            this.ignoreCheck_checkBox.AutoSize = true;
            this.ignoreCheck_checkBox.Location = new System.Drawing.Point(12, 352);
            this.ignoreCheck_checkBox.Name = "ignoreCheck_checkBox";
            this.ignoreCheck_checkBox.Size = new System.Drawing.Size(106, 22);
            this.ignoreCheck_checkBox.TabIndex = 10;
            this.ignoreCheck_checkBox.Text = "屏蔽校验";
            this.ignoreCheck_checkBox.UseVisualStyleBackColor = true;
            this.ignoreCheck_checkBox.CheckedChanged += new System.EventHandler(this.chkbox_ignoreCheck_checked);
            // 
            // btn_Form3
            // 
            this.btn_Form3.Location = new System.Drawing.Point(19, 72);
            this.btn_Form3.Name = "btn_Form3";
            this.btn_Form3.Size = new System.Drawing.Size(90, 40);
            this.btn_Form3.TabIndex = 3;
            this.btn_Form3.Text = "MES设置";
            this.btn_Form3.UseVisualStyleBackColor = true;
            this.btn_Form3.Click += new System.EventHandler(this.btn_form3Open);
            // 
            // btn_Form1
            // 
            this.btn_Form1.Location = new System.Drawing.Point(16, 12);
            this.btn_Form1.Name = "btn_Form1";
            this.btn_Form1.Size = new System.Drawing.Size(90, 40);
            this.btn_Form1.TabIndex = 1;
            this.btn_Form1.Text = "工作流程";
            this.btn_Form1.UseVisualStyleBackColor = true;
            this.btn_Form1.Click += new System.EventHandler(this.btn_form1Open);
            // 
            // btn_Form2
            // 
            this.btn_Form2.Location = new System.Drawing.Point(16, 137);
            this.btn_Form2.Name = "btn_Form2";
            this.btn_Form2.Size = new System.Drawing.Size(90, 40);
            this.btn_Form2.TabIndex = 2;
            this.btn_Form2.Text = "PLC通信";
            this.btn_Form2.UseVisualStyleBackColor = true;
            this.btn_Form2.Click += new System.EventHandler(this.btn_form2Open);
            // 
            // btn_SaveIni
            // 
            this.btn_SaveIni.Location = new System.Drawing.Point(8, 608);
            this.btn_SaveIni.Name = "btn_SaveIni";
            this.btn_SaveIni.Size = new System.Drawing.Size(54, 54);
            this.btn_SaveIni.TabIndex = 4;
            this.btn_SaveIni.Text = "保存配置";
            this.btn_SaveIni.UseVisualStyleBackColor = true;
            this.btn_SaveIni.Click += new System.EventHandler(this.btnSaveIni_Click);
            // 
            // btn_LoadConfig
            // 
            this.btn_LoadConfig.Location = new System.Drawing.Point(68, 608);
            this.btn_LoadConfig.Name = "btn_LoadConfig";
            this.btn_LoadConfig.Size = new System.Drawing.Size(54, 54);
            this.btn_LoadConfig.TabIndex = 5;
            this.btn_LoadConfig.Text = "加载配置";
            this.btn_LoadConfig.UseVisualStyleBackColor = true;
            this.btn_LoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // autoRun_btn
            // 
            this.autoRun_btn.Location = new System.Drawing.Point(16, 222);
            this.autoRun_btn.Name = "autoRun_btn";
            this.autoRun_btn.Size = new System.Drawing.Size(90, 40);
            this.autoRun_btn.TabIndex = 6;
            this.autoRun_btn.Text = "自动一次";
            this.autoRun_btn.UseVisualStyleBackColor = true;
            this.autoRun_btn.Click += new System.EventHandler(this.autoRun_btn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2500;
            this.timer1.Tick += new System.EventHandler(this.autoRun_btn_Click);
            // 
            // autoRun_checkBox
            // 
            this.autoRun_checkBox.AutoSize = true;
            this.autoRun_checkBox.Location = new System.Drawing.Point(12, 513);
            this.autoRun_checkBox.Name = "autoRun_checkBox";
            this.autoRun_checkBox.Size = new System.Drawing.Size(106, 22);
            this.autoRun_checkBox.TabIndex = 7;
            this.autoRun_checkBox.Text = "自动流程";
            this.autoRun_checkBox.UseVisualStyleBackColor = true;
            this.autoRun_checkBox.CheckedChanged += new System.EventHandler(this.trigger1_CheckBox_CheckedChanged);
            // 
            // ignorePlc_checkBox
            // 
            this.ignorePlc_checkBox.AutoSize = true;
            this.ignorePlc_checkBox.Location = new System.Drawing.Point(12, 380);
            this.ignorePlc_checkBox.Name = "ignorePlc_checkBox";
            this.ignorePlc_checkBox.Size = new System.Drawing.Size(97, 22);
            this.ignorePlc_checkBox.TabIndex = 8;
            this.ignorePlc_checkBox.Text = "屏蔽PLC";
            this.ignorePlc_checkBox.UseVisualStyleBackColor = true;
            // 
            // tcpServer_checkBox
            // 
            this.tcpServer_checkBox.AutoSize = true;
            this.tcpServer_checkBox.Location = new System.Drawing.Point(13, 541);
            this.tcpServer_checkBox.Name = "tcpServer_checkBox";
            this.tcpServer_checkBox.Size = new System.Drawing.Size(97, 22);
            this.tcpServer_checkBox.TabIndex = 9;
            this.tcpServer_checkBox.Text = "TCP接收";
            this.tcpServer_checkBox.UseVisualStyleBackColor = true;
            this.tcpServer_checkBox.CheckedChanged += new System.EventHandler(this.tcpServer_checkBox_CheckedChanged);
            // 
            // connectPlc_checkBox
            // 
            this.connectPlc_checkBox.AutoSize = true;
            this.connectPlc_checkBox.Location = new System.Drawing.Point(12, 408);
            this.connectPlc_checkBox.Name = "connectPlc_checkBox";
            this.connectPlc_checkBox.Size = new System.Drawing.Size(97, 22);
            this.connectPlc_checkBox.TabIndex = 11;
            this.connectPlc_checkBox.Text = "连接PLC";
            this.connectPlc_checkBox.UseVisualStyleBackColor = true;
            this.connectPlc_checkBox.CheckedChanged += new System.EventHandler(this.connectPlc_checkBox_CheckedChanged);
            // 
            // lable_PlcConnectStatus
            // 
            this.lable_PlcConnectStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lable_PlcConnectStatus.BackColor = System.Drawing.Color.Gray;
            this.lable_PlcConnectStatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable_PlcConnectStatus.ForeColor = System.Drawing.Color.White;
            this.lable_PlcConnectStatus.Location = new System.Drawing.Point(9, 408);
            this.lable_PlcConnectStatus.Name = "lable_PlcConnectStatus";
            this.lable_PlcConnectStatus.Size = new System.Drawing.Size(110, 40);
            this.lable_PlcConnectStatus.TabIndex = 14;
            this.lable_PlcConnectStatus.Text = "PLC状态";
            this.lable_PlcConnectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ignoreCam_checkBox
            // 
            this.ignoreCam_checkBox.AutoSize = true;
            this.ignoreCam_checkBox.Location = new System.Drawing.Point(12, 324);
            this.ignoreCam_checkBox.Name = "ignoreCam_checkBox";
            this.ignoreCam_checkBox.Size = new System.Drawing.Size(106, 22);
            this.ignoreCam_checkBox.TabIndex = 15;
            this.ignoreCam_checkBox.Text = "跳过相机";
            this.ignoreCam_checkBox.UseVisualStyleBackColor = true;
            this.ignoreCam_checkBox.CheckedChanged += new System.EventHandler(this.ignoreCam_checkBox_CheckedChanged);
            // 
            // mainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(937, 674);
            this.Controls.Add(this.ignoreCam_checkBox);
            this.Controls.Add(this.ignoreCheck_checkBox);
            this.Controls.Add(this.lable_PlcConnectStatus);
            this.Controls.Add(this.btn_Form3);
            this.Controls.Add(this.connectPlc_checkBox);
            this.Controls.Add(this.tcpServer_checkBox);
            this.Controls.Add(this.ignorePlc_checkBox);
            this.Controls.Add(this.autoRun_checkBox);
            this.Controls.Add(this.autoRun_btn);
            this.Controls.Add(this.btn_LoadConfig);
            this.Controls.Add(this.btn_SaveIni);
            this.Controls.Add(this.btn_Form2);
            this.Controls.Add(this.btn_Form1);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btn_Form1;
        private System.Windows.Forms.Button btn_Form2;
        private System.Windows.Forms.Button btn_Form3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SaveIni;
        private System.Windows.Forms.Button btn_LoadConfig;
        private System.Windows.Forms.Button autoRun_btn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox autoRun_checkBox;
        private System.Windows.Forms.CheckBox ignorePlc_checkBox;
        private System.Windows.Forms.CheckBox tcpServer_checkBox;
        private System.Windows.Forms.CheckBox ignoreCheck_checkBox;
        private System.Windows.Forms.CheckBox connectPlc_checkBox;
        private System.Windows.Forms.Label lable_PlcConnectStatus;
        private System.Windows.Forms.CheckBox ignoreCam_checkBox;
    }
}