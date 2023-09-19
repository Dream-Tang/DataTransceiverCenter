
namespace Data_Transceiver_Center
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.veriCode_txtBox = new System.Windows.Forms.TextBox();
            this.mesId_txtBox = new System.Windows.Forms.TextBox();
            this.makeZpl_btn = new System.Windows.Forms.Button();
            this.sendToPrt_btn = new System.Windows.Forms.Button();
            this.mesCmd1_btn = new System.Windows.Forms.Button();
            this.label1_4 = new System.Windows.Forms.Label();
            this.label1_2 = new System.Windows.Forms.Label();
            this.zplPath_txtBox = new System.Windows.Forms.TextBox();
            this.label1_3 = new System.Windows.Forms.Label();
            this.prtPath_txtBox = new System.Windows.Forms.TextBox();
            this.label3_3 = new System.Windows.Forms.Label();
            this.serialRead_txtBox = new System.Windows.Forms.TextBox();
            this.label3_2 = new System.Windows.Forms.Label();
            this.label3_1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.openSerial_btn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3_4 = new System.Windows.Forms.Label();
            this.chckResult_txtBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.position_txtBox = new System.Windows.Forms.TextBox();
            this.label2_3 = new System.Windows.Forms.Label();
            this.label2_4 = new System.Windows.Forms.Label();
            this.label2_5 = new System.Windows.Forms.Label();
            this.fogId_txtBox = new System.Windows.Forms.TextBox();
            this.mesCmd2_btn = new System.Windows.Forms.Button();
            this.mesCmd3_btn = new System.Windows.Forms.Button();
            this.mesApi_txtBox = new System.Windows.Forms.TextBox();
            this.label2_1 = new System.Windows.Forms.Label();
            this.label1_5 = new System.Windows.Forms.Label();
            this.prtCode_txtBox = new System.Windows.Forms.TextBox();
            this.lastPrtCode_label = new System.Windows.Forms.Label();
            this.pnl_Mes = new System.Windows.Forms.Panel();
            this.label2_2 = new System.Windows.Forms.Label();
            this.JsonMsg_txtBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.mesAddr_txtBox = new System.Windows.Forms.TextBox();
            this.scnCode_txtBox = new System.Windows.Forms.TextBox();
            this.label1_6 = new System.Windows.Forms.Label();
            this.serialPort_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reloadPort_btn = new System.Windows.Forms.Button();
            this.camValue_label = new System.Windows.Forms.Label();
            this.prtValue_label = new System.Windows.Forms.Label();
            this.scnValue_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.runStatus_lable = new System.Windows.Forms.Label();
            this.veriCodeHistory_txtBox = new System.Windows.Forms.TextBox();
            this.autoRun_checkBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.apiTest_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_Mes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // veriCode_txtBox
            // 
            this.veriCode_txtBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.veriCode_txtBox.Location = new System.Drawing.Point(3, 25);
            this.veriCode_txtBox.Name = "veriCode_txtBox";
            this.veriCode_txtBox.Size = new System.Drawing.Size(152, 29);
            this.veriCode_txtBox.TabIndex = 0;
            // 
            // mesId_txtBox
            // 
            this.mesId_txtBox.Location = new System.Drawing.Point(8, 61);
            this.mesId_txtBox.Name = "mesId_txtBox";
            this.mesId_txtBox.Size = new System.Drawing.Size(142, 23);
            this.mesId_txtBox.TabIndex = 1;
            // 
            // makeZpl_btn
            // 
            this.makeZpl_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.makeZpl_btn.Location = new System.Drawing.Point(4, 177);
            this.makeZpl_btn.Name = "makeZpl_btn";
            this.makeZpl_btn.Size = new System.Drawing.Size(64, 26);
            this.makeZpl_btn.TabIndex = 2;
            this.makeZpl_btn.Text = "生成ZPL文件";
            this.makeZpl_btn.UseVisualStyleBackColor = true;
            this.makeZpl_btn.Click += new System.EventHandler(this.makeZpl_btn_Click);
            // 
            // sendToPrt_btn
            // 
            this.sendToPrt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendToPrt_btn.Location = new System.Drawing.Point(92, 177);
            this.sendToPrt_btn.Name = "sendToPrt_btn";
            this.sendToPrt_btn.Size = new System.Drawing.Size(64, 26);
            this.sendToPrt_btn.TabIndex = 3;
            this.sendToPrt_btn.Text = "发送ZPL文件";
            this.sendToPrt_btn.UseVisualStyleBackColor = true;
            this.sendToPrt_btn.Click += new System.EventHandler(this.sendToPrt_btn_Click);
            // 
            // mesCmd1_btn
            // 
            this.mesCmd1_btn.Location = new System.Drawing.Point(161, 8);
            this.mesCmd1_btn.Name = "mesCmd1_btn";
            this.mesCmd1_btn.Size = new System.Drawing.Size(75, 33);
            this.mesCmd1_btn.TabIndex = 7;
            this.mesCmd1_btn.Text = "MES通信1";
            this.mesCmd1_btn.UseVisualStyleBackColor = true;
            this.mesCmd1_btn.Click += new System.EventHandler(this.mesCmd1_btn_Click);
            // 
            // label1_4
            // 
            this.label1_4.AutoSize = true;
            this.label1_4.BackColor = System.Drawing.SystemColors.Info;
            this.label1_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_4.Location = new System.Drawing.Point(3, 1);
            this.label1_4.Name = "label1_4";
            this.label1_4.Size = new System.Drawing.Size(70, 19);
            this.label1_4.TabIndex = 10;
            this.label1_4.Text = "二维码读码";
            // 
            // label1_2
            // 
            this.label1_2.AutoSize = true;
            this.label1_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_2.Location = new System.Drawing.Point(5, 8);
            this.label1_2.Name = "label1_2";
            this.label1_2.Size = new System.Drawing.Size(73, 14);
            this.label1_2.TabIndex = 11;
            this.label1_2.Text = "ZPL文件路径";
            // 
            // zplPath_txtBox
            // 
            this.zplPath_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zplPath_txtBox.Location = new System.Drawing.Point(84, 5);
            this.zplPath_txtBox.Name = "zplPath_txtBox";
            this.zplPath_txtBox.Size = new System.Drawing.Size(346, 19);
            this.zplPath_txtBox.TabIndex = 12;
            // 
            // label1_3
            // 
            this.label1_3.AutoSize = true;
            this.label1_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_3.Location = new System.Drawing.Point(5, 32);
            this.label1_3.Name = "label1_3";
            this.label1_3.Size = new System.Drawing.Size(67, 14);
            this.label1_3.TabIndex = 13;
            this.label1_3.Text = "打印机地址";
            // 
            // prtPath_txtBox
            // 
            this.prtPath_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prtPath_txtBox.Location = new System.Drawing.Point(84, 30);
            this.prtPath_txtBox.Name = "prtPath_txtBox";
            this.prtPath_txtBox.Size = new System.Drawing.Size(346, 19);
            this.prtPath_txtBox.TabIndex = 14;
            // 
            // label3_3
            // 
            this.label3_3.AutoSize = true;
            this.label3_3.Location = new System.Drawing.Point(6, 177);
            this.label3_3.Name = "label3_3";
            this.label3_3.Size = new System.Drawing.Size(53, 12);
            this.label3_3.TabIndex = 25;
            this.label3_3.Text = "串口数据";
            // 
            // serialRead_txtBox
            // 
            this.serialRead_txtBox.Location = new System.Drawing.Point(5, 192);
            this.serialRead_txtBox.Name = "serialRead_txtBox";
            this.serialRead_txtBox.Size = new System.Drawing.Size(150, 21);
            this.serialRead_txtBox.TabIndex = 24;
            this.serialRead_txtBox.TextChanged += new System.EventHandler(this.serialRead_txtBox_TextChanged);
            // 
            // label3_2
            // 
            this.label3_2.AutoSize = true;
            this.label3_2.Location = new System.Drawing.Point(3, 98);
            this.label3_2.Name = "label3_2";
            this.label3_2.Size = new System.Drawing.Size(41, 12);
            this.label3_2.TabIndex = 22;
            this.label3_2.Text = "波特率";
            // 
            // label3_1
            // 
            this.label3_1.AutoSize = true;
            this.label3_1.Location = new System.Drawing.Point(3, 60);
            this.label3_1.Name = "label3_1";
            this.label3_1.Size = new System.Drawing.Size(41, 12);
            this.label3_1.TabIndex = 21;
            this.label3_1.Text = "端口号";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataRecived);
            // 
            // openSerial_btn
            // 
            this.openSerial_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openSerial_btn.Location = new System.Drawing.Point(77, 106);
            this.openSerial_btn.Name = "openSerial_btn";
            this.openSerial_btn.Size = new System.Drawing.Size(75, 32);
            this.openSerial_btn.TabIndex = 26;
            this.openSerial_btn.Text = "打开串口";
            this.openSerial_btn.UseVisualStyleBackColor = true;
            this.openSerial_btn.Click += new System.EventHandler(this.openSerial_btn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(2, 75);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(67, 20);
            this.comboBox1.TabIndex = 27;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBox2.Location = new System.Drawing.Point(2, 113);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(67, 20);
            this.comboBox2.TabIndex = 28;
            this.comboBox2.Text = "9600";
            // 
            // label3_4
            // 
            this.label3_4.AutoSize = true;
            this.label3_4.BackColor = System.Drawing.SystemColors.Info;
            this.label3_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3_4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3_4.Location = new System.Drawing.Point(3, 1);
            this.label3_4.Name = "label3_4";
            this.label3_4.Size = new System.Drawing.Size(58, 19);
            this.label3_4.TabIndex = 29;
            this.label3_4.Text = "校验结果";
            // 
            // chckResult_txtBox
            // 
            this.chckResult_txtBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chckResult_txtBox.Location = new System.Drawing.Point(2, 25);
            this.chckResult_txtBox.Name = "chckResult_txtBox";
            this.chckResult_txtBox.ReadOnly = true;
            this.chckResult_txtBox.Size = new System.Drawing.Size(185, 29);
            this.chckResult_txtBox.TabIndex = 30;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // position_txtBox
            // 
            this.position_txtBox.Location = new System.Drawing.Point(8, 18);
            this.position_txtBox.Name = "position_txtBox";
            this.position_txtBox.Size = new System.Drawing.Size(76, 23);
            this.position_txtBox.TabIndex = 33;
            // 
            // label2_3
            // 
            this.label2_3.AutoSize = true;
            this.label2_3.Location = new System.Drawing.Point(8, -1);
            this.label2_3.Name = "label2_3";
            this.label2_3.Size = new System.Drawing.Size(32, 17);
            this.label2_3.TabIndex = 34;
            this.label2_3.Text = "线别";
            // 
            // label2_4
            // 
            this.label2_4.AutoSize = true;
            this.label2_4.Location = new System.Drawing.Point(8, 43);
            this.label2_4.Name = "label2_4";
            this.label2_4.Size = new System.Drawing.Size(108, 17);
            this.label2_4.TabIndex = 35;
            this.label2_4.Text = "LINE ID(线体识别)";
            // 
            // label2_5
            // 
            this.label2_5.AutoSize = true;
            this.label2_5.Location = new System.Drawing.Point(8, 88);
            this.label2_5.Name = "label2_5";
            this.label2_5.Size = new System.Drawing.Size(94, 17);
            this.label2_5.TabIndex = 36;
            this.label2_5.Text = "FOG ID(打印码)";
            // 
            // fogId_txtBox
            // 
            this.fogId_txtBox.Location = new System.Drawing.Point(8, 103);
            this.fogId_txtBox.Name = "fogId_txtBox";
            this.fogId_txtBox.Size = new System.Drawing.Size(142, 23);
            this.fogId_txtBox.TabIndex = 37;
            this.fogId_txtBox.TextChanged += new System.EventHandler(this.fogId_txtBox_TextChanged);
            // 
            // mesCmd2_btn
            // 
            this.mesCmd2_btn.Location = new System.Drawing.Point(161, 52);
            this.mesCmd2_btn.Name = "mesCmd2_btn";
            this.mesCmd2_btn.Size = new System.Drawing.Size(75, 33);
            this.mesCmd2_btn.TabIndex = 40;
            this.mesCmd2_btn.Text = "MES通信2";
            this.mesCmd2_btn.UseVisualStyleBackColor = true;
            this.mesCmd2_btn.Click += new System.EventHandler(this.mesCmd2_btn_Click);
            // 
            // mesCmd3_btn
            // 
            this.mesCmd3_btn.Location = new System.Drawing.Point(161, 97);
            this.mesCmd3_btn.Name = "mesCmd3_btn";
            this.mesCmd3_btn.Size = new System.Drawing.Size(75, 33);
            this.mesCmd3_btn.TabIndex = 41;
            this.mesCmd3_btn.Text = "MES通信3";
            this.mesCmd3_btn.UseVisualStyleBackColor = true;
            this.mesCmd3_btn.Click += new System.EventHandler(this.mesCmd3_btn_Click);
            // 
            // mesApi_txtBox
            // 
            this.mesApi_txtBox.BackColor = System.Drawing.SystemColors.Info;
            this.mesApi_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mesApi_txtBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mesApi_txtBox.Location = new System.Drawing.Point(1, 18);
            this.mesApi_txtBox.Multiline = true;
            this.mesApi_txtBox.Name = "mesApi_txtBox";
            this.mesApi_txtBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.mesApi_txtBox.Size = new System.Drawing.Size(402, 86);
            this.mesApi_txtBox.TabIndex = 42;
            // 
            // label2_1
            // 
            this.label2_1.AutoSize = true;
            this.label2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_1.Location = new System.Drawing.Point(2, 2);
            this.label2_1.Name = "label2_1";
            this.label2_1.Size = new System.Drawing.Size(49, 14);
            this.label2_1.TabIndex = 43;
            this.label2_1.Text = "MES api";
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BackColor = System.Drawing.SystemColors.Info;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_5.Location = new System.Drawing.Point(3, 1);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(72, 19);
            this.label1_5.TabIndex = 46;
            this.label1_5.Text = "MES打印码";
            // 
            // prtCode_txtBox
            // 
            this.prtCode_txtBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prtCode_txtBox.Location = new System.Drawing.Point(3, 25);
            this.prtCode_txtBox.Name = "prtCode_txtBox";
            this.prtCode_txtBox.Size = new System.Drawing.Size(152, 29);
            this.prtCode_txtBox.TabIndex = 45;
            this.prtCode_txtBox.TextChanged += new System.EventHandler(this.prtCode_txtBox_TextChanged);
            // 
            // lastPrtCode_label
            // 
            this.lastPrtCode_label.BackColor = System.Drawing.SystemColors.Window;
            this.lastPrtCode_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lastPrtCode_label.Location = new System.Drawing.Point(4, 126);
            this.lastPrtCode_label.Name = "lastPrtCode_label";
            this.lastPrtCode_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lastPrtCode_label.Size = new System.Drawing.Size(152, 25);
            this.lastPrtCode_label.TabIndex = 47;
            this.lastPrtCode_label.Text = "上次打印码";
            this.lastPrtCode_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Mes
            // 
            this.pnl_Mes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Mes.Controls.Add(this.label2_2);
            this.pnl_Mes.Controls.Add(this.JsonMsg_txtBox);
            this.pnl_Mes.Controls.Add(this.panel1);
            this.pnl_Mes.Controls.Add(this.label2_1);
            this.pnl_Mes.Controls.Add(this.mesApi_txtBox);
            this.pnl_Mes.Location = new System.Drawing.Point(3, 288);
            this.pnl_Mes.Name = "pnl_Mes";
            this.pnl_Mes.Size = new System.Drawing.Size(572, 248);
            this.pnl_Mes.TabIndex = 51;
            // 
            // label2_2
            // 
            this.label2_2.AutoSize = true;
            this.label2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_2.Location = new System.Drawing.Point(408, 3);
            this.label2_2.Name = "label2_2";
            this.label2_2.Size = new System.Drawing.Size(55, 14);
            this.label2_2.TabIndex = 53;
            this.label2_2.Text = "JSON消息";
            // 
            // JsonMsg_txtBox
            // 
            this.JsonMsg_txtBox.BackColor = System.Drawing.SystemColors.Info;
            this.JsonMsg_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JsonMsg_txtBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.JsonMsg_txtBox.Location = new System.Drawing.Point(406, 18);
            this.JsonMsg_txtBox.Multiline = true;
            this.JsonMsg_txtBox.Name = "JsonMsg_txtBox";
            this.JsonMsg_txtBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.JsonMsg_txtBox.Size = new System.Drawing.Size(161, 225);
            this.JsonMsg_txtBox.TabIndex = 52;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.apiTest_btn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.mesAddr_txtBox);
            this.panel1.Controls.Add(this.label2_3);
            this.panel1.Controls.Add(this.mesCmd2_btn);
            this.panel1.Controls.Add(this.fogId_txtBox);
            this.panel1.Controls.Add(this.mesCmd3_btn);
            this.panel1.Controls.Add(this.label2_5);
            this.panel1.Controls.Add(this.position_txtBox);
            this.panel1.Controls.Add(this.label2_4);
            this.panel1.Controls.Add(this.mesCmd1_btn);
            this.panel1.Controls.Add(this.mesId_txtBox);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 136);
            this.panel1.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "MES 服务器地址";
            // 
            // mesAddr_txtBox
            // 
            this.mesAddr_txtBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.mesAddr_txtBox.Location = new System.Drawing.Point(268, 20);
            this.mesAddr_txtBox.Name = "mesAddr_txtBox";
            this.mesAddr_txtBox.Size = new System.Drawing.Size(120, 23);
            this.mesAddr_txtBox.TabIndex = 42;
            this.mesAddr_txtBox.Text = "192.168.50.7:7199";
            // 
            // scnCode_txtBox
            // 
            this.scnCode_txtBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scnCode_txtBox.Location = new System.Drawing.Point(3, 25);
            this.scnCode_txtBox.Name = "scnCode_txtBox";
            this.scnCode_txtBox.Size = new System.Drawing.Size(152, 29);
            this.scnCode_txtBox.TabIndex = 52;
            this.scnCode_txtBox.TextChanged += new System.EventHandler(this.scnCode_txtBox_TextChanged);
            // 
            // label1_6
            // 
            this.label1_6.AutoSize = true;
            this.label1_6.BackColor = System.Drawing.SystemColors.Info;
            this.label1_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_6.Location = new System.Drawing.Point(3, 1);
            this.label1_6.Name = "label1_6";
            this.label1_6.Size = new System.Drawing.Size(70, 19);
            this.label1_6.TabIndex = 53;
            this.label1_6.Text = "校验扫码枪";
            // 
            // serialPort_label
            // 
            this.serialPort_label.AutoSize = true;
            this.serialPort_label.BackColor = System.Drawing.SystemColors.ControlText;
            this.serialPort_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serialPort_label.ForeColor = System.Drawing.SystemColors.Control;
            this.serialPort_label.Location = new System.Drawing.Point(4, 141);
            this.serialPort_label.Name = "serialPort_label";
            this.serialPort_label.Size = new System.Drawing.Size(65, 20);
            this.serialPort_label.TabIndex = 54;
            this.serialPort_label.Text = "串口已关";
            this.serialPort_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.camValue_label);
            this.panel2.Controls.Add(this.prtValue_label);
            this.panel2.Controls.Add(this.scnValue_label);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(578, 288);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 248);
            this.panel2.TabIndex = 55;
            // 
            // reloadPort_btn
            // 
            this.reloadPort_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reloadPort_btn.Location = new System.Drawing.Point(77, 68);
            this.reloadPort_btn.Name = "reloadPort_btn";
            this.reloadPort_btn.Size = new System.Drawing.Size(75, 32);
            this.reloadPort_btn.TabIndex = 55;
            this.reloadPort_btn.Text = "刷新串口号";
            this.reloadPort_btn.UseVisualStyleBackColor = true;
            this.reloadPort_btn.Click += new System.EventHandler(this.reloadPort_btn_Click);
            // 
            // camValue_label
            // 
            this.camValue_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.camValue_label.Location = new System.Drawing.Point(59, 57);
            this.camValue_label.Name = "camValue_label";
            this.camValue_label.Size = new System.Drawing.Size(20, 17);
            this.camValue_label.TabIndex = 56;
            this.camValue_label.Text = "camValue";
            this.camValue_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prtValue_label
            // 
            this.prtValue_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prtValue_label.Location = new System.Drawing.Point(59, 39);
            this.prtValue_label.Name = "prtValue_label";
            this.prtValue_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.prtValue_label.Size = new System.Drawing.Size(20, 14);
            this.prtValue_label.TabIndex = 57;
            this.prtValue_label.Text = "prtValue";
            this.prtValue_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scnValue_label
            // 
            this.scnValue_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scnValue_label.Location = new System.Drawing.Point(59, 21);
            this.scnValue_label.Name = "scnValue_label";
            this.scnValue_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scnValue_label.Size = new System.Drawing.Size(20, 14);
            this.scnValue_label.TabIndex = 58;
            this.scnValue_label.Text = "scnValue";
            this.scnValue_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(4, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // runStatus_lable
            // 
            this.runStatus_lable.AutoSize = true;
            this.runStatus_lable.BackColor = System.Drawing.SystemColors.ControlText;
            this.runStatus_lable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.runStatus_lable.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.runStatus_lable.ForeColor = System.Drawing.SystemColors.Control;
            this.runStatus_lable.Location = new System.Drawing.Point(443, 9);
            this.runStatus_lable.Name = "runStatus_lable";
            this.runStatus_lable.Size = new System.Drawing.Size(95, 22);
            this.runStatus_lable.TabIndex = 31;
            this.runStatus_lable.Text = "自动运行状态";
            // 
            // veriCodeHistory_txtBox
            // 
            this.veriCodeHistory_txtBox.Location = new System.Drawing.Point(5, 60);
            this.veriCodeHistory_txtBox.Multiline = true;
            this.veriCodeHistory_txtBox.Name = "veriCodeHistory_txtBox";
            this.veriCodeHistory_txtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.veriCodeHistory_txtBox.Size = new System.Drawing.Size(151, 153);
            this.veriCodeHistory_txtBox.TabIndex = 60;
            this.veriCodeHistory_txtBox.TextChanged += new System.EventHandler(this.veriCodeHistory_txtBox_TextChanged);
            // 
            // autoRun_checkBox
            // 
            this.autoRun_checkBox.AutoSize = true;
            this.autoRun_checkBox.Checked = true;
            this.autoRun_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRun_checkBox.Location = new System.Drawing.Point(443, 35);
            this.autoRun_checkBox.Name = "autoRun_checkBox";
            this.autoRun_checkBox.Size = new System.Drawing.Size(72, 16);
            this.autoRun_checkBox.TabIndex = 61;
            this.autoRun_checkBox.Text = "锁定设置";
            this.autoRun_checkBox.UseVisualStyleBackColor = true;
            this.autoRun_checkBox.CheckedChanged += new System.EventHandler(this.autoRun_checkBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 64;
            this.label2.Text = "scnValue";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 63;
            this.label3.Text = "prtValue";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 62;
            this.label4.Text = "camValue";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label1_4);
            this.panel3.Controls.Add(this.veriCode_txtBox);
            this.panel3.Controls.Add(this.veriCodeHistory_txtBox);
            this.panel3.Location = new System.Drawing.Point(6, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(163, 220);
            this.panel3.TabIndex = 65;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.prtCode_txtBox);
            this.panel4.Controls.Add(this.label1_5);
            this.panel4.Controls.Add(this.lastPrtCode_label);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.makeZpl_btn);
            this.panel4.Controls.Add(this.sendToPrt_btn);
            this.panel4.Location = new System.Drawing.Point(173, 57);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(163, 220);
            this.panel4.TabIndex = 66;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.serialRead_txtBox);
            this.panel5.Controls.Add(this.serialPort_label);
            this.panel5.Controls.Add(this.reloadPort_btn);
            this.panel5.Controls.Add(this.label3_3);
            this.panel5.Controls.Add(this.label1_6);
            this.panel5.Controls.Add(this.openSerial_btn);
            this.panel5.Controls.Add(this.scnCode_txtBox);
            this.panel5.Controls.Add(this.label3_1);
            this.panel5.Controls.Add(this.comboBox2);
            this.panel5.Controls.Add(this.label3_2);
            this.panel5.Controls.Add(this.comboBox1);
            this.panel5.Location = new System.Drawing.Point(340, 57);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(163, 220);
            this.panel5.TabIndex = 67;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label3_4);
            this.panel6.Controls.Add(this.chckResult_txtBox);
            this.panel6.Location = new System.Drawing.Point(507, 57);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(195, 220);
            this.panel6.TabIndex = 68;
            // 
            // apiTest_btn
            // 
            this.apiTest_btn.Location = new System.Drawing.Point(268, 52);
            this.apiTest_btn.Name = "apiTest_btn";
            this.apiTest_btn.Size = new System.Drawing.Size(75, 33);
            this.apiTest_btn.TabIndex = 44;
            this.apiTest_btn.Text = "Api Test";
            this.apiTest_btn.UseVisualStyleBackColor = true;
            this.apiTest_btn.Click += new System.EventHandler(this.apiTest_btn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 65;
            this.label5.Text = "PLC状态";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(715, 540);
            this.ControlBox = false;
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.autoRun_checkBox);
            this.Controls.Add(this.pnl_Mes);
            this.Controls.Add(this.runStatus_lable);
            this.Controls.Add(this.prtPath_txtBox);
            this.Controls.Add(this.label1_3);
            this.Controls.Add(this.zplPath_txtBox);
            this.Controls.Add(this.label1_2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TextChanged += new System.EventHandler(this.veriCodeHistory_txtBox_TextChanged);
            this.pnl_Mes.ResumeLayout(false);
            this.pnl_Mes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox veriCode_txtBox;
        private System.Windows.Forms.TextBox mesId_txtBox;
        private System.Windows.Forms.Button makeZpl_btn;
        private System.Windows.Forms.Button sendToPrt_btn;
        private System.Windows.Forms.Button mesCmd1_btn;
        private System.Windows.Forms.Label label1_4;
        private System.Windows.Forms.Label label1_2;
        private System.Windows.Forms.TextBox zplPath_txtBox;
        private System.Windows.Forms.Label label1_3;
        private System.Windows.Forms.TextBox prtPath_txtBox;
        private System.Windows.Forms.Label label3_3;
        private System.Windows.Forms.TextBox serialRead_txtBox;
        private System.Windows.Forms.Label label3_2;
        private System.Windows.Forms.Label label3_1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button openSerial_btn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3_4;
        private System.Windows.Forms.TextBox chckResult_txtBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox position_txtBox;
        private System.Windows.Forms.Label label2_3;
        private System.Windows.Forms.Label label2_4;
        private System.Windows.Forms.Label label2_5;
        private System.Windows.Forms.TextBox fogId_txtBox;
        private System.Windows.Forms.Button mesCmd2_btn;
        private System.Windows.Forms.Button mesCmd3_btn;
        private System.Windows.Forms.TextBox mesApi_txtBox;
        private System.Windows.Forms.Label label2_1;
        private System.Windows.Forms.Label label1_5;
        private System.Windows.Forms.TextBox prtCode_txtBox;
        private System.Windows.Forms.Label lastPrtCode_label;
        //private System.Windows.Forms.Button getJsonButton;
        //private System.Windows.Forms.Button convertJsonButton;
        private System.Windows.Forms.Panel pnl_Mes;
        private System.Windows.Forms.TextBox JsonMsg_txtBox;
        private System.Windows.Forms.Label label2_2;
        private System.Windows.Forms.TextBox scnCode_txtBox;
        private System.Windows.Forms.Label label1_6;
        private System.Windows.Forms.Label serialPort_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button reloadPort_btn;
        private System.Windows.Forms.Label camValue_label;
        private System.Windows.Forms.Label prtValue_label;
        private System.Windows.Forms.Label scnValue_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label runStatus_lable;
        private System.Windows.Forms.CheckBox autoRun_checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mesAddr_txtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button apiTest_btn;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox veriCodeHistory_txtBox;
    }
}

