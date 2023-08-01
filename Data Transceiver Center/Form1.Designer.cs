
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
            this.cmdTextBox = new System.Windows.Forms.TextBox();
            this.mesIDBox = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdTextBox
            // 
            this.cmdTextBox.Location = new System.Drawing.Point(24, 88);
            this.cmdTextBox.Name = "cmdTextBox";
            this.cmdTextBox.Size = new System.Drawing.Size(142, 21);
            this.cmdTextBox.TabIndex = 0;
            // 
            // mesIDBox
            // 
            this.mesIDBox.Location = new System.Drawing.Point(183, 452);
            this.mesIDBox.Name = "mesIDBox";
            this.mesIDBox.Size = new System.Drawing.Size(142, 21);
            this.mesIDBox.TabIndex = 1;
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
            this.getJsonButton.Location = new System.Drawing.Point(24, 426);
            this.getJsonButton.Name = "getJsonButton";
            this.getJsonButton.Size = new System.Drawing.Size(116, 44);
            this.getJsonButton.TabIndex = 4;
            this.getJsonButton.Text = "get JSON";
            this.getJsonButton.UseVisualStyleBackColor = true;
            this.getJsonButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // convertJsonButton
            // 
            this.convertJsonButton.Location = new System.Drawing.Point(24, 489);
            this.convertJsonButton.Name = "convertJsonButton";
            this.convertJsonButton.Size = new System.Drawing.Size(116, 44);
            this.convertJsonButton.TabIndex = 5;
            this.convertJsonButton.Text = "解析 JSON";
            this.convertJsonButton.UseVisualStyleBackColor = true;
            this.convertJsonButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // mesCmd1Button
            // 
            this.mesCmd1Button.Location = new System.Drawing.Point(183, 489);
            this.mesCmd1Button.Name = "mesCmd1Button";
            this.mesCmd1Button.Size = new System.Drawing.Size(116, 44);
            this.mesCmd1Button.TabIndex = 7;
            this.mesCmd1Button.Text = "MES通信1";
            this.mesCmd1Button.UseVisualStyleBackColor = true;
            this.mesCmd1Button.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(24, 362);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "解析内容";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(24, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "取JSON中特定项";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(24, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "打印内容";
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
            this.dataGridView1.Size = new System.Drawing.Size(388, 150);
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
            this.checkBox1.Location = new System.Drawing.Point(438, 93);
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
            this.label7.Location = new System.Drawing.Point(366, 474);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "串口数据";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(422, 471);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(167, 21);
            this.textBox3.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(366, 424);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "波特率";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(366, 386);
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
            this.button1.Location = new System.Drawing.Point(466, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 44);
            this.button1.TabIndex = 26;
            this.button1.Text = "打开串口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(368, 401);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 20);
            this.comboBox1.TabIndex = 27;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(368, 439);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(76, 20);
            this.comboBox2.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(366, 505);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "校验结果";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(422, 502);
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
            this.label11.Location = new System.Drawing.Point(438, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 14);
            this.label11.TabIndex = 31;
            this.label11.Text = "CSV文件状态";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(438, 115);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(102, 16);
            this.checkBox2.TabIndex = 32;
            this.checkBox2.Text = "打印后删除csv";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 569);
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
            this.Controls.Add(this.mesIDBox);
            this.Controls.Add(this.cmdTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cmdTextBox;
        private System.Windows.Forms.TextBox mesIDBox;
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
    }
}

