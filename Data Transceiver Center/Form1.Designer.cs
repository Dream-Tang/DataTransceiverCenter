
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
            this.visionCode_txtBox = new System.Windows.Forms.TextBox();
            this.mesId_txtBox = new System.Windows.Forms.TextBox();
            this.makeZpl_btn = new System.Windows.Forms.Button();
            this.sendToPrt_btn = new System.Windows.Forms.Button();
            this.mesCmd1_btn = new System.Windows.Forms.Button();
            this.label1_4 = new System.Windows.Forms.Label();
            this.label1_2 = new System.Windows.Forms.Label();
            this.zplPath_txtBox = new System.Windows.Forms.TextBox();
            this.label1_3 = new System.Windows.Forms.Label();
            this.prtPath_txtBox = new System.Windows.Forms.TextBox();
            this.csvPath_txtBox = new System.Windows.Forms.TextBox();
            this.label1_1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.readCsv_btn = new System.Windows.Forms.Button();
            this.autoRun_checkBox = new System.Windows.Forms.CheckBox();
            this.label3_3 = new System.Windows.Forms.Label();
            this.serialRead__txtBox = new System.Windows.Forms.TextBox();
            this.label3_2 = new System.Windows.Forms.Label();
            this.label3_1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.openSerial_btn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3_4 = new System.Windows.Forms.Label();
            this.chckResult_txtBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_RunStatus = new System.Windows.Forms.Label();
            this.deleCsv_checkBox = new System.Windows.Forms.CheckBox();
            this.position_txtBox = new System.Windows.Forms.TextBox();
            this.label2_3 = new System.Windows.Forms.Label();
            this.label2_4 = new System.Windows.Forms.Label();
            this.label2_5 = new System.Windows.Forms.Label();
            this.fogId_txtBox = new System.Windows.Forms.TextBox();
            this.mesCmd2_btn = new System.Windows.Forms.Button();
            this.mesCmd3_btn = new System.Windows.Forms.Button();
            this.mesApiBox = new System.Windows.Forms.TextBox();
            this.label2_1 = new System.Windows.Forms.Label();
            this.label1_5 = new System.Windows.Forms.Label();
            this.prtCode_txtBox = new System.Windows.Forms.TextBox();
            this.prtCodelabel = new System.Windows.Forms.Label();
            this.visionCodelabel = new System.Windows.Forms.Label();
            this.pnl_Mes = new System.Windows.Forms.Panel();
            this.label2_2 = new System.Windows.Forms.Label();
            this.txtBox_GetJson = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scnCode_txtBox = new System.Windows.Forms.TextBox();
            this.label1_6 = new System.Windows.Forms.Label();
            this.serialPort_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnl_Mes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // visionCode_txtBox
            // 
            this.visionCode_txtBox.Location = new System.Drawing.Point(5, 106);
            this.visionCode_txtBox.Name = "visionCode_txtBox";
            this.visionCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.visionCode_txtBox.TabIndex = 0;
            // 
            // mesId_txtBox
            // 
            this.mesId_txtBox.Location = new System.Drawing.Point(8, 58);
            this.mesId_txtBox.Name = "mesId_txtBox";
            this.mesId_txtBox.Size = new System.Drawing.Size(142, 23);
            this.mesId_txtBox.TabIndex = 1;
            // 
            // makeZpl_btn
            // 
            this.makeZpl_btn.Location = new System.Drawing.Point(116, 133);
            this.makeZpl_btn.Name = "makeZpl_btn";
            this.makeZpl_btn.Size = new System.Drawing.Size(80, 40);
            this.makeZpl_btn.TabIndex = 2;
            this.makeZpl_btn.Text = "生成ZPL文件";
            this.makeZpl_btn.UseVisualStyleBackColor = true;
            this.makeZpl_btn.Click += new System.EventHandler(this.makeZpl_btn_Click);
            // 
            // sendToPrt_btn
            // 
            this.sendToPrt_btn.Location = new System.Drawing.Point(226, 133);
            this.sendToPrt_btn.Name = "sendToPrt_btn";
            this.sendToPrt_btn.Size = new System.Drawing.Size(80, 40);
            this.sendToPrt_btn.TabIndex = 3;
            this.sendToPrt_btn.Text = "发送ZPL文件";
            this.sendToPrt_btn.UseVisualStyleBackColor = true;
            this.sendToPrt_btn.Click += new System.EventHandler(this.sendToPrt_btn_Click);
            // 
            // mesCmd1_btn
            // 
            this.mesCmd1_btn.Location = new System.Drawing.Point(155, 7);
            this.mesCmd1_btn.Name = "mesCmd1_btn";
            this.mesCmd1_btn.Size = new System.Drawing.Size(67, 33);
            this.mesCmd1_btn.TabIndex = 7;
            this.mesCmd1_btn.Text = "MES通信1";
            this.mesCmd1_btn.UseVisualStyleBackColor = true;
            this.mesCmd1_btn.Click += new System.EventHandler(this.mesCmd1_btn_Click);
            // 
            // label1_4
            // 
            this.label1_4.AutoSize = true;
            this.label1_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_4.Location = new System.Drawing.Point(6, 89);
            this.label1_4.Name = "label1_4";
            this.label1_4.Size = new System.Drawing.Size(55, 14);
            this.label1_4.TabIndex = 10;
            this.label1_4.Text = "视觉读码";
            // 
            // label1_2
            // 
            this.label1_2.AutoSize = true;
            this.label1_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_2.Location = new System.Drawing.Point(5, 31);
            this.label1_2.Name = "label1_2";
            this.label1_2.Size = new System.Drawing.Size(73, 14);
            this.label1_2.TabIndex = 11;
            this.label1_2.Text = "ZPL文件路径";
            // 
            // zplPath_txtBox
            // 
            this.zplPath_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zplPath_txtBox.Location = new System.Drawing.Point(84, 28);
            this.zplPath_txtBox.Name = "zplPath_txtBox";
            this.zplPath_txtBox.Size = new System.Drawing.Size(346, 19);
            this.zplPath_txtBox.TabIndex = 12;
            // 
            // label1_3
            // 
            this.label1_3.AutoSize = true;
            this.label1_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_3.Location = new System.Drawing.Point(5, 54);
            this.label1_3.Name = "label1_3";
            this.label1_3.Size = new System.Drawing.Size(67, 14);
            this.label1_3.TabIndex = 13;
            this.label1_3.Text = "打印机地址";
            // 
            // prtPath_txtBox
            // 
            this.prtPath_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prtPath_txtBox.Location = new System.Drawing.Point(84, 52);
            this.prtPath_txtBox.Name = "prtPath_txtBox";
            this.prtPath_txtBox.Size = new System.Drawing.Size(346, 19);
            this.prtPath_txtBox.TabIndex = 14;
            // 
            // csvPath_txtBox
            // 
            this.csvPath_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.csvPath_txtBox.Location = new System.Drawing.Point(84, 5);
            this.csvPath_txtBox.Name = "csvPath_txtBox";
            this.csvPath_txtBox.Size = new System.Drawing.Size(346, 19);
            this.csvPath_txtBox.TabIndex = 16;
            // 
            // label1_1
            // 
            this.label1_1.AutoSize = true;
            this.label1_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_1.Location = new System.Drawing.Point(5, 8);
            this.label1_1.Name = "label1_1";
            this.label1_1.Size = new System.Drawing.Size(73, 14);
            this.label1_1.TabIndex = 15;
            this.label1_1.Text = "CSV文件路径";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 179);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(425, 86);
            this.dataGridView1.TabIndex = 17;
            // 
            // readCsv_btn
            // 
            this.readCsv_btn.Location = new System.Drawing.Point(5, 133);
            this.readCsv_btn.Name = "readCsv_btn";
            this.readCsv_btn.Size = new System.Drawing.Size(80, 40);
            this.readCsv_btn.TabIndex = 18;
            this.readCsv_btn.Text = "读CSV数据";
            this.readCsv_btn.UseVisualStyleBackColor = true;
            this.readCsv_btn.Click += new System.EventHandler(this.button7_Click);
            // 
            // autoRun_checkBox
            // 
            this.autoRun_checkBox.AutoSize = true;
            this.autoRun_checkBox.Location = new System.Drawing.Point(499, 10);
            this.autoRun_checkBox.Name = "autoRun_checkBox";
            this.autoRun_checkBox.Size = new System.Drawing.Size(126, 16);
            this.autoRun_checkBox.TabIndex = 19;
            this.autoRun_checkBox.Text = "自动读取csv并发送";
            this.autoRun_checkBox.UseVisualStyleBackColor = true;
            this.autoRun_checkBox.CheckedChanged += new System.EventHandler(this.autoRun_checkBox_CheckedChanged);
            // 
            // label3_3
            // 
            this.label3_3.AutoSize = true;
            this.label3_3.Location = new System.Drawing.Point(460, 266);
            this.label3_3.Name = "label3_3";
            this.label3_3.Size = new System.Drawing.Size(53, 12);
            this.label3_3.TabIndex = 25;
            this.label3_3.Text = "串口数据";
            // 
            // serialRead__txtBox
            // 
            this.serialRead__txtBox.Location = new System.Drawing.Point(516, 263);
            this.serialRead__txtBox.Name = "serialRead__txtBox";
            this.serialRead__txtBox.Size = new System.Drawing.Size(167, 21);
            this.serialRead__txtBox.TabIndex = 24;
            // 
            // label3_2
            // 
            this.label3_2.AutoSize = true;
            this.label3_2.Location = new System.Drawing.Point(460, 216);
            this.label3_2.Name = "label3_2";
            this.label3_2.Size = new System.Drawing.Size(41, 12);
            this.label3_2.TabIndex = 22;
            this.label3_2.Text = "波特率";
            // 
            // label3_1
            // 
            this.label3_1.AutoSize = true;
            this.label3_1.Location = new System.Drawing.Point(460, 178);
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
            this.openSerial_btn.Location = new System.Drawing.Point(566, 193);
            this.openSerial_btn.Name = "openSerial_btn";
            this.openSerial_btn.Size = new System.Drawing.Size(59, 44);
            this.openSerial_btn.TabIndex = 26;
            this.openSerial_btn.Text = "打开串口";
            this.openSerial_btn.UseVisualStyleBackColor = true;
            this.openSerial_btn.Click += new System.EventHandler(this.openSerial_btn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(462, 193);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 20);
            this.comboBox1.TabIndex = 27;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(462, 231);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(76, 20);
            this.comboBox2.TabIndex = 28;
            // 
            // label3_4
            // 
            this.label3_4.AutoSize = true;
            this.label3_4.Location = new System.Drawing.Point(460, 297);
            this.label3_4.Name = "label3_4";
            this.label3_4.Size = new System.Drawing.Size(53, 12);
            this.label3_4.TabIndex = 29;
            this.label3_4.Text = "校验结果";
            // 
            // chckResult_txtBox
            // 
            this.chckResult_txtBox.Location = new System.Drawing.Point(516, 294);
            this.chckResult_txtBox.Name = "chckResult_txtBox";
            this.chckResult_txtBox.Size = new System.Drawing.Size(167, 21);
            this.chckResult_txtBox.TabIndex = 30;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_RunStatus
            // 
            this.label_RunStatus.AutoSize = true;
            this.label_RunStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_RunStatus.Location = new System.Drawing.Point(499, 57);
            this.label_RunStatus.Name = "label_RunStatus";
            this.label_RunStatus.Size = new System.Drawing.Size(79, 14);
            this.label_RunStatus.TabIndex = 31;
            this.label_RunStatus.Text = "自动运行状态";
            // 
            // deleCsv_checkBox
            // 
            this.deleCsv_checkBox.AutoSize = true;
            this.deleCsv_checkBox.Location = new System.Drawing.Point(499, 32);
            this.deleCsv_checkBox.Name = "deleCsv_checkBox";
            this.deleCsv_checkBox.Size = new System.Drawing.Size(102, 16);
            this.deleCsv_checkBox.TabIndex = 32;
            this.deleCsv_checkBox.Text = "打印后删除csv";
            this.deleCsv_checkBox.UseVisualStyleBackColor = true;
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
            this.label2_3.Location = new System.Drawing.Point(8, 3);
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
            this.label2_4.Size = new System.Drawing.Size(51, 17);
            this.label2_4.TabIndex = 35;
            this.label2_4.Text = "MES ID";
            // 
            // label2_5
            // 
            this.label2_5.AutoSize = true;
            this.label2_5.Location = new System.Drawing.Point(8, 88);
            this.label2_5.Name = "label2_5";
            this.label2_5.Size = new System.Drawing.Size(50, 17);
            this.label2_5.TabIndex = 36;
            this.label2_5.Text = "FOG ID";
            // 
            // fogId_txtBox
            // 
            this.fogId_txtBox.Location = new System.Drawing.Point(8, 103);
            this.fogId_txtBox.Name = "fogId_txtBox";
            this.fogId_txtBox.Size = new System.Drawing.Size(142, 23);
            this.fogId_txtBox.TabIndex = 37;
            // 
            // mesCmd2_btn
            // 
            this.mesCmd2_btn.Location = new System.Drawing.Point(155, 51);
            this.mesCmd2_btn.Name = "mesCmd2_btn";
            this.mesCmd2_btn.Size = new System.Drawing.Size(67, 33);
            this.mesCmd2_btn.TabIndex = 40;
            this.mesCmd2_btn.Text = "MES通信2";
            this.mesCmd2_btn.UseVisualStyleBackColor = true;
            this.mesCmd2_btn.Click += new System.EventHandler(this.mesCmd2_btn_Click);
            // 
            // mesCmd3_btn
            // 
            this.mesCmd3_btn.Location = new System.Drawing.Point(155, 96);
            this.mesCmd3_btn.Name = "mesCmd3_btn";
            this.mesCmd3_btn.Size = new System.Drawing.Size(67, 33);
            this.mesCmd3_btn.TabIndex = 41;
            this.mesCmd3_btn.Text = "MES通信3";
            this.mesCmd3_btn.UseVisualStyleBackColor = true;
            this.mesCmd3_btn.Click += new System.EventHandler(this.mesCmd3_btn_Click);
            // 
            // mesApiBox
            // 
            this.mesApiBox.BackColor = System.Drawing.SystemColors.Info;
            this.mesApiBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mesApiBox.Location = new System.Drawing.Point(1, 15);
            this.mesApiBox.Multiline = true;
            this.mesApiBox.Name = "mesApiBox";
            this.mesApiBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.mesApiBox.Size = new System.Drawing.Size(424, 69);
            this.mesApiBox.TabIndex = 42;
            // 
            // label2_1
            // 
            this.label2_1.AutoSize = true;
            this.label2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_1.Location = new System.Drawing.Point(5, 2);
            this.label2_1.Name = "label2_1";
            this.label2_1.Size = new System.Drawing.Size(49, 14);
            this.label2_1.TabIndex = 43;
            this.label2_1.Text = "MES api";
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Location = new System.Drawing.Point(150, 91);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(43, 14);
            this.label1_5.TabIndex = 46;
            this.label1_5.Text = "打印码";
            // 
            // prtCode_txtBox
            // 
            this.prtCode_txtBox.Location = new System.Drawing.Point(149, 106);
            this.prtCode_txtBox.Name = "prtCode_txtBox";
            this.prtCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.prtCode_txtBox.TabIndex = 45;
            // 
            // prtCodelabel
            // 
            this.prtCodelabel.AutoSize = true;
            this.prtCodelabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prtCodelabel.Location = new System.Drawing.Point(499, 104);
            this.prtCodelabel.Name = "prtCodelabel";
            this.prtCodelabel.Size = new System.Drawing.Size(67, 14);
            this.prtCodelabel.TabIndex = 47;
            this.prtCodelabel.Text = "上次打印码";
            // 
            // visionCodelabel
            // 
            this.visionCodelabel.AutoSize = true;
            this.visionCodelabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.visionCodelabel.Location = new System.Drawing.Point(499, 80);
            this.visionCodelabel.Name = "visionCodelabel";
            this.visionCodelabel.Size = new System.Drawing.Size(67, 14);
            this.visionCodelabel.TabIndex = 48;
            this.visionCodelabel.Text = "上次视觉码";
            // 
            // pnl_Mes
            // 
            this.pnl_Mes.Controls.Add(this.label2_2);
            this.pnl_Mes.Controls.Add(this.txtBox_GetJson);
            this.pnl_Mes.Controls.Add(this.panel1);
            this.pnl_Mes.Controls.Add(this.label2_1);
            this.pnl_Mes.Controls.Add(this.mesApiBox);
            this.pnl_Mes.Location = new System.Drawing.Point(5, 285);
            this.pnl_Mes.Name = "pnl_Mes";
            this.pnl_Mes.Size = new System.Drawing.Size(431, 232);
            this.pnl_Mes.TabIndex = 51;
            // 
            // label2_2
            // 
            this.label2_2.AutoSize = true;
            this.label2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_2.Location = new System.Drawing.Point(231, 86);
            this.label2_2.Name = "label2_2";
            this.label2_2.Size = new System.Drawing.Size(55, 14);
            this.label2_2.TabIndex = 53;
            this.label2_2.Text = "JSON消息";
            // 
            // txtBox_GetJson
            // 
            this.txtBox_GetJson.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_GetJson.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_GetJson.Location = new System.Drawing.Point(229, 100);
            this.txtBox_GetJson.Multiline = true;
            this.txtBox_GetJson.Name = "txtBox_GetJson";
            this.txtBox_GetJson.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_GetJson.Size = new System.Drawing.Size(196, 129);
            this.txtBox_GetJson.TabIndex = 52;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.panel1.Location = new System.Drawing.Point(0, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 136);
            this.panel1.TabIndex = 55;
            // 
            // scnCode_txtBox
            // 
            this.scnCode_txtBox.Location = new System.Drawing.Point(295, 106);
            this.scnCode_txtBox.Name = "scnCode_txtBox";
            this.scnCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.scnCode_txtBox.TabIndex = 52;
            // 
            // label1_6
            // 
            this.label1_6.AutoSize = true;
            this.label1_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_6.Location = new System.Drawing.Point(298, 90);
            this.label1_6.Name = "label1_6";
            this.label1_6.Size = new System.Drawing.Size(43, 14);
            this.label1_6.TabIndex = 53;
            this.label1_6.Text = "扫码枪";
            // 
            // serialPort_label
            // 
            this.serialPort_label.AutoSize = true;
            this.serialPort_label.BackColor = System.Drawing.SystemColors.Control;
            this.serialPort_label.Location = new System.Drawing.Point(570, 240);
            this.serialPort_label.Name = "serialPort_label";
            this.serialPort_label.Size = new System.Drawing.Size(53, 12);
            this.serialPort_label.TabIndex = 54;
            this.serialPort_label.Text = "串口已关";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(698, 522);
            this.ControlBox = false;
            this.Controls.Add(this.serialPort_label);
            this.Controls.Add(this.label1_6);
            this.Controls.Add(this.scnCode_txtBox);
            this.Controls.Add(this.pnl_Mes);
            this.Controls.Add(this.visionCodelabel);
            this.Controls.Add(this.prtCodelabel);
            this.Controls.Add(this.label1_5);
            this.Controls.Add(this.prtCode_txtBox);
            this.Controls.Add(this.deleCsv_checkBox);
            this.Controls.Add(this.label_RunStatus);
            this.Controls.Add(this.chckResult_txtBox);
            this.Controls.Add(this.label3_4);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.openSerial_btn);
            this.Controls.Add(this.label3_3);
            this.Controls.Add(this.serialRead__txtBox);
            this.Controls.Add(this.label3_2);
            this.Controls.Add(this.label3_1);
            this.Controls.Add(this.autoRun_checkBox);
            this.Controls.Add(this.readCsv_btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.csvPath_txtBox);
            this.Controls.Add(this.label1_1);
            this.Controls.Add(this.prtPath_txtBox);
            this.Controls.Add(this.label1_3);
            this.Controls.Add(this.zplPath_txtBox);
            this.Controls.Add(this.label1_2);
            this.Controls.Add(this.label1_4);
            this.Controls.Add(this.sendToPrt_btn);
            this.Controls.Add(this.makeZpl_btn);
            this.Controls.Add(this.visionCode_txtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnl_Mes.ResumeLayout(false);
            this.pnl_Mes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox visionCode_txtBox;
        private System.Windows.Forms.TextBox mesId_txtBox;
        private System.Windows.Forms.Button makeZpl_btn;
        private System.Windows.Forms.Button sendToPrt_btn;
        private System.Windows.Forms.Button mesCmd1_btn;
        private System.Windows.Forms.Label label1_4;
        private System.Windows.Forms.Label label1_2;
        private System.Windows.Forms.TextBox zplPath_txtBox;
        private System.Windows.Forms.Label label1_3;
        private System.Windows.Forms.TextBox prtPath_txtBox;
        private System.Windows.Forms.TextBox csvPath_txtBox;
        private System.Windows.Forms.Label label1_1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button readCsv_btn;
        private System.Windows.Forms.CheckBox autoRun_checkBox;
        private System.Windows.Forms.Label label3_3;
        private System.Windows.Forms.TextBox serialRead__txtBox;
        private System.Windows.Forms.Label label3_2;
        private System.Windows.Forms.Label label3_1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button openSerial_btn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3_4;
        private System.Windows.Forms.TextBox chckResult_txtBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_RunStatus;
        private System.Windows.Forms.CheckBox deleCsv_checkBox;
        private System.Windows.Forms.TextBox position_txtBox;
        private System.Windows.Forms.Label label2_3;
        private System.Windows.Forms.Label label2_4;
        private System.Windows.Forms.Label label2_5;
        private System.Windows.Forms.TextBox fogId_txtBox;
        private System.Windows.Forms.Button mesCmd2_btn;
        private System.Windows.Forms.Button mesCmd3_btn;
        private System.Windows.Forms.TextBox mesApiBox;
        private System.Windows.Forms.Label label2_1;
        private System.Windows.Forms.Label label1_5;
        private System.Windows.Forms.TextBox prtCode_txtBox;
        private System.Windows.Forms.Label prtCodelabel;
        private System.Windows.Forms.Label visionCodelabel;
        //private System.Windows.Forms.Button getJsonButton;
        //private System.Windows.Forms.Button convertJsonButton;
        private System.Windows.Forms.Panel pnl_Mes;
        private System.Windows.Forms.TextBox txtBox_GetJson;
        private System.Windows.Forms.Label label2_2;
        private System.Windows.Forms.TextBox scnCode_txtBox;
        private System.Windows.Forms.Label label1_6;
        private System.Windows.Forms.Label serialPort_label;
        private System.Windows.Forms.Panel panel1;
    }
}

