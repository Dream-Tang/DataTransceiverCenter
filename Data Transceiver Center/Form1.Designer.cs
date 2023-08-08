
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
            this.visionCodeBox = new System.Windows.Forms.TextBox();
            this.mesIdBox = new System.Windows.Forms.TextBox();
            this.makeZplButton = new System.Windows.Forms.Button();
            this.sendToPrtButton = new System.Windows.Forms.Button();
            this.getJsonButton = new System.Windows.Forms.Button();
            this.convertJsonButton = new System.Windows.Forms.Button();
            this.mesCmd1Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.zplPathBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.prtPathBox = new System.Windows.Forms.TextBox();
            this.csvPathBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.readCsvButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.positionBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.fogIdBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.typrBox = new System.Windows.Forms.TextBox();
            this.mesCmd2Button = new System.Windows.Forms.Button();
            this.mesCmd3Button = new System.Windows.Forms.Button();
            this.mesApiBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.printCallBackLable = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.prtCodeBox = new System.Windows.Forms.TextBox();
            this.prtCodelabel = new System.Windows.Forms.Label();
            this.visionCodelabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.rd_serialStatus = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // visionCodeBox
            // 
            this.visionCodeBox.Location = new System.Drawing.Point(24, 88);
            this.visionCodeBox.Name = "visionCodeBox";
            this.visionCodeBox.Size = new System.Drawing.Size(142, 21);
            this.visionCodeBox.TabIndex = 0;
            // 
            // mesIdBox
            // 
            this.mesIdBox.Location = new System.Drawing.Point(24, 439);
            this.mesIdBox.Name = "mesIdBox";
            this.mesIdBox.Size = new System.Drawing.Size(142, 21);
            this.mesIdBox.TabIndex = 1;
            // 
            // makeZplButton
            // 
            this.makeZplButton.Location = new System.Drawing.Point(158, 124);
            this.makeZplButton.Name = "makeZplButton";
            this.makeZplButton.Size = new System.Drawing.Size(116, 44);
            this.makeZplButton.TabIndex = 2;
            this.makeZplButton.Text = "生成打印机命令";
            this.makeZplButton.UseVisualStyleBackColor = true;
            this.makeZplButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // sendToPrtButton
            // 
            this.sendToPrtButton.Location = new System.Drawing.Point(296, 124);
            this.sendToPrtButton.Name = "sendToPrtButton";
            this.sendToPrtButton.Size = new System.Drawing.Size(116, 44);
            this.sendToPrtButton.TabIndex = 3;
            this.sendToPrtButton.Text = "发送打印机命令";
            this.sendToPrtButton.UseVisualStyleBackColor = true;
            this.sendToPrtButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // getJsonButton
            // 
            this.getJsonButton.Location = new System.Drawing.Point(590, 230);
            this.getJsonButton.Name = "getJsonButton";
            this.getJsonButton.Size = new System.Drawing.Size(82, 44);
            this.getJsonButton.TabIndex = 4;
            this.getJsonButton.Text = "get JSON";
            this.getJsonButton.UseVisualStyleBackColor = true;
            this.getJsonButton.Visible = false;
            this.getJsonButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // convertJsonButton
            // 
            this.convertJsonButton.Location = new System.Drawing.Point(590, 291);
            this.convertJsonButton.Name = "convertJsonButton";
            this.convertJsonButton.Size = new System.Drawing.Size(83, 44);
            this.convertJsonButton.TabIndex = 5;
            this.convertJsonButton.Text = "解析 JSON";
            this.convertJsonButton.UseVisualStyleBackColor = true;
            this.convertJsonButton.Visible = false;
            this.convertJsonButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // mesCmd1Button
            // 
            this.mesCmd1Button.Location = new System.Drawing.Point(183, 420);
            this.mesCmd1Button.Name = "mesCmd1Button";
            this.mesCmd1Button.Size = new System.Drawing.Size(73, 44);
            this.mesCmd1Button.TabIndex = 7;
            this.mesCmd1Button.Text = "MES通信1";
            this.mesCmd1Button.UseVisualStyleBackColor = true;
            this.mesCmd1Button.Click += new System.EventHandler(this.mesCmd1Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(590, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "解析内容";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(590, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "取JSON中特定项";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(24, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "视觉读码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(24, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "ZPL文件路径";
            // 
            // zplPathBox
            // 
            this.zplPathBox.Location = new System.Drawing.Point(24, 30);
            this.zplPathBox.Name = "zplPathBox";
            this.zplPathBox.Size = new System.Drawing.Size(142, 21);
            this.zplPathBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(183, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "打印机地址";
            // 
            // prtPathBox
            // 
            this.prtPathBox.Location = new System.Drawing.Point(183, 30);
            this.prtPathBox.Name = "prtPathBox";
            this.prtPathBox.Size = new System.Drawing.Size(142, 21);
            this.prtPathBox.TabIndex = 14;
            // 
            // csvPathBox
            // 
            this.csvPathBox.Location = new System.Drawing.Point(339, 30);
            this.csvPathBox.Name = "csvPathBox";
            this.csvPathBox.Size = new System.Drawing.Size(142, 21);
            this.csvPathBox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(339, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 14);
            this.label6.TabIndex = 15;
            this.label6.Text = "CSV文件路径";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 174);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(388, 133);
            this.dataGridView1.TabIndex = 17;
            // 
            // readCsvButton
            // 
            this.readCsvButton.Location = new System.Drawing.Point(24, 124);
            this.readCsvButton.Name = "readCsvButton";
            this.readCsvButton.Size = new System.Drawing.Size(116, 44);
            this.readCsvButton.TabIndex = 18;
            this.readCsvButton.Text = "读CSV数据";
            this.readCsvButton.UseVisualStyleBackColor = true;
            this.readCsvButton.Click += new System.EventHandler(this.button7_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(514, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "自动读取csv并发送";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(458, 474);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "串口数据";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(514, 471);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(167, 21);
            this.textBox3.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(458, 424);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "波特率";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(458, 386);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "端口号";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataRecived);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(557, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 44);
            this.button1.TabIndex = 26;
            this.button1.Text = "打开串口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(460, 401);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 20);
            this.comboBox1.TabIndex = 27;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(460, 439);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(76, 20);
            this.comboBox2.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(458, 505);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "校验结果";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(514, 502);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(167, 21);
            this.textBox4.TabIndex = 30;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(514, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 14);
            this.label11.TabIndex = 31;
            this.label11.Text = "CSV文件状态";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(514, 52);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(102, 16);
            this.checkBox2.TabIndex = 32;
            this.checkBox2.Text = "打印后删除csv";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // positionBox
            // 
            this.positionBox.Location = new System.Drawing.Point(24, 394);
            this.positionBox.Name = "positionBox";
            this.positionBox.Size = new System.Drawing.Size(55, 21);
            this.positionBox.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 379);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "线别";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 424);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 35;
            this.label13.Text = "MES ID";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 470);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 36;
            this.label14.Text = "FOG ID";
            // 
            // fogIdBox
            // 
            this.fogIdBox.Location = new System.Drawing.Point(24, 485);
            this.fogIdBox.Name = "fogIdBox";
            this.fogIdBox.Size = new System.Drawing.Size(142, 21);
            this.fogIdBox.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(85, 379);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 39;
            this.label15.Text = "Type";
            // 
            // typrBox
            // 
            this.typrBox.Location = new System.Drawing.Point(85, 394);
            this.typrBox.Name = "typrBox";
            this.typrBox.Size = new System.Drawing.Size(55, 21);
            this.typrBox.TabIndex = 38;
            // 
            // mesCmd2Button
            // 
            this.mesCmd2Button.Location = new System.Drawing.Point(183, 474);
            this.mesCmd2Button.Name = "mesCmd2Button";
            this.mesCmd2Button.Size = new System.Drawing.Size(73, 44);
            this.mesCmd2Button.TabIndex = 40;
            this.mesCmd2Button.Text = "MES通信2";
            this.mesCmd2Button.UseVisualStyleBackColor = true;
            this.mesCmd2Button.Click += new System.EventHandler(this.mesCmd2Button_Click);
            // 
            // mesCmd3Button
            // 
            this.mesCmd3Button.Location = new System.Drawing.Point(273, 474);
            this.mesCmd3Button.Name = "mesCmd3Button";
            this.mesCmd3Button.Size = new System.Drawing.Size(73, 44);
            this.mesCmd3Button.TabIndex = 41;
            this.mesCmd3Button.Text = "MES通信3";
            this.mesCmd3Button.UseVisualStyleBackColor = true;
            this.mesCmd3Button.Click += new System.EventHandler(this.mesCmd3Button_Click);
            // 
            // mesApiBox
            // 
            this.mesApiBox.Location = new System.Drawing.Point(24, 346);
            this.mesApiBox.Name = "mesApiBox";
            this.mesApiBox.Size = new System.Drawing.Size(657, 21);
            this.mesApiBox.TabIndex = 42;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 331);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 12);
            this.label16.TabIndex = 43;
            this.label16.Text = "MES api";
            // 
            // printCallBackLable
            // 
            this.printCallBackLable.AutoSize = true;
            this.printCallBackLable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.printCallBackLable.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.printCallBackLable.Location = new System.Drawing.Point(273, 424);
            this.printCallBackLable.Name = "printCallBackLable";
            this.printCallBackLable.Size = new System.Drawing.Size(122, 29);
            this.printCallBackLable.TabIndex = 44;
            this.printCallBackLable.Text = "回调结果";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(183, 71);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 14);
            this.label17.TabIndex = 46;
            this.label17.Text = "打印码";
            // 
            // prtCodeBox
            // 
            this.prtCodeBox.Location = new System.Drawing.Point(183, 88);
            this.prtCodeBox.Name = "prtCodeBox";
            this.prtCodeBox.Size = new System.Drawing.Size(142, 21);
            this.prtCodeBox.TabIndex = 45;
            // 
            // prtCodelabel
            // 
            this.prtCodelabel.AutoSize = true;
            this.prtCodelabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prtCodelabel.Location = new System.Drawing.Point(339, 95);
            this.prtCodelabel.Name = "prtCodelabel";
            this.prtCodelabel.Size = new System.Drawing.Size(67, 14);
            this.prtCodelabel.TabIndex = 47;
            this.prtCodelabel.Text = "上次打印码";
            // 
            // visionCodelabel
            // 
            this.visionCodelabel.AutoSize = true;
            this.visionCodelabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.visionCodelabel.Location = new System.Drawing.Point(339, 71);
            this.visionCodelabel.Name = "visionCodelabel";
            this.visionCodelabel.Size = new System.Drawing.Size(67, 14);
            this.visionCodelabel.TabIndex = 48;
            this.visionCodelabel.Text = "上次视觉码";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(629, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 44);
            this.button2.TabIndex = 49;
            this.button2.Text = "关闭串口";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // rd_serialStatus
            // 
            this.rd_serialStatus.AutoSize = true;
            this.rd_serialStatus.Location = new System.Drawing.Point(557, 379);
            this.rd_serialStatus.Name = "rd_serialStatus";
            this.rd_serialStatus.Size = new System.Drawing.Size(107, 16);
            this.rd_serialStatus.TabIndex = 50;
            this.rd_serialStatus.TabStop = true;
            this.rd_serialStatus.Text = "串口状态指示灯";
            this.rd_serialStatus.UseVisualStyleBackColor = true;
            this.rd_serialStatus.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(704, 538);
            this.ControlBox = false;
            this.Controls.Add(this.rd_serialStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.visionCodelabel);
            this.Controls.Add(this.prtCodelabel);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.prtCodeBox);
            this.Controls.Add(this.printCallBackLable);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.mesApiBox);
            this.Controls.Add(this.mesCmd3Button);
            this.Controls.Add(this.mesCmd2Button);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.typrBox);
            this.Controls.Add(this.fogIdBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.positionBox);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.readCsvButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.csvPathBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.prtPathBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.zplPathBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mesCmd1Button);
            this.Controls.Add(this.convertJsonButton);
            this.Controls.Add(this.getJsonButton);
            this.Controls.Add(this.sendToPrtButton);
            this.Controls.Add(this.makeZplButton);
            this.Controls.Add(this.mesIdBox);
            this.Controls.Add(this.visionCodeBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox visionCodeBox;
        private System.Windows.Forms.TextBox mesIdBox;
        private System.Windows.Forms.Button makeZplButton;
        private System.Windows.Forms.Button sendToPrtButton;
        private System.Windows.Forms.Button getJsonButton;
        private System.Windows.Forms.Button convertJsonButton;
        private System.Windows.Forms.Button mesCmd1Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox zplPathBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox prtPathBox;
        private System.Windows.Forms.TextBox csvPathBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button readCsvButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox positionBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox fogIdBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox typrBox;
        private System.Windows.Forms.Button mesCmd2Button;
        private System.Windows.Forms.Button mesCmd3Button;
        private System.Windows.Forms.TextBox mesApiBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label printCallBackLable;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox prtCodeBox;
        private System.Windows.Forms.Label prtCodelabel;
        private System.Windows.Forms.Label visionCodelabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton rd_serialStatus;
    }
}

