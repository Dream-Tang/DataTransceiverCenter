
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
            this.btn_makeZpl = new System.Windows.Forms.Button();
            this.btn_sendToPrt = new System.Windows.Forms.Button();
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
            this.label1_5 = new System.Windows.Forms.Label();
            this.txtBox_prtCode = new System.Windows.Forms.TextBox();
            this.lastPrtCode_label = new System.Windows.Forms.Label();
            this.txtBox_scnCode = new System.Windows.Forms.TextBox();
            this.label1_6 = new System.Windows.Forms.Label();
            this.serialPort_label = new System.Windows.Forms.Label();
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
            this.btn_RetryChk = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_apiTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBox_postData = new System.Windows.Forms.TextBox();
            this.label2_2 = new System.Windows.Forms.Label();
            this.txtBox_responseData = new System.Windows.Forms.TextBox();
            this.label2_1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBox_veriCode
            // 
            this.txtBox_veriCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_veriCode.Location = new System.Drawing.Point(3, 57);
            this.txtBox_veriCode.Name = "txtBox_veriCode";
            this.txtBox_veriCode.Size = new System.Drawing.Size(194, 39);
            this.txtBox_veriCode.TabIndex = 0;
            // 
            // btn_makeZpl
            // 
            this.btn_makeZpl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_makeZpl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_makeZpl.Location = new System.Drawing.Point(104, 219);
            this.btn_makeZpl.Name = "btn_makeZpl";
            this.btn_makeZpl.Size = new System.Drawing.Size(89, 45);
            this.btn_makeZpl.TabIndex = 2;
            this.btn_makeZpl.Text = "生成ZPL";
            this.btn_makeZpl.UseVisualStyleBackColor = true;
            this.btn_makeZpl.Click += new System.EventHandler(this.makeZpl_btn_Click);
            // 
            // btn_sendToPrt
            // 
            this.btn_sendToPrt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sendToPrt.Location = new System.Drawing.Point(3, 282);
            this.btn_sendToPrt.Name = "btn_sendToPrt";
            this.btn_sendToPrt.Size = new System.Drawing.Size(193, 27);
            this.btn_sendToPrt.TabIndex = 3;
            this.btn_sendToPrt.Text = "打印条码";
            this.btn_sendToPrt.UseVisualStyleBackColor = true;
            this.btn_sendToPrt.Click += new System.EventHandler(this.sendToPrt_btn_Click);
            // 
            // label1_1
            // 
            this.label1_1.AutoSize = true;
            this.label1_1.BackColor = System.Drawing.SystemColors.Info;
            this.label1_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_1.Location = new System.Drawing.Point(3, 1);
            this.label1_1.Name = "label1_1";
            this.label1_1.Size = new System.Drawing.Size(84, 26);
            this.label1_1.TabIndex = 10;
            this.label1_1.Text = "读玻璃码";
            // 
            // label1_2
            // 
            this.label1_2.AutoSize = true;
            this.label1_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_2.Location = new System.Drawing.Point(5, 8);
            this.label1_2.Name = "label1_2";
            this.label1_2.Size = new System.Drawing.Size(109, 20);
            this.label1_2.TabIndex = 11;
            this.label1_2.Text = "ZPL文件路径";
            // 
            // txtBox_zplPath
            // 
            this.txtBox_zplPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_zplPath.Location = new System.Drawing.Point(117, 7);
            this.txtBox_zplPath.Name = "txtBox_zplPath";
            this.txtBox_zplPath.Size = new System.Drawing.Size(470, 24);
            this.txtBox_zplPath.TabIndex = 12;
            this.txtBox_zplPath.TextChanged += new System.EventHandler(this.txtBox_zplPath_TextChanged);
            // 
            // label1_3
            // 
            this.label1_3.AutoSize = true;
            this.label1_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_3.Location = new System.Drawing.Point(5, 32);
            this.label1_3.Name = "label1_3";
            this.label1_3.Size = new System.Drawing.Size(100, 20);
            this.label1_3.TabIndex = 13;
            this.label1_3.Text = "打印机地址";
            // 
            // txtBox_prtPath
            // 
            this.txtBox_prtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_prtPath.Location = new System.Drawing.Point(117, 37);
            this.txtBox_prtPath.Name = "txtBox_prtPath";
            this.txtBox_prtPath.Size = new System.Drawing.Size(470, 24);
            this.txtBox_prtPath.TabIndex = 14;
            this.txtBox_prtPath.TextChanged += new System.EventHandler(this.txtBox_prtPath_TextChanged);
            // 
            // txtBox_serialRead
            // 
            this.txtBox_serialRead.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_serialRead.Location = new System.Drawing.Point(2, 237);
            this.txtBox_serialRead.Name = "txtBox_serialRead";
            this.txtBox_serialRead.Size = new System.Drawing.Size(188, 39);
            this.txtBox_serialRead.TabIndex = 24;
            this.txtBox_serialRead.TextChanged += new System.EventHandler(this.serialRead_txtBox_TextChanged);
            // 
            // label3_2
            // 
            this.label3_2.AutoSize = true;
            this.label3_2.Location = new System.Drawing.Point(0, 154);
            this.label3_2.Name = "label3_2";
            this.label3_2.Size = new System.Drawing.Size(62, 18);
            this.label3_2.TabIndex = 22;
            this.label3_2.Text = "波特率";
            // 
            // label3_1
            // 
            this.label3_1.AutoSize = true;
            this.label3_1.Location = new System.Drawing.Point(2, 98);
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
            this.btn_openSerial.Location = new System.Drawing.Point(85, 169);
            this.btn_openSerial.Name = "btn_openSerial";
            this.btn_openSerial.Size = new System.Drawing.Size(105, 32);
            this.btn_openSerial.TabIndex = 26;
            this.btn_openSerial.Text = "打开串口";
            this.btn_openSerial.UseVisualStyleBackColor = true;
            this.btn_openSerial.Click += new System.EventHandler(this.openSerial_btn_Click);
            // 
            // cobBox_SeriPortNum
            // 
            this.cobBox_SeriPortNum.FormattingEnabled = true;
            this.cobBox_SeriPortNum.Location = new System.Drawing.Point(1, 119);
            this.cobBox_SeriPortNum.Name = "cobBox_SeriPortNum";
            this.cobBox_SeriPortNum.Size = new System.Drawing.Size(67, 26);
            this.cobBox_SeriPortNum.TabIndex = 27;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBox2.Location = new System.Drawing.Point(3, 175);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(67, 26);
            this.comboBox2.TabIndex = 28;
            this.comboBox2.Text = "9600";
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BackColor = System.Drawing.SystemColors.Info;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_5.Location = new System.Drawing.Point(3, 1);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(84, 26);
            this.label1_5.TabIndex = 46;
            this.label1_5.Text = "打印条码";
            // 
            // txtBox_prtCode
            // 
            this.txtBox_prtCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_prtCode.Location = new System.Drawing.Point(3, 57);
            this.txtBox_prtCode.Name = "txtBox_prtCode";
            this.txtBox_prtCode.Size = new System.Drawing.Size(194, 39);
            this.txtBox_prtCode.TabIndex = 45;
            this.txtBox_prtCode.TextChanged += new System.EventHandler(this.prtCode_txtBox_TextChanged);
            // 
            // lastPrtCode_label
            // 
            this.lastPrtCode_label.BackColor = System.Drawing.SystemColors.Window;
            this.lastPrtCode_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lastPrtCode_label.Location = new System.Drawing.Point(3, 176);
            this.lastPrtCode_label.Name = "lastPrtCode_label";
            this.lastPrtCode_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lastPrtCode_label.Size = new System.Drawing.Size(190, 27);
            this.lastPrtCode_label.TabIndex = 47;
            this.lastPrtCode_label.Text = "上次打印码";
            this.lastPrtCode_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBox_scnCode
            // 
            this.txtBox_scnCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_scnCode.Location = new System.Drawing.Point(3, 57);
            this.txtBox_scnCode.Name = "txtBox_scnCode";
            this.txtBox_scnCode.Size = new System.Drawing.Size(194, 39);
            this.txtBox_scnCode.TabIndex = 52;
            this.txtBox_scnCode.TextChanged += new System.EventHandler(this.scnCode_txtBox_TextChanged);
            // 
            // label1_6
            // 
            this.label1_6.AutoSize = true;
            this.label1_6.BackColor = System.Drawing.SystemColors.Info;
            this.label1_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_6.Location = new System.Drawing.Point(3, 1);
            this.label1_6.Name = "label1_6";
            this.label1_6.Size = new System.Drawing.Size(84, 26);
            this.label1_6.TabIndex = 53;
            this.label1_6.Text = "验码绑定";
            // 
            // serialPort_label
            // 
            this.serialPort_label.BackColor = System.Drawing.Color.Gray;
            this.serialPort_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serialPort_label.ForeColor = System.Drawing.Color.White;
            this.serialPort_label.Location = new System.Drawing.Point(3, 204);
            this.serialPort_label.Name = "serialPort_label";
            this.serialPort_label.Size = new System.Drawing.Size(186, 28);
            this.serialPort_label.TabIndex = 54;
            this.serialPort_label.Text = "串口已关闭";
            this.serialPort_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_reloadPort
            // 
            this.btn_reloadPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reloadPort.Location = new System.Drawing.Point(83, 115);
            this.btn_reloadPort.Name = "btn_reloadPort";
            this.btn_reloadPort.Size = new System.Drawing.Size(107, 32);
            this.btn_reloadPort.TabIndex = 55;
            this.btn_reloadPort.Text = "刷新串口号";
            this.btn_reloadPort.UseVisualStyleBackColor = true;
            this.btn_reloadPort.Click += new System.EventHandler(this.reloadPort_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(2, 116);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 50);
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
            this.runStatus_lable.Location = new System.Drawing.Point(599, 1);
            this.runStatus_lable.Name = "runStatus_lable";
            this.runStatus_lable.Size = new System.Drawing.Size(140, 30);
            this.runStatus_lable.TabIndex = 31;
            this.runStatus_lable.Text = "自动运行状态";
            // 
            // txtBox_veriCodeHistory
            // 
            this.txtBox_veriCodeHistory.Location = new System.Drawing.Point(3, 102);
            this.txtBox_veriCodeHistory.MaxLength = 0;
            this.txtBox_veriCodeHistory.Multiline = true;
            this.txtBox_veriCodeHistory.Name = "txtBox_veriCodeHistory";
            this.txtBox_veriCodeHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBox_veriCodeHistory.Size = new System.Drawing.Size(200, 174);
            this.txtBox_veriCodeHistory.TabIndex = 60;
            this.txtBox_veriCodeHistory.TextChanged += new System.EventHandler(this.veriCodeHistory_txtBox_TextChanged);
            this.txtBox_veriCodeHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtBox_veriCodeHistory_MouseDoubleClick);
            // 
            // lockSettings_checkBox
            // 
            this.lockSettings_checkBox.AutoSize = true;
            this.lockSettings_checkBox.Checked = true;
            this.lockSettings_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lockSettings_checkBox.Location = new System.Drawing.Point(633, 39);
            this.lockSettings_checkBox.Name = "lockSettings_checkBox";
            this.lockSettings_checkBox.Size = new System.Drawing.Size(106, 22);
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
            this.panel3.Location = new System.Drawing.Point(10, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 350);
            this.panel3.TabIndex = 65;
            // 
            // lb_ReadCodeNote
            // 
            this.lb_ReadCodeNote.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ReadCodeNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ReadCodeNote.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ReadCodeNote.Location = new System.Drawing.Point(0, 320);
            this.lb_ReadCodeNote.Name = "lb_ReadCodeNote";
            this.lb_ReadCodeNote.Size = new System.Drawing.Size(200, 25);
            this.lb_ReadCodeNote.TabIndex = 63;
            // 
            // lb_ReadCode
            // 
            this.lb_ReadCode.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ReadCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ReadCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ReadCode.Location = new System.Drawing.Point(3, 27);
            this.lb_ReadCode.Name = "lb_ReadCode";
            this.lb_ReadCode.Size = new System.Drawing.Size(194, 27);
            this.lb_ReadCode.TabIndex = 62;
            this.lb_ReadCode.Text = "读玻璃码";
            // 
            // btn_RetryRead
            // 
            this.btn_RetryRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RetryRead.Location = new System.Drawing.Point(3, 282);
            this.btn_RetryRead.Name = "btn_RetryRead";
            this.btn_RetryRead.Size = new System.Drawing.Size(193, 27);
            this.btn_RetryRead.TabIndex = 61;
            this.btn_RetryRead.Text = "手动读码";
            this.btn_RetryRead.UseVisualStyleBackColor = true;
            this.btn_RetryRead.Click += new System.EventHandler(this.btn_RetryRead_Click);
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
            this.panel4.Location = new System.Drawing.Point(300, 80);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 350);
            this.panel4.TabIndex = 66;
            // 
            // lb_PrtCodeNote
            // 
            this.lb_PrtCodeNote.BackColor = System.Drawing.SystemColors.Info;
            this.lb_PrtCodeNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PrtCodeNote.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_PrtCodeNote.Location = new System.Drawing.Point(0, 320);
            this.lb_PrtCodeNote.Name = "lb_PrtCodeNote";
            this.lb_PrtCodeNote.Size = new System.Drawing.Size(200, 25);
            this.lb_PrtCodeNote.TabIndex = 64;
            // 
            // lb_PrtCode
            // 
            this.lb_PrtCode.BackColor = System.Drawing.SystemColors.Info;
            this.lb_PrtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PrtCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_PrtCode.Location = new System.Drawing.Point(3, 27);
            this.lb_PrtCode.Name = "lb_PrtCode";
            this.lb_PrtCode.Size = new System.Drawing.Size(194, 27);
            this.lb_PrtCode.TabIndex = 63;
            this.lb_PrtCode.Text = "打印条码";
            // 
            // label_zplTemp
            // 
            this.label_zplTemp.BackColor = System.Drawing.SystemColors.Control;
            this.label_zplTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_zplTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_zplTemp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_zplTemp.Location = new System.Drawing.Point(6, 219);
            this.label_zplTemp.Name = "label_zplTemp";
            this.label_zplTemp.Size = new System.Drawing.Size(92, 45);
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
            this.panel5.Controls.Add(this.btn_RetryChk);
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
            this.panel5.Location = new System.Drawing.Point(590, 80);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 350);
            this.panel5.TabIndex = 67;
            // 
            // lb_ChkCode
            // 
            this.lb_ChkCode.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ChkCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ChkCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ChkCode.Location = new System.Drawing.Point(3, 27);
            this.lb_ChkCode.Name = "lb_ChkCode";
            this.lb_ChkCode.Size = new System.Drawing.Size(194, 27);
            this.lb_ChkCode.TabIndex = 66;
            this.lb_ChkCode.Text = "验码结果";
            // 
            // lb_ChkCodeNote
            // 
            this.lb_ChkCodeNote.BackColor = System.Drawing.SystemColors.Info;
            this.lb_ChkCodeNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ChkCodeNote.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ChkCodeNote.Location = new System.Drawing.Point(0, 320);
            this.lb_ChkCodeNote.Name = "lb_ChkCodeNote";
            this.lb_ChkCodeNote.Size = new System.Drawing.Size(200, 25);
            this.lb_ChkCodeNote.TabIndex = 65;
            // 
            // btn_RetryChk
            // 
            this.btn_RetryChk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RetryChk.Location = new System.Drawing.Point(3, 282);
            this.btn_RetryChk.Name = "btn_RetryChk";
            this.btn_RetryChk.Size = new System.Drawing.Size(193, 27);
            this.btn_RetryChk.TabIndex = 62;
            this.btn_RetryChk.Text = "手动验码";
            this.btn_RetryChk.UseVisualStyleBackColor = true;
            this.btn_RetryChk.Click += new System.EventHandler(this.btn_RetryChk_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(232, 201);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 69;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(528, 201);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 31);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 70;
            this.pictureBox3.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2500;
            this.timer1.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btn_apiTest
            // 
            this.btn_apiTest.Location = new System.Drawing.Point(681, 428);
            this.btn_apiTest.Name = "btn_apiTest";
            this.btn_apiTest.Size = new System.Drawing.Size(110, 44);
            this.btn_apiTest.TabIndex = 44;
            this.btn_apiTest.Text = "Mes通信";
            this.btn_apiTest.UseVisualStyleBackColor = true;
            this.btn_apiTest.Click += new System.EventHandler(this.apiTest_btn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2_1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBox_responseData, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2_2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBox_postData, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 436);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 202);
            this.tableLayoutPanel1.TabIndex = 71;
            // 
            // txtBox_postData
            // 
            this.txtBox_postData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_postData.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_postData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_postData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_postData.Location = new System.Drawing.Point(3, 23);
            this.txtBox_postData.Multiline = true;
            this.txtBox_postData.Name = "txtBox_postData";
            this.txtBox_postData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_postData.Size = new System.Drawing.Size(775, 75);
            this.txtBox_postData.TabIndex = 42;
            // 
            // label2_2
            // 
            this.label2_2.AutoSize = true;
            this.label2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_2.Location = new System.Drawing.Point(3, 101);
            this.label2_2.Name = "label2_2";
            this.label2_2.Size = new System.Drawing.Size(127, 20);
            this.label2_2.TabIndex = 53;
            this.label2_2.Text = "MES回复的消息";
            // 
            // txtBox_responseData
            // 
            this.txtBox_responseData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_responseData.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_responseData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_responseData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_responseData.Location = new System.Drawing.Point(3, 124);
            this.txtBox_responseData.Multiline = true;
            this.txtBox_responseData.Name = "txtBox_responseData";
            this.txtBox_responseData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_responseData.Size = new System.Drawing.Size(775, 75);
            this.txtBox_responseData.TabIndex = 52;
            // 
            // label2_1
            // 
            this.label2_1.AutoSize = true;
            this.label2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_1.Location = new System.Drawing.Point(3, 0);
            this.label2_1.Name = "label2_1";
            this.label2_1.Size = new System.Drawing.Size(127, 20);
            this.label2_1.TabIndex = 43;
            this.label2_1.Text = "发给MES的消息";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.ControlBox = false;
            this.Controls.Add(this.btn_apiTest);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lockSettings_checkBox);
            this.Controls.Add(this.runStatus_lable);
            this.Controls.Add(this.txtBox_prtPath);
            this.Controls.Add(this.label1_3);
            this.Controls.Add(this.txtBox_zplPath);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_makeZpl;
        private System.Windows.Forms.Button btn_sendToPrt;
        private System.Windows.Forms.Label label1_1;
        private System.Windows.Forms.Label label1_2;
        private System.Windows.Forms.Label label1_3;
        private System.Windows.Forms.TextBox txtBox_serialRead;
        private System.Windows.Forms.Label label3_2;
        private System.Windows.Forms.Label label3_1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btn_openSerial;
        private System.Windows.Forms.ComboBox cobBox_SeriPortNum;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1_5;
        private System.Windows.Forms.TextBox txtBox_prtCode;
        private System.Windows.Forms.Label lastPrtCode_label;
        private System.Windows.Forms.TextBox txtBox_scnCode;
        private System.Windows.Forms.Label label1_6;
        private System.Windows.Forms.Label serialPort_label;
        private System.Windows.Forms.Button btn_reloadPort;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label runStatus_lable;
        private System.Windows.Forms.CheckBox lockSettings_checkBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.TextBox txtBox_veriCodeHistory;
        private System.Windows.Forms.Label lb_ReadCode;
        private System.Windows.Forms.Button btn_RetryRead;
        private System.Windows.Forms.Label lb_PrtCode;
        private System.Windows.Forms.Button btn_RetryChk;
        private System.Windows.Forms.Label lb_ReadCodeNote;
        private System.Windows.Forms.Label lb_PrtCodeNote;
        private System.Windows.Forms.Label lb_ChkCodeNote;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lb_ChkCode;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.TextBox txtBox_veriCode;
        public System.Windows.Forms.TextBox txtBox_zplPath;
        public System.Windows.Forms.TextBox txtBox_prtPath;
        private System.Windows.Forms.Label label_zplTemp;
        private System.Windows.Forms.Button btn_apiTest;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2_1;
        private System.Windows.Forms.TextBox txtBox_responseData;
        private System.Windows.Forms.Label label2_2;
        private System.Windows.Forms.TextBox txtBox_postData;
        //private System.Windows.Forms.Label veriCount_label;
        //private System.Windows.Forms.Label label6;
    }
}

