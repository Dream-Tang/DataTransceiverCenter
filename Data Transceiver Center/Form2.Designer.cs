
namespace Data_Transceiver_Center
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btn_ReadDeviceRandom2 = new System.Windows.Forms.Button();
            this.btn_WriteDeviceRandom2 = new System.Windows.Forms.Button();
            this.txt_DeviceSizeRandom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_LogicalStationNumber = new System.Windows.Forms.TextBox();
            this.txt_ReturnCode = new System.Windows.Forms.TextBox();
            this.txt_Data = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad_ActUtlType = new System.Windows.Forms.RadioButton();
            this.axActUtlType1 = new AxActUtlTypeLib.AxActUtlType();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_DeviceNameRandom = new System.Windows.Forms.TextBox();
            this.txt_DeviceDataRandom = new System.Windows.Forms.TextBox();
            this.rd_CamAllow = new System.Windows.Forms.RadioButton();
            this.rd_CamOK = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rd_ScannerComplete = new System.Windows.Forms.RadioButton();
            this.rd_checkNG = new System.Windows.Forms.RadioButton();
            this.rd_checkOK = new System.Windows.Forms.RadioButton();
            this.rd_ScannerStart = new System.Windows.Forms.RadioButton();
            this.rd_PrtReady = new System.Windows.Forms.RadioButton();
            this.rd_PrtComplete = new System.Windows.Forms.RadioButton();
            this.rd_CamNG = new System.Windows.Forms.RadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Timer1Interval = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axActUtlType1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ReadDeviceRandom2
            // 
            this.btn_ReadDeviceRandom2.Location = new System.Drawing.Point(151, 158);
            this.btn_ReadDeviceRandom2.Name = "btn_ReadDeviceRandom2";
            this.btn_ReadDeviceRandom2.Size = new System.Drawing.Size(75, 50);
            this.btn_ReadDeviceRandom2.TabIndex = 0;
            this.btn_ReadDeviceRandom2.Text = "读出寄存器";
            this.btn_ReadDeviceRandom2.UseVisualStyleBackColor = true;
            this.btn_ReadDeviceRandom2.Click += new System.EventHandler(this.btn_ReadDeviceRandom2_Click);
            // 
            // btn_WriteDeviceRandom2
            // 
            this.btn_WriteDeviceRandom2.Location = new System.Drawing.Point(151, 230);
            this.btn_WriteDeviceRandom2.Name = "btn_WriteDeviceRandom2";
            this.btn_WriteDeviceRandom2.Size = new System.Drawing.Size(75, 50);
            this.btn_WriteDeviceRandom2.TabIndex = 1;
            this.btn_WriteDeviceRandom2.Text = "写入寄存器";
            this.btn_WriteDeviceRandom2.UseVisualStyleBackColor = true;
            this.btn_WriteDeviceRandom2.Click += new System.EventHandler(this.btn_WriteDeviceRandom2_Click);
            // 
            // txt_DeviceSizeRandom
            // 
            this.txt_DeviceSizeRandom.Location = new System.Drawing.Point(13, 174);
            this.txt_DeviceSizeRandom.Name = "txt_DeviceSizeRandom";
            this.txt_DeviceSizeRandom.Size = new System.Drawing.Size(100, 21);
            this.txt_DeviceSizeRandom.TabIndex = 4;
            this.txt_DeviceSizeRandom.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "PLC寄存器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "数据大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "写入数据";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "PLC站号";
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(201, 23);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(75, 23);
            this.btn_Open.TabIndex = 10;
            this.btn_Open.Text = "连接";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(201, 59);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "中断";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_LogicalStationNumber
            // 
            this.txt_LogicalStationNumber.Location = new System.Drawing.Point(91, 32);
            this.txt_LogicalStationNumber.Name = "txt_LogicalStationNumber";
            this.txt_LogicalStationNumber.Size = new System.Drawing.Size(64, 21);
            this.txt_LogicalStationNumber.TabIndex = 12;
            this.txt_LogicalStationNumber.Text = "1";
            // 
            // txt_ReturnCode
            // 
            this.txt_ReturnCode.Location = new System.Drawing.Point(299, 25);
            this.txt_ReturnCode.Name = "txt_ReturnCode";
            this.txt_ReturnCode.Size = new System.Drawing.Size(100, 21);
            this.txt_ReturnCode.TabIndex = 14;
            // 
            // txt_Data
            // 
            this.txt_Data.Location = new System.Drawing.Point(151, 118);
            this.txt_Data.Name = "txt_Data";
            this.txt_Data.Size = new System.Drawing.Size(100, 21);
            this.txt_Data.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "数据";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "通信回应";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rad_ActUtlType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_LogicalStationNumber);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 76);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // rad_ActUtlType
            // 
            this.rad_ActUtlType.AutoSize = true;
            this.rad_ActUtlType.Checked = true;
            this.rad_ActUtlType.Location = new System.Drawing.Point(6, 20);
            this.rad_ActUtlType.Name = "rad_ActUtlType";
            this.rad_ActUtlType.Size = new System.Drawing.Size(83, 16);
            this.rad_ActUtlType.TabIndex = 14;
            this.rad_ActUtlType.TabStop = true;
            this.rad_ActUtlType.Text = "ActUtlType";
            this.rad_ActUtlType.UseVisualStyleBackColor = true;
            // 
            // axActUtlType1
            // 
            this.axActUtlType1.Enabled = true;
            this.axActUtlType1.Location = new System.Drawing.Point(50, 50);
            this.axActUtlType1.Name = "axActUtlType1";
            this.axActUtlType1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axActUtlType1.OcxState")));
            this.axActUtlType1.Size = new System.Drawing.Size(32, 32);
            this.axActUtlType1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(411, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "通信结果";
            // 
            // txt_DeviceNameRandom
            // 
            this.txt_DeviceNameRandom.AcceptsReturn = true;
            this.txt_DeviceNameRandom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_DeviceNameRandom.Location = new System.Drawing.Point(13, 118);
            this.txt_DeviceNameRandom.Name = "txt_DeviceNameRandom";
            this.txt_DeviceNameRandom.Size = new System.Drawing.Size(100, 21);
            this.txt_DeviceNameRandom.TabIndex = 20;
            this.txt_DeviceNameRandom.Text = "D1000";
            // 
            // txt_DeviceDataRandom
            // 
            this.txt_DeviceDataRandom.Location = new System.Drawing.Point(13, 246);
            this.txt_DeviceDataRandom.Name = "txt_DeviceDataRandom";
            this.txt_DeviceDataRandom.Size = new System.Drawing.Size(100, 21);
            this.txt_DeviceDataRandom.TabIndex = 21;
            // 
            // rd_CamAllow
            // 
            this.rd_CamAllow.AutoSize = true;
            this.rd_CamAllow.Location = new System.Drawing.Point(5, 17);
            this.rd_CamAllow.Name = "rd_CamAllow";
            this.rd_CamAllow.Size = new System.Drawing.Size(95, 16);
            this.rd_CamAllow.TabIndex = 22;
            this.rd_CamAllow.TabStop = true;
            this.rd_CamAllow.Text = "允许相机拍照";
            this.rd_CamAllow.UseVisualStyleBackColor = true;
            this.rd_CamAllow.CheckedChanged += new System.EventHandler(this.rd_CamAllow_CheckedChanged);
            // 
            // rd_CamOK
            // 
            this.rd_CamOK.AutoSize = true;
            this.rd_CamOK.Location = new System.Drawing.Point(5, 39);
            this.rd_CamOK.Name = "rd_CamOK";
            this.rd_CamOK.Size = new System.Drawing.Size(83, 16);
            this.rd_CamOK.TabIndex = 23;
            this.rd_CamOK.TabStop = true;
            this.rd_CamOK.Text = "扫描OK放行";
            this.rd_CamOK.UseVisualStyleBackColor = true;
            this.rd_CamOK.CheckedChanged += new System.EventHandler(this.rd_CamOK_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.txt_Timer1Interval);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(299, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 219);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PLC控制台";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label13.Location = new System.Drawing.Point(61, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 34;
            this.label13.Text = "D1002";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label12.Location = new System.Drawing.Point(71, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "D1001";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(59, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "D1000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "拍码D1000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "打印机D1001";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "扫码D1002";
            // 
            // rd_ScannerComplete
            // 
            this.rd_ScannerComplete.AutoSize = true;
            this.rd_ScannerComplete.Location = new System.Drawing.Point(7, 37);
            this.rd_ScannerComplete.Name = "rd_ScannerComplete";
            this.rd_ScannerComplete.Size = new System.Drawing.Size(71, 16);
            this.rd_ScannerComplete.TabIndex = 30;
            this.rd_ScannerComplete.TabStop = true;
            this.rd_ScannerComplete.Text = "扫码完成";
            this.rd_ScannerComplete.UseVisualStyleBackColor = true;
            this.rd_ScannerComplete.CheckedChanged += new System.EventHandler(this.rd_ScannerComplete_CheckedChanged);
            // 
            // rd_checkNG
            // 
            this.rd_checkNG.AutoSize = true;
            this.rd_checkNG.Location = new System.Drawing.Point(7, 81);
            this.rd_checkNG.Name = "rd_checkNG";
            this.rd_checkNG.Size = new System.Drawing.Size(83, 16);
            this.rd_checkNG.TabIndex = 29;
            this.rd_checkNG.TabStop = true;
            this.rd_checkNG.Text = "判定数据NG";
            this.rd_checkNG.UseVisualStyleBackColor = true;
            this.rd_checkNG.CheckedChanged += new System.EventHandler(this.rd_checkNG_CheckedChanged);
            // 
            // rd_checkOK
            // 
            this.rd_checkOK.AutoSize = true;
            this.rd_checkOK.Location = new System.Drawing.Point(7, 59);
            this.rd_checkOK.Name = "rd_checkOK";
            this.rd_checkOK.Size = new System.Drawing.Size(83, 16);
            this.rd_checkOK.TabIndex = 28;
            this.rd_checkOK.TabStop = true;
            this.rd_checkOK.Text = "判定数据OK";
            this.rd_checkOK.UseVisualStyleBackColor = true;
            this.rd_checkOK.CheckedChanged += new System.EventHandler(this.rd_checkOK_CheckedChanged);
            // 
            // rd_ScannerStart
            // 
            this.rd_ScannerStart.AutoSize = true;
            this.rd_ScannerStart.Location = new System.Drawing.Point(7, 15);
            this.rd_ScannerStart.Name = "rd_ScannerStart";
            this.rd_ScannerStart.Size = new System.Drawing.Size(71, 16);
            this.rd_ScannerStart.TabIndex = 27;
            this.rd_ScannerStart.TabStop = true;
            this.rd_ScannerStart.Text = "扫码开始";
            this.rd_ScannerStart.UseVisualStyleBackColor = true;
            this.rd_ScannerStart.CheckedChanged += new System.EventHandler(this.rd_ScannerStart_CheckedChanged);
            // 
            // rd_PrtReady
            // 
            this.rd_PrtReady.AutoSize = true;
            this.rd_PrtReady.Location = new System.Drawing.Point(5, 24);
            this.rd_PrtReady.Name = "rd_PrtReady";
            this.rd_PrtReady.Size = new System.Drawing.Size(101, 16);
            this.rd_PrtReady.TabIndex = 26;
            this.rd_PrtReady.TabStop = true;
            this.rd_PrtReady.Text = "取标平台ready";
            this.rd_PrtReady.UseVisualStyleBackColor = true;
            this.rd_PrtReady.CheckedChanged += new System.EventHandler(this.rd_PrtReady_CheckedChanged);
            // 
            // rd_PrtComplete
            // 
            this.rd_PrtComplete.AutoSize = true;
            this.rd_PrtComplete.Location = new System.Drawing.Point(5, 46);
            this.rd_PrtComplete.Name = "rd_PrtComplete";
            this.rd_PrtComplete.Size = new System.Drawing.Size(83, 16);
            this.rd_PrtComplete.TabIndex = 25;
            this.rd_PrtComplete.TabStop = true;
            this.rd_PrtComplete.Text = "标打印完成";
            this.rd_PrtComplete.UseVisualStyleBackColor = true;
            this.rd_PrtComplete.CheckedChanged += new System.EventHandler(this.rd_PrtComplete_CheckedChanged);
            // 
            // rd_CamNG
            // 
            this.rd_CamNG.AutoSize = true;
            this.rd_CamNG.Location = new System.Drawing.Point(5, 61);
            this.rd_CamNG.Name = "rd_CamNG";
            this.rd_CamNG.Size = new System.Drawing.Size(83, 16);
            this.rd_CamNG.TabIndex = 24;
            this.rd_CamNG.TabStop = true;
            this.rd_CamNG.Text = "扫描NG报警";
            this.rd_CamNG.UseVisualStyleBackColor = true;
            this.rd_CamNG.CheckedChanged += new System.EventHandler(this.rd_CamNG_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(483, 46);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "自动刷新";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.rd_CamOK);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.rd_CamAllow);
            this.panel1.Controls.Add(this.rd_CamNG);
            this.panel1.Location = new System.Drawing.Point(15, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 100);
            this.panel1.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.rd_PrtComplete);
            this.panel2.Controls.Add(this.rd_PrtReady);
            this.panel2.Location = new System.Drawing.Point(15, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(107, 69);
            this.panel2.TabIndex = 29;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.rd_ScannerStart);
            this.panel3.Controls.Add(this.rd_checkOK);
            this.panel3.Controls.Add(this.rd_ScannerComplete);
            this.panel3.Controls.Add(this.rd_checkNG);
            this.panel3.Location = new System.Drawing.Point(134, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(107, 100);
            this.panel3.TabIndex = 29;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(135, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 12);
            this.label14.TabIndex = 28;
            this.label14.Text = "刷新间隔（ms）";
            // 
            // txt_Timer1Interval
            // 
            this.txt_Timer1Interval.Location = new System.Drawing.Point(134, 153);
            this.txt_Timer1Interval.Name = "txt_Timer1Interval";
            this.txt_Timer1Interval.Size = new System.Drawing.Size(83, 21);
            this.txt_Timer1Interval.TabIndex = 27;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 316);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txt_DeviceDataRandom);
            this.Controls.Add(this.txt_DeviceNameRandom);
            this.Controls.Add(this.axActUtlType1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Data);
            this.Controls.Add(this.txt_ReturnCode);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Open);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_DeviceSizeRandom);
            this.Controls.Add(this.btn_WriteDeviceRandom2);
            this.Controls.Add(this.btn_ReadDeviceRandom2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axActUtlType1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ReadDeviceRandom2;
        private System.Windows.Forms.Button btn_WriteDeviceRandom2;
        private System.Windows.Forms.TextBox txt_DeviceSizeRandom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_LogicalStationNumber;
        private System.Windows.Forms.TextBox txt_ReturnCode;
        private System.Windows.Forms.TextBox txt_Data;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rad_ActUtlType;

        private AxActUtlTypeLib.AxActUtlType axActUtlType1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_DeviceNameRandom;
        private System.Windows.Forms.TextBox txt_DeviceDataRandom;
        private System.Windows.Forms.RadioButton rd_CamAllow;
        private System.Windows.Forms.RadioButton rd_CamOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rd_ScannerComplete;
        private System.Windows.Forms.RadioButton rd_checkNG;
        private System.Windows.Forms.RadioButton rd_checkOK;
        private System.Windows.Forms.RadioButton rd_ScannerStart;
        private System.Windows.Forms.RadioButton rd_PrtReady;
        private System.Windows.Forms.RadioButton rd_PrtComplete;
        private System.Windows.Forms.RadioButton rd_CamNG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Timer1Interval;
    }
}