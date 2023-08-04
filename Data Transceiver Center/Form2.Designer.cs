
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btn_ReadDeviceRandom2 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.txt_DeviceSizeRandom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_LogicalStationNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_ReturnCode = new System.Windows.Forms.TextBox();
            this.txt_Data = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad_ActUtlType = new System.Windows.Forms.RadioButton();
            this.axActUtlType1 = new AxActUtlTypeLib.AxActUtlType();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_DeviceNameRandom = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axActUtlType1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ReadDeviceRandom2
            // 
            this.btn_ReadDeviceRandom2.Location = new System.Drawing.Point(200, 341);
            this.btn_ReadDeviceRandom2.Name = "btn_ReadDeviceRandom2";
            this.btn_ReadDeviceRandom2.Size = new System.Drawing.Size(75, 50);
            this.btn_ReadDeviceRandom2.TabIndex = 0;
            this.btn_ReadDeviceRandom2.Text = "读出寄存器";
            this.btn_ReadDeviceRandom2.UseVisualStyleBackColor = true;
            this.btn_ReadDeviceRandom2.Click += new System.EventHandler(this.btn_ReadDeviceRandom2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(347, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "写入寄存器";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(301, 304);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 3;
            // 
            // txt_DeviceSizeRandom
            // 
            this.txt_DeviceSizeRandom.Location = new System.Drawing.Point(175, 303);
            this.txt_DeviceSizeRandom.Name = "txt_DeviceSizeRandom";
            this.txt_DeviceSizeRandom.Size = new System.Drawing.Size(100, 21);
            this.txt_DeviceSizeRandom.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "PLC寄存器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "数据大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "写入数据";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(174, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "PLC站号";
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(312, 16);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(75, 23);
            this.btn_Open.TabIndex = 10;
            this.btn_Open.Text = "连接";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(312, 45);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "中断";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_LogicalStationNumber
            // 
            this.txt_LogicalStationNumber.Location = new System.Drawing.Point(174, 30);
            this.txt_LogicalStationNumber.Name = "txt_LogicalStationNumber";
            this.txt_LogicalStationNumber.Size = new System.Drawing.Size(100, 21);
            this.txt_LogicalStationNumber.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "地址";
            // 
            // txt_ReturnCode
            // 
            this.txt_ReturnCode.Location = new System.Drawing.Point(416, 32);
            this.txt_ReturnCode.Name = "txt_ReturnCode";
            this.txt_ReturnCode.Size = new System.Drawing.Size(100, 21);
            this.txt_ReturnCode.TabIndex = 14;
            // 
            // txt_Data
            // 
            this.txt_Data.Location = new System.Drawing.Point(465, 302);
            this.txt_Data.Name = "txt_Data";
            this.txt_Data.Size = new System.Drawing.Size(100, 21);
            this.txt_Data.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "数据";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(414, 16);
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
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 130);
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
            this.axActUtlType1.Location = new System.Drawing.Point(0, 0);
            this.axActUtlType1.Name = "axActUtlType1";
            this.axActUtlType1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axActUtlType1.OcxState")));
            this.axActUtlType1.Size = new System.Drawing.Size(32, 32);
            this.axActUtlType1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(416, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "通信结果";
            // 
            // txt_DeviceNameRandom
            // 
            this.txt_DeviceNameRandom.Location = new System.Drawing.Point(32, 304);
            this.txt_DeviceNameRandom.Name = "txt_DeviceNameRandom";
            this.txt_DeviceNameRandom.Size = new System.Drawing.Size(100, 21);
            this.txt_DeviceNameRandom.TabIndex = 20;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 449);
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
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_ReadDeviceRandom2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axActUtlType1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ReadDeviceRandom2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox txt_DeviceSizeRandom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_LogicalStationNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_ReturnCode;
        private System.Windows.Forms.TextBox txt_Data;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rad_ActUtlType;

        private AxActUtlTypeLib.AxActUtlType axActUtlType1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_DeviceNameRandom;
    }
}