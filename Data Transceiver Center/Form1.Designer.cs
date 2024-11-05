
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtBox_veriCode = new System.Windows.Forms.TextBox();
            this.txtBox_mesId = new System.Windows.Forms.TextBox();
            this.btn_makeZpl = new System.Windows.Forms.Button();
            this.btn_sendToPrt = new System.Windows.Forms.Button();
            this.btn_mesCmd1 = new System.Windows.Forms.Button();
            this.label1_1 = new System.Windows.Forms.Label();
            this.label1_2 = new System.Windows.Forms.Label();
            this.txtBox_zplPath = new System.Windows.Forms.TextBox();
            this.label1_3 = new System.Windows.Forms.Label();
            this.txtBox_prtPath = new System.Windows.Forms.TextBox();
            this.txtBox_serialRead = new System.Windows.Forms.TextBox();
            this.label3_2 = new System.Windows.Forms.Label();
            this.label3_1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btn_openSerial = new System.Windows.Forms.Button();
            this.cobBox_SeriPortNum = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtBox_position = new System.Windows.Forms.TextBox();
            this.label2_3 = new System.Windows.Forms.Label();
            this.label2_4 = new System.Windows.Forms.Label();
            this.label2_5 = new System.Windows.Forms.Label();
            this.txtBox_fogId = new System.Windows.Forms.TextBox();
            this.btn_mesCmd2 = new System.Windows.Forms.Button();
            this.btn_mesCmd3 = new System.Windows.Forms.Button();
            this.txtBox_mesApi = new System.Windows.Forms.TextBox();
            this.label2_1 = new System.Windows.Forms.Label();
            this.label1_5 = new System.Windows.Forms.Label();
            this.txtBox_prtCode = new System.Windows.Forms.TextBox();
            this.lastPrtCode_label = new System.Windows.Forms.Label();
            this.pnl_Mes = new System.Windows.Forms.Panel();
            this.label2_2 = new System.Windows.Forms.Label();
            this.txtBox_jsonMsg = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_apiTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_mesAddr = new System.Windows.Forms.TextBox();
            this.txtBox_scnCode = new System.Windows.Forms.TextBox();
            this.label1_6 = new System.Windows.Forms.Label();
            this.serialPort_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.camValue_label = new System.Windows.Forms.Label();
            this.prtValue_label = new System.Windows.Forms.Label();
            this.scnValue_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_reloadPort = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.runStatus_lable = new System.Windows.Forms.Label();
            this.txtBox_veriCodeHistory = new System.Windows.Forms.TextBox();
            this.lockSettings_checkBox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb_ReadCodeNote = new System.Windows.Forms.Label();
            this.lb_ReadCode = new System.Windows.Forms.Label();
            this.btn_RetryRead = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lb_PrtCodeNote = new System.Windows.Forms.Label();
            this.lb_PrtCode = new System.Windows.Forms.Label();
            this.label_zplTemp = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lb_ChkCode = new System.Windows.Forms.Label();
            this.lb_ChkCodeNote = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnl_Mes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBox_veriCode
            // 
            this.txtBox_veriCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_veriCode.Location = new System.Drawing.Point(4, 66);
            this.txtBox_veriCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_veriCode.Name = "txtBox_veriCode";
            this.txtBox_veriCode.Size = new System.Drawing.Size(226, 29);
            this.txtBox_veriCode.TabIndex = 0;
            // 
            // txtBox_mesId
            // 
            this.txtBox_mesId.Location = new System.Drawing.Point(12, 92);
            this.txtBox_mesId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_mesId.Name = "txtBox_mesId";
            this.txtBox_mesId.Size = new System.Drawing.Size(211, 23);
            this.txtBox_mesId.TabIndex = 1;
            // 
            // btn_makeZpl
            // 
            this.btn_makeZpl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_makeZpl.Location = new System.Drawing.Point(132, 306);
            this.btn_makeZpl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_makeZpl.Name = "btn_makeZpl";
            this.btn_makeZpl.Size = new System.Drawing.Size(100, 39);
            this.btn_makeZpl.TabIndex = 2;
            this.btn_makeZpl.Text = "生成ZPL文件";
            this.btn_makeZpl.UseVisualStyleBackColor = true;
            this.btn_makeZpl.Click += new System.EventHandler(this.makeZpl_btn_Click);
            // 
            // btn_sendToPrt
            // 
            this.btn_sendToPrt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sendToPrt.Location = new System.Drawing.Point(6, 356);
            this.btn_sendToPrt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_sendToPrt.Name = "btn_sendToPrt";
            this.btn_sendToPrt.Size = new System.Drawing.Size(226, 39);
            this.btn_sendToPrt.TabIndex = 3;
            this.btn_sendToPrt.Text = "打印条码";
            this.btn_sendToPrt.UseVisualStyleBackColor = true;
            this.btn_sendToPrt.Click += new System.EventHandler(this.sendToPrt_btn_Click);
            // 
            // btn_mesCmd1
            // 
            this.btn_mesCmd1.Location = new System.Drawing.Point(242, 12);
            this.btn_mesCmd1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_mesCmd1.Name = "btn_mesCmd1";
            this.btn_mesCmd1.Size = new System.Drawing.Size(112, 50);
            this.btn_mesCmd1.TabIndex = 7;
            this.btn_mesCmd1.Text = "MES通信1";
            this.btn_mesCmd1.UseVisualStyleBackColor = true;
            this.btn_mesCmd1.Click += new System.EventHandler(this.mesCmd1_btn_Click);
            // 
            // label1_1
            // 
            this.label1_1.AutoSize = true;
            this.label1_1.BackColor = System.Drawing.SystemColors.Info;
            this.label1_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_1.Location = new System.Drawing.Point(4, 2);
            this.label1_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1_1.Name = "label1_1";
            this.label1_1.Size = new System.Drawing.Size(58, 19);
            this.label1_1.TabIndex = 10;
            this.label1_1.Text = "读玻璃码";
            // 
            // label1_2
            // 
            this.label1_2.AutoSize = true;
            this.label1_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_2.Location = new System.Drawing.Point(8, 12);
            this.label1_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1_2.Name = "label1_2";
            this.label1_2.Size = new System.Drawing.Size(109, 20);
            this.label1_2.TabIndex = 11;
            this.label1_2.Text = "ZPL文件路径";
            // 
            // txtBox_zplPath
            // 
            this.txtBox_zplPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_zplPath.Location = new System.Drawing.Point(126, 8);
            this.txtBox_zplPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_zplPath.Name = "txtBox_zplPath";
            this.txtBox_zplPath.Size = new System.Drawing.Size(517, 19);
            this.txtBox_zplPath.TabIndex = 12;
            // 
            // label1_3
            // 
            this.label1_3.AutoSize = true;
            this.label1_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_3.Location = new System.Drawing.Point(8, 48);
            this.label1_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1_3.Name = "label1_3";
            this.label1_3.Size = new System.Drawing.Size(100, 20);
            this.label1_3.TabIndex = 13;
            this.label1_3.Text = "打印机地址";
            // 
            // txtBox_prtPath
            // 
            this.txtBox_prtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_prtPath.Location = new System.Drawing.Point(126, 45);
            this.txtBox_prtPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_prtPath.Name = "txtBox_prtPath";
            this.txtBox_prtPath.Size = new System.Drawing.Size(517, 19);
            this.txtBox_prtPath.TabIndex = 14;
            // 
            // txtBox_serialRead
            // 
            this.txtBox_serialRead.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_serialRead.Location = new System.Drawing.Point(3, 302);
            this.txtBox_serialRead.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_serialRead.Name = "txtBox_serialRead";
            this.txtBox_serialRead.Size = new System.Drawing.Size(229, 29);
            this.txtBox_serialRead.TabIndex = 24;
            this.txtBox_serialRead.TextChanged += new System.EventHandler(this.serialRead_txtBox_TextChanged);
            // 
            // label3_2
            // 
            this.label3_2.AutoSize = true;
            this.label3_2.Location = new System.Drawing.Point(3, 174);
            this.label3_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3_2.Name = "label3_2";
            this.label3_2.Size = new System.Drawing.Size(62, 18);
            this.label3_2.TabIndex = 22;
            this.label3_2.Text = "波特率";
            // 
            // label3_1
            // 
            this.label3_1.AutoSize = true;
            this.label3_1.Location = new System.Drawing.Point(3, 117);
            this.label3_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3_1.Name = "label3_1";
            this.label3_1.Size = new System.Drawing.Size(62, 18);
            this.label3_1.TabIndex = 21;
            this.label3_1.Text = "端口号";
            // 
            // serialPort1
            // 
            this.serialPort1.ParityReplace = ((byte)(0));
            this.serialPort1.ReadTimeout = 2000;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataRecived);
            // 
            // btn_openSerial
            // 
            this.btn_openSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_openSerial.Location = new System.Drawing.Point(114, 186);
            this.btn_openSerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_openSerial.Name = "btn_openSerial";
            this.btn_openSerial.Size = new System.Drawing.Size(112, 48);
            this.btn_openSerial.TabIndex = 26;
            this.btn_openSerial.Text = "打开串口";
            this.btn_openSerial.UseVisualStyleBackColor = true;
            this.btn_openSerial.Click += new System.EventHandler(this.openSerial_btn_Click);
            // 
            // cobBox_SeriPortNum
            // 
            this.cobBox_SeriPortNum.FormattingEnabled = true;
            this.cobBox_SeriPortNum.Location = new System.Drawing.Point(2, 140);
            this.cobBox_SeriPortNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cobBox_SeriPortNum.Name = "cobBox_SeriPortNum";
            this.cobBox_SeriPortNum.Size = new System.Drawing.Size(98, 26);
            this.cobBox_SeriPortNum.TabIndex = 27;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBox2.Location = new System.Drawing.Point(2, 196);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(98, 26);
            this.comboBox2.TabIndex = 28;
            this.comboBox2.Text = "9600";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtBox_position
            // 
            this.txtBox_position.Location = new System.Drawing.Point(12, 27);
            this.txtBox_position.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_position.Name = "txtBox_position";
            this.txtBox_position.Size = new System.Drawing.Size(112, 23);
            this.txtBox_position.TabIndex = 33;
            // 
            // label2_3
            // 
            this.label2_3.AutoSize = true;
            this.label2_3.Location = new System.Drawing.Point(12, -2);
            this.label2_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2_3.Name = "label2_3";
            this.label2_3.Size = new System.Drawing.Size(32, 17);
            this.label2_3.TabIndex = 34;
            this.label2_3.Text = "线别";
            // 
            // label2_4
            // 
            this.label2_4.AutoSize = true;
            this.label2_4.Location = new System.Drawing.Point(12, 64);
            this.label2_4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2_4.Name = "label2_4";
            this.label2_4.Size = new System.Drawing.Size(108, 17);
            this.label2_4.TabIndex = 35;
            this.label2_4.Text = "LINE ID(线体识别)";
            // 
            // label2_5
            // 
            this.label2_5.AutoSize = true;
            this.label2_5.Location = new System.Drawing.Point(12, 132);
            this.label2_5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2_5.Name = "label2_5";
            this.label2_5.Size = new System.Drawing.Size(94, 17);
            this.label2_5.TabIndex = 36;
            this.label2_5.Text = "FOG ID(打印码)";
            // 
            // txtBox_fogId
            // 
            this.txtBox_fogId.Location = new System.Drawing.Point(12, 154);
            this.txtBox_fogId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_fogId.Name = "txtBox_fogId";
            this.txtBox_fogId.Size = new System.Drawing.Size(211, 23);
            this.txtBox_fogId.TabIndex = 37;
            this.txtBox_fogId.TextChanged += new System.EventHandler(this.fogId_txtBox_TextChanged);
            // 
            // btn_mesCmd2
            // 
            this.btn_mesCmd2.Location = new System.Drawing.Point(242, 78);
            this.btn_mesCmd2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_mesCmd2.Name = "btn_mesCmd2";
            this.btn_mesCmd2.Size = new System.Drawing.Size(112, 50);
            this.btn_mesCmd2.TabIndex = 40;
            this.btn_mesCmd2.Text = "MES通信2";
            this.btn_mesCmd2.UseVisualStyleBackColor = true;
            this.btn_mesCmd2.Click += new System.EventHandler(this.mesCmd2_btn_Click);
            // 
            // btn_mesCmd3
            // 
            this.btn_mesCmd3.Location = new System.Drawing.Point(242, 146);
            this.btn_mesCmd3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_mesCmd3.Name = "btn_mesCmd3";
            this.btn_mesCmd3.Size = new System.Drawing.Size(112, 50);
            this.btn_mesCmd3.TabIndex = 41;
            this.btn_mesCmd3.Text = "MES通信3";
            this.btn_mesCmd3.UseVisualStyleBackColor = true;
            this.btn_mesCmd3.Click += new System.EventHandler(this.mesCmd3_btn_Click);
            // 
            // txtBox_mesApi
            // 
            this.txtBox_mesApi.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_mesApi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_mesApi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_mesApi.Location = new System.Drawing.Point(2, 27);
            this.txtBox_mesApi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_mesApi.Multiline = true;
            this.txtBox_mesApi.Name = "txtBox_mesApi";
            this.txtBox_mesApi.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_mesApi.Size = new System.Drawing.Size(598, 128);
            this.txtBox_mesApi.TabIndex = 42;
            // 
            // label2_1
            // 
            this.label2_1.AutoSize = true;
            this.label2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_1.Location = new System.Drawing.Point(3, 3);
            this.label2_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2_1.Name = "label2_1";
            this.label2_1.Size = new System.Drawing.Size(127, 20);
            this.label2_1.TabIndex = 43;
            this.label2_1.Text = "发给MES的消息";
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BackColor = System.Drawing.SystemColors.Info;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_5.Location = new System.Drawing.Point(4, 2);
            this.label1_5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(58, 19);
            this.label1_5.TabIndex = 46;
            this.label1_5.Text = "打印条码";
            // 
            // txtBox_prtCode
            // 
            this.txtBox_prtCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_prtCode.Location = new System.Drawing.Point(9, 66);
            this.txtBox_prtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_prtCode.Name = "txtBox_prtCode";
            this.txtBox_prtCode.Size = new System.Drawing.Size(226, 29);
            this.txtBox_prtCode.TabIndex = 45;
            this.txtBox_prtCode.TextChanged += new System.EventHandler(this.prtCode_txtBox_TextChanged);
            // 
            // lastPrtCode_label
            // 
            this.lastPrtCode_label.BackColor = System.Drawing.SystemColors.Window;
            this.lastPrtCode_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lastPrtCode_label.Location = new System.Drawing.Point(4, 204);
            this.lastPrtCode_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lastPrtCode_label.Name = "lastPrtCode_label";
            this.lastPrtCode_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lastPrtCode_label.Size = new System.Drawing.Size(228, 38);
            this.lastPrtCode_label.TabIndex = 47;
            this.lastPrtCode_label.Text = "上次打印码";
            this.lastPrtCode_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Mes
            // 
            this.pnl_Mes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Mes.Controls.Add(this.label2_2);
            this.pnl_Mes.Controls.Add(this.txtBox_jsonMsg);
            this.pnl_Mes.Controls.Add(this.panel1);
            this.pnl_Mes.Controls.Add(this.label2_1);
            this.pnl_Mes.Controls.Add(this.txtBox_mesApi);
            this.pnl_Mes.Location = new System.Drawing.Point(4, 540);
            this.pnl_Mes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_Mes.Name = "pnl_Mes";
            this.pnl_Mes.Size = new System.Drawing.Size(814, 371);
            this.pnl_Mes.TabIndex = 51;
            // 
            // label2_2
            // 
            this.label2_2.AutoSize = true;
            this.label2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_2.Location = new System.Drawing.Point(606, 4);
            this.label2_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2_2.Name = "label2_2";
            this.label2_2.Size = new System.Drawing.Size(127, 20);
            this.label2_2.TabIndex = 53;
            this.label2_2.Text = "MES回复的消息";
            // 
            // txtBox_jsonMsg
            // 
            this.txtBox_jsonMsg.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_jsonMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_jsonMsg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_jsonMsg.Location = new System.Drawing.Point(606, 27);
            this.txtBox_jsonMsg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_jsonMsg.Multiline = true;
            this.txtBox_jsonMsg.Name = "txtBox_jsonMsg";
            this.txtBox_jsonMsg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_jsonMsg.Size = new System.Drawing.Size(194, 336);
            this.txtBox_jsonMsg.TabIndex = 52;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_apiTest);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBox_mesAddr);
            this.panel1.Controls.Add(this.label2_3);
            this.panel1.Controls.Add(this.btn_mesCmd2);
            this.panel1.Controls.Add(this.txtBox_fogId);
            this.panel1.Controls.Add(this.btn_mesCmd3);
            this.panel1.Controls.Add(this.label2_5);
            this.panel1.Controls.Add(this.txtBox_position);
            this.panel1.Controls.Add(this.label2_4);
            this.panel1.Controls.Add(this.btn_mesCmd1);
            this.panel1.Controls.Add(this.txtBox_mesId);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 160);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 203);
            this.panel1.TabIndex = 55;
            // 
            // btn_apiTest
            // 
            this.btn_apiTest.Location = new System.Drawing.Point(405, 78);
            this.btn_apiTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_apiTest.Name = "btn_apiTest";
            this.btn_apiTest.Size = new System.Drawing.Size(112, 50);
            this.btn_apiTest.TabIndex = 44;
            this.btn_apiTest.Text = "Api Test";
            this.btn_apiTest.UseVisualStyleBackColor = true;
            this.btn_apiTest.Click += new System.EventHandler(this.apiTest_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "MES 服务器地址";
            // 
            // txtBox_mesAddr
            // 
            this.txtBox_mesAddr.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtBox_mesAddr.Location = new System.Drawing.Point(405, 30);
            this.txtBox_mesAddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_mesAddr.Name = "txtBox_mesAddr";
            this.txtBox_mesAddr.Size = new System.Drawing.Size(178, 23);
            this.txtBox_mesAddr.TabIndex = 42;
            this.txtBox_mesAddr.Text = "192.168.50.7:7199";
            // 
            // txtBox_scnCode
            // 
            this.txtBox_scnCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_scnCode.Location = new System.Drawing.Point(3, 66);
            this.txtBox_scnCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_scnCode.Name = "txtBox_scnCode";
            this.txtBox_scnCode.Size = new System.Drawing.Size(226, 29);
            this.txtBox_scnCode.TabIndex = 52;
            this.txtBox_scnCode.TextChanged += new System.EventHandler(this.scnCode_txtBox_TextChanged);
            // 
            // label1_6
            // 
            this.label1_6.AutoSize = true;
            this.label1_6.BackColor = System.Drawing.SystemColors.Info;
            this.label1_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_6.Location = new System.Drawing.Point(4, 2);
            this.label1_6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1_6.Name = "label1_6";
            this.label1_6.Size = new System.Drawing.Size(58, 19);
            this.label1_6.TabIndex = 53;
            this.label1_6.Text = "验码绑定";
            // 
            // serialPort_label
            // 
            this.serialPort_label.AutoSize = true;
            this.serialPort_label.BackColor = System.Drawing.SystemColors.ControlText;
            this.serialPort_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serialPort_label.ForeColor = System.Drawing.SystemColors.Control;
            this.serialPort_label.Location = new System.Drawing.Point(4, 244);
            this.serialPort_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.panel2.Location = new System.Drawing.Point(828, 540);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(142, 371);
            this.panel2.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 65;
            this.label5.Text = "PLC状态";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(82, 26);
            this.label3.TabIndex = 63;
            this.label3.Text = "prtValue";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // camValue_label
            // 
            this.camValue_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.camValue_label.Location = new System.Drawing.Point(88, 30);
            this.camValue_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.camValue_label.Name = "camValue_label";
            this.camValue_label.Size = new System.Drawing.Size(29, 24);
            this.camValue_label.TabIndex = 56;
            this.camValue_label.Text = "camValue";
            this.camValue_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prtValue_label
            // 
            this.prtValue_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prtValue_label.Location = new System.Drawing.Point(88, 58);
            this.prtValue_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.prtValue_label.Name = "prtValue_label";
            this.prtValue_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.prtValue_label.Size = new System.Drawing.Size(29, 20);
            this.prtValue_label.TabIndex = 57;
            this.prtValue_label.Text = "prtValue";
            this.prtValue_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scnValue_label
            // 
            this.scnValue_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scnValue_label.Location = new System.Drawing.Point(88, 84);
            this.scnValue_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scnValue_label.Name = "scnValue_label";
            this.scnValue_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scnValue_label.Size = new System.Drawing.Size(29, 20);
            this.scnValue_label.TabIndex = 58;
            this.scnValue_label.Text = "scnValue";
            this.scnValue_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 26);
            this.label4.TabIndex = 62;
            this.label4.Text = "camValue";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(82, 26);
            this.label2.TabIndex = 64;
            this.label2.Text = "scnValue";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_reloadPort
            // 
            this.btn_reloadPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reloadPort.Location = new System.Drawing.Point(114, 129);
            this.btn_reloadPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_reloadPort.Name = "btn_reloadPort";
            this.btn_reloadPort.Size = new System.Drawing.Size(112, 48);
            this.btn_reloadPort.TabIndex = 55;
            this.btn_reloadPort.Text = "刷新串口号";
            this.btn_reloadPort.UseVisualStyleBackColor = true;
            this.btn_reloadPort.Click += new System.EventHandler(this.reloadPort_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(4, 118);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 73);
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
            this.runStatus_lable.Location = new System.Drawing.Point(664, 14);
            this.runStatus_lable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.runStatus_lable.Name = "runStatus_lable";
            this.runStatus_lable.Size = new System.Drawing.Size(95, 22);
            this.runStatus_lable.TabIndex = 31;
            this.runStatus_lable.Text = "自动运行状态";
            // 
            // txtBox_veriCodeHistory
            // 
            this.txtBox_veriCodeHistory.Location = new System.Drawing.Point(8, 118);
            this.txtBox_veriCodeHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox_veriCodeHistory.MaxLength = 0;
            this.txtBox_veriCodeHistory.Multiline = true;
            this.txtBox_veriCodeHistory.Name = "txtBox_veriCodeHistory";
            this.txtBox_veriCodeHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBox_veriCodeHistory.Size = new System.Drawing.Size(224, 228);
            this.txtBox_veriCodeHistory.TabIndex = 60;
            this.txtBox_veriCodeHistory.TextChanged += new System.EventHandler(this.veriCodeHistory_txtBox_TextChanged);
            this.txtBox_veriCodeHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtBox_veriCodeHistory_MouseDoubleClick);
            // 
            // lockSettings_checkBox
            // 
            this.lockSettings_checkBox.AutoSize = true;
            this.lockSettings_checkBox.Checked = true;
            this.lockSettings_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lockSettings_checkBox.Location = new System.Drawing.Point(664, 52);
            this.lockSettings_checkBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lockSettings_checkBox.Name = "lockSettings_checkBox";
            this.lockSettings_checkBox.Size = new System.Drawing.Size(99, 22);
            this.lockSettings_checkBox.TabIndex = 61;
            this.lockSettings_checkBox.Text = "锁定设置";
            this.lockSettings_checkBox.UseVisualStyleBackColor = true;
            this.lockSettings_checkBox.CheckedChanged += new System.EventHandler(this.lockSettings_checkBox_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lb_ReadCodeNote);
            this.panel3.Controls.Add(this.lb_ReadCode);
            this.panel3.Controls.Add(this.btn_RetryRead);
            this.panel3.Controls.Add(this.label1_1);
            this.panel3.Controls.Add(this.txtBox_veriCode);
            this.panel3.Controls.Add(this.txtBox_veriCodeHistory);
            this.panel3.Location = new System.Drawing.Point(9, 86);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 444);
            this.panel3.TabIndex = 65;
            // 
            // lb_ReadCodeNote
            // 
            this.lb_ReadCodeNote.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ReadCodeNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ReadCodeNote.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ReadCodeNote.Location = new System.Drawing.Point(-2, 402);
            this.lb_ReadCodeNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_ReadCodeNote.Name = "lb_ReadCodeNote";
            this.lb_ReadCodeNote.Size = new System.Drawing.Size(244, 40);
            this.lb_ReadCodeNote.TabIndex = 63;
            // 
            // lb_ReadCode
            // 
            this.lb_ReadCode.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ReadCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ReadCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ReadCode.Location = new System.Drawing.Point(4, 34);
            this.lb_ReadCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_ReadCode.Name = "lb_ReadCode";
            this.lb_ReadCode.Size = new System.Drawing.Size(228, 28);
            this.lb_ReadCode.TabIndex = 62;
            this.lb_ReadCode.Text = "读玻璃码";
            // 
            // btn_RetryRead
            // 
            this.btn_RetryRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RetryRead.Location = new System.Drawing.Point(8, 356);
            this.btn_RetryRead.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_RetryRead.Name = "btn_RetryRead";
            this.btn_RetryRead.Size = new System.Drawing.Size(225, 40);
            this.btn_RetryRead.TabIndex = 61;
            this.btn_RetryRead.Text = "手动读码";
            this.btn_RetryRead.UseVisualStyleBackColor = true;
            this.btn_RetryRead.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lb_PrtCodeNote);
            this.panel4.Controls.Add(this.lb_PrtCode);
            this.panel4.Controls.Add(this.label_zplTemp);
            this.panel4.Controls.Add(this.txtBox_prtCode);
            this.panel4.Controls.Add(this.label1_5);
            this.panel4.Controls.Add(this.lastPrtCode_label);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.btn_makeZpl);
            this.panel4.Controls.Add(this.btn_sendToPrt);
            this.panel4.Location = new System.Drawing.Point(362, 84);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(244, 444);
            this.panel4.TabIndex = 66;
            // 
            // lb_PrtCodeNote
            // 
            this.lb_PrtCodeNote.BackColor = System.Drawing.SystemColors.Info;
            this.lb_PrtCodeNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PrtCodeNote.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_PrtCodeNote.Location = new System.Drawing.Point(-2, 400);
            this.lb_PrtCodeNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_PrtCodeNote.Name = "lb_PrtCodeNote";
            this.lb_PrtCodeNote.Size = new System.Drawing.Size(244, 41);
            this.lb_PrtCodeNote.TabIndex = 64;
            // 
            // lb_PrtCode
            // 
            this.lb_PrtCode.BackColor = System.Drawing.SystemColors.Info;
            this.lb_PrtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PrtCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_PrtCode.Location = new System.Drawing.Point(3, 34);
            this.lb_PrtCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_PrtCode.Name = "lb_PrtCode";
            this.lb_PrtCode.Size = new System.Drawing.Size(228, 28);
            this.lb_PrtCode.TabIndex = 63;
            this.lb_PrtCode.Text = "打印条码";
            // 
            // label_zplTemp
            // 
            this.label_zplTemp.BackColor = System.Drawing.SystemColors.Control;
            this.label_zplTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_zplTemp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_zplTemp.Location = new System.Drawing.Point(6, 306);
            this.label_zplTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_zplTemp.Name = "label_zplTemp";
            this.label_zplTemp.Size = new System.Drawing.Size(96, 38);
            this.label_zplTemp.TabIndex = 61;
            this.label_zplTemp.Text = "加载模板";
            this.label_zplTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_zplTemp.Click += new System.EventHandler(this.btn_loadZpl_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lb_ChkCode);
            this.panel5.Controls.Add(this.lb_ChkCodeNote);
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.txtBox_serialRead);
            this.panel5.Controls.Add(this.serialPort_label);
            this.panel5.Controls.Add(this.btn_reloadPort);
            this.panel5.Controls.Add(this.label1_6);
            this.panel5.Controls.Add(this.btn_openSerial);
            this.panel5.Controls.Add(this.txtBox_scnCode);
            this.panel5.Controls.Add(this.label3_1);
            this.panel5.Controls.Add(this.comboBox2);
            this.panel5.Controls.Add(this.label3_2);
            this.panel5.Controls.Add(this.cobBox_SeriPortNum);
            this.panel5.Location = new System.Drawing.Point(726, 84);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(244, 444);
            this.panel5.TabIndex = 67;
            // 
            // lb_ChkCode
            // 
            this.lb_ChkCode.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ChkCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ChkCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ChkCode.Location = new System.Drawing.Point(3, 34);
            this.lb_ChkCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_ChkCode.Name = "lb_ChkCode";
            this.lb_ChkCode.Size = new System.Drawing.Size(228, 28);
            this.lb_ChkCode.TabIndex = 66;
            this.lb_ChkCode.Text = "验码结果";
            // 
            // lb_ChkCodeNote
            // 
            this.lb_ChkCodeNote.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ChkCodeNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ChkCodeNote.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ChkCodeNote.Location = new System.Drawing.Point(-2, 400);
            this.lb_ChkCodeNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_ChkCodeNote.Name = "lb_ChkCodeNote";
            this.lb_ChkCodeNote.Size = new System.Drawing.Size(244, 41);
            this.lb_ChkCodeNote.TabIndex = 65;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(6, 356);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 40);
            this.button2.TabIndex = 62;
            this.button2.Text = "手动验码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(279, 266);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(51, 46);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 69;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(644, 266);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(51, 46);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 70;
            this.pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 918);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lockSettings_checkBox);
            this.Controls.Add(this.pnl_Mes);
            this.Controls.Add(this.runStatus_lable);
            this.Controls.Add(this.txtBox_prtPath);
            this.Controls.Add(this.label1_3);
            this.Controls.Add(this.txtBox_zplPath);
            this.Controls.Add(this.label1_2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox_veriCode;
        private System.Windows.Forms.TextBox txtBox_mesId;
        private System.Windows.Forms.Button btn_makeZpl;
        private System.Windows.Forms.Button btn_sendToPrt;
        private System.Windows.Forms.Button btn_mesCmd1;
        private System.Windows.Forms.Label label1_1;
        private System.Windows.Forms.Label label1_2;
        private System.Windows.Forms.TextBox txtBox_zplPath;
        private System.Windows.Forms.Label label1_3;
        private System.Windows.Forms.TextBox txtBox_prtPath;
        private System.Windows.Forms.TextBox txtBox_serialRead;
        private System.Windows.Forms.Label label3_2;
        private System.Windows.Forms.Label label3_1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btn_openSerial;
        private System.Windows.Forms.ComboBox cobBox_SeriPortNum;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtBox_position;
        private System.Windows.Forms.Label label2_3;
        private System.Windows.Forms.Label label2_4;
        private System.Windows.Forms.Label label2_5;
        private System.Windows.Forms.TextBox txtBox_fogId;
        private System.Windows.Forms.Button btn_mesCmd2;
        private System.Windows.Forms.Button btn_mesCmd3;
        private System.Windows.Forms.TextBox txtBox_mesApi;
        private System.Windows.Forms.Label label2_1;
        private System.Windows.Forms.Label label1_5;
        private System.Windows.Forms.TextBox txtBox_prtCode;
        private System.Windows.Forms.Label lastPrtCode_label;
        //private System.Windows.Forms.Button getJsonButton;
        //private System.Windows.Forms.Button convertJsonButton;
        private System.Windows.Forms.Panel pnl_Mes;
        private System.Windows.Forms.TextBox txtBox_jsonMsg;
        private System.Windows.Forms.Label label2_2;
        private System.Windows.Forms.TextBox txtBox_scnCode;
        private System.Windows.Forms.Label label1_6;
        private System.Windows.Forms.Label serialPort_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_reloadPort;
        private System.Windows.Forms.Label camValue_label;
        private System.Windows.Forms.Label prtValue_label;
        private System.Windows.Forms.Label scnValue_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label runStatus_lable;
        private System.Windows.Forms.CheckBox lockSettings_checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBox_mesAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_apiTest;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtBox_veriCodeHistory;
        private System.Windows.Forms.Label label_zplTemp;
        private System.Windows.Forms.Label lb_ReadCode;
        private System.Windows.Forms.Button btn_RetryRead;
        private System.Windows.Forms.Label lb_PrtCode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lb_ReadCodeNote;
        private System.Windows.Forms.Label lb_PrtCodeNote;
        private System.Windows.Forms.Label lb_ChkCodeNote;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lb_ChkCode;
        //private System.Windows.Forms.Label veriCount_label;
        //private System.Windows.Forms.Label label6;
    }
}

