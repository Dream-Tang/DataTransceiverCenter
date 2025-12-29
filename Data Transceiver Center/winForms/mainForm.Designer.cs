
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
            this.checkBox_ignoreCheck = new System.Windows.Forms.CheckBox();
            this.btn_openForm3 = new System.Windows.Forms.Button();
            this.btn_openForm1 = new System.Windows.Forms.Button();
            this.btn_openForm2 = new System.Windows.Forms.Button();
            this.btn_SaveIni = new System.Windows.Forms.Button();
            this.btn_LoadConfig = new System.Windows.Forms.Button();
            this.btn_autoRun = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox_autoMode = new System.Windows.Forms.CheckBox();
            this.checkBox_ignorePlc = new System.Windows.Forms.CheckBox();
            this.checkBox_tcpServer = new System.Windows.Forms.CheckBox();
            this.checkBox_connectPlc = new System.Windows.Forms.CheckBox();
            this.lable_PlcConnectStatus = new System.Windows.Forms.Label();
            this.checkBox_ignoreCam = new System.Windows.Forms.CheckBox();
            this.statusStrip_mainForm = new System.Windows.Forms.StatusStrip();
            this.statusModule = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLevel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelContainer.SuspendLayout();
            this.statusStrip_mainForm.SuspendLayout();
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
            // checkBox_ignoreCheck
            // 
            this.checkBox_ignoreCheck.AutoSize = true;
            this.checkBox_ignoreCheck.Location = new System.Drawing.Point(12, 337);
            this.checkBox_ignoreCheck.Name = "checkBox_ignoreCheck";
            this.checkBox_ignoreCheck.Size = new System.Drawing.Size(106, 22);
            this.checkBox_ignoreCheck.TabIndex = 10;
            this.checkBox_ignoreCheck.Text = "屏蔽校验";
            this.checkBox_ignoreCheck.UseVisualStyleBackColor = true;
            this.checkBox_ignoreCheck.CheckedChanged += new System.EventHandler(this.chkbox_ignoreCheck_checked);
            // 
            // btn_openForm3
            // 
            this.btn_openForm3.Location = new System.Drawing.Point(16, 74);
            this.btn_openForm3.Name = "btn_openForm3";
            this.btn_openForm3.Size = new System.Drawing.Size(90, 40);
            this.btn_openForm3.TabIndex = 3;
            this.btn_openForm3.Text = "MES设置";
            this.btn_openForm3.UseVisualStyleBackColor = true;
            this.btn_openForm3.Click += new System.EventHandler(this.btn_form3Open);
            // 
            // btn_openForm1
            // 
            this.btn_openForm1.Location = new System.Drawing.Point(16, 12);
            this.btn_openForm1.Name = "btn_openForm1";
            this.btn_openForm1.Size = new System.Drawing.Size(90, 40);
            this.btn_openForm1.TabIndex = 1;
            this.btn_openForm1.Text = "工作流程";
            this.btn_openForm1.UseVisualStyleBackColor = true;
            this.btn_openForm1.Click += new System.EventHandler(this.btn_form1Open);
            // 
            // btn_openForm2
            // 
            this.btn_openForm2.Location = new System.Drawing.Point(16, 136);
            this.btn_openForm2.Name = "btn_openForm2";
            this.btn_openForm2.Size = new System.Drawing.Size(90, 40);
            this.btn_openForm2.TabIndex = 2;
            this.btn_openForm2.Text = "PLC通信";
            this.btn_openForm2.UseVisualStyleBackColor = true;
            this.btn_openForm2.Click += new System.EventHandler(this.btn_form2Open);
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
            // btn_autoRun
            // 
            this.btn_autoRun.Location = new System.Drawing.Point(16, 222);
            this.btn_autoRun.Name = "btn_autoRun";
            this.btn_autoRun.Size = new System.Drawing.Size(90, 40);
            this.btn_autoRun.TabIndex = 6;
            this.btn_autoRun.Text = "自动一次";
            this.btn_autoRun.UseVisualStyleBackColor = true;
            this.btn_autoRun.Click += new System.EventHandler(this.autoRun_btn_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox_autoMode
            // 
            this.checkBox_autoMode.AutoSize = true;
            this.checkBox_autoMode.Location = new System.Drawing.Point(12, 513);
            this.checkBox_autoMode.Name = "checkBox_autoMode";
            this.checkBox_autoMode.Size = new System.Drawing.Size(106, 22);
            this.checkBox_autoMode.TabIndex = 7;
            this.checkBox_autoMode.Text = "自动流程";
            this.checkBox_autoMode.UseVisualStyleBackColor = true;
            this.checkBox_autoMode.CheckedChanged += new System.EventHandler(this.chkBox_autoMode_CheckedChanged);
            // 
            // checkBox_ignorePlc
            // 
            this.checkBox_ignorePlc.AutoSize = true;
            this.checkBox_ignorePlc.Location = new System.Drawing.Point(12, 365);
            this.checkBox_ignorePlc.Name = "checkBox_ignorePlc";
            this.checkBox_ignorePlc.Size = new System.Drawing.Size(97, 22);
            this.checkBox_ignorePlc.TabIndex = 8;
            this.checkBox_ignorePlc.Text = "屏蔽PLC";
            this.checkBox_ignorePlc.UseVisualStyleBackColor = true;
            this.checkBox_ignorePlc.Visible = false;
            // 
            // checkBox_tcpServer
            // 
            this.checkBox_tcpServer.AutoSize = true;
            this.checkBox_tcpServer.Location = new System.Drawing.Point(13, 541);
            this.checkBox_tcpServer.Name = "checkBox_tcpServer";
            this.checkBox_tcpServer.Size = new System.Drawing.Size(97, 22);
            this.checkBox_tcpServer.TabIndex = 9;
            this.checkBox_tcpServer.Text = "TCP接收";
            this.checkBox_tcpServer.UseVisualStyleBackColor = true;
            this.checkBox_tcpServer.CheckedChanged += new System.EventHandler(this.tcpServer_checkBox_CheckedChanged);
            // 
            // checkBox_connectPlc
            // 
            this.checkBox_connectPlc.AutoSize = true;
            this.checkBox_connectPlc.Location = new System.Drawing.Point(12, 393);
            this.checkBox_connectPlc.Name = "checkBox_connectPlc";
            this.checkBox_connectPlc.Size = new System.Drawing.Size(97, 22);
            this.checkBox_connectPlc.TabIndex = 11;
            this.checkBox_connectPlc.Text = "连接PLC";
            this.checkBox_connectPlc.UseVisualStyleBackColor = true;
            this.checkBox_connectPlc.CheckedChanged += new System.EventHandler(this.connectPlc_checkBox_CheckedChanged);
            // 
            // lable_PlcConnectStatus
            // 
            this.lable_PlcConnectStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lable_PlcConnectStatus.BackColor = System.Drawing.Color.Gray;
            this.lable_PlcConnectStatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable_PlcConnectStatus.ForeColor = System.Drawing.Color.White;
            this.lable_PlcConnectStatus.Location = new System.Drawing.Point(5, 448);
            this.lable_PlcConnectStatus.Name = "lable_PlcConnectStatus";
            this.lable_PlcConnectStatus.Size = new System.Drawing.Size(110, 40);
            this.lable_PlcConnectStatus.TabIndex = 14;
            this.lable_PlcConnectStatus.Text = "PLC状态";
            this.lable_PlcConnectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_ignoreCam
            // 
            this.checkBox_ignoreCam.AutoSize = true;
            this.checkBox_ignoreCam.Location = new System.Drawing.Point(12, 309);
            this.checkBox_ignoreCam.Name = "checkBox_ignoreCam";
            this.checkBox_ignoreCam.Size = new System.Drawing.Size(106, 22);
            this.checkBox_ignoreCam.TabIndex = 15;
            this.checkBox_ignoreCam.Text = "跳过相机";
            this.checkBox_ignoreCam.UseVisualStyleBackColor = true;
            this.checkBox_ignoreCam.CheckedChanged += new System.EventHandler(this.chkBox_ignoreCam_CheckedChanged);
            // 
            // statusStrip_mainForm
            // 
            this.statusStrip_mainForm.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip_mainForm.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip_mainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusTime,
            this.statusModule,
            this.statusLevel,
            this.statusMessage});
            this.statusStrip_mainForm.Location = new System.Drawing.Point(0, 665);
            this.statusStrip_mainForm.Name = "statusStrip_mainForm";
            this.statusStrip_mainForm.Size = new System.Drawing.Size(938, 39);
            this.statusStrip_mainForm.SizingGrip = false;
            this.statusStrip_mainForm.TabIndex = 16;
            this.statusStrip_mainForm.Text = "statusStrip1";
            // 
            // statusModule
            // 
            this.statusModule.Name = "statusModule";
            this.statusModule.Size = new System.Drawing.Size(103, 32);
            this.statusModule.Text = "[module]";
            // 
            // statusLevel
            // 
            this.statusLevel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusLevel.Name = "statusLevel";
            this.statusLevel.Size = new System.Drawing.Size(77, 32);
            this.statusLevel.Text = "[level]";
            // 
            // statusTime
            // 
            this.statusTime.BackColor = System.Drawing.SystemColors.ControlText;
            this.statusTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusTime.ForeColor = System.Drawing.SystemColors.Control;
            this.statusTime.Name = "statusTime";
            this.statusTime.Size = new System.Drawing.Size(0, 32);
            // 
            // statusMessage
            // 
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(697, 32);
            this.statusMessage.Spring = true;
            this.statusMessage.Text = "[message]";
            // 
            // mainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(938, 704);
            this.Controls.Add(this.statusStrip_mainForm);
            this.Controls.Add(this.checkBox_ignoreCam);
            this.Controls.Add(this.checkBox_ignoreCheck);
            this.Controls.Add(this.lable_PlcConnectStatus);
            this.Controls.Add(this.btn_openForm3);
            this.Controls.Add(this.checkBox_connectPlc);
            this.Controls.Add(this.checkBox_tcpServer);
            this.Controls.Add(this.checkBox_ignorePlc);
            this.Controls.Add(this.checkBox_autoMode);
            this.Controls.Add(this.btn_autoRun);
            this.Controls.Add(this.btn_LoadConfig);
            this.Controls.Add(this.btn_SaveIni);
            this.Controls.Add(this.btn_openForm2);
            this.Controls.Add(this.btn_openForm1);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.statusStrip_mainForm.ResumeLayout(false);
            this.statusStrip_mainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btn_openForm1;
        private System.Windows.Forms.Button btn_openForm2;
        private System.Windows.Forms.Button btn_openForm3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SaveIni;
        private System.Windows.Forms.Button btn_LoadConfig;
        private System.Windows.Forms.Button btn_autoRun;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox_autoMode;
        private System.Windows.Forms.CheckBox checkBox_ignorePlc;
        private System.Windows.Forms.CheckBox checkBox_tcpServer;
        private System.Windows.Forms.CheckBox checkBox_ignoreCheck;
        private System.Windows.Forms.CheckBox checkBox_connectPlc;
        private System.Windows.Forms.Label lable_PlcConnectStatus;
        private System.Windows.Forms.CheckBox checkBox_ignoreCam;
        private System.Windows.Forms.StatusStrip statusStrip_mainForm;
        private System.Windows.Forms.ToolStripStatusLabel statusModule;
        private System.Windows.Forms.ToolStripStatusLabel statusLevel;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
    }
}