﻿
namespace Data_Transceiver_Center
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Form1 = new System.Windows.Forms.Button();
            this.btn_Form2 = new System.Windows.Forms.Button();
            this.btn_Form3 = new System.Windows.Forms.Button();
            this.btn_SaveIni = new System.Windows.Forms.Button();
            this.btn_LoadIni = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(128, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 551);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "广告位招租";
            // 
            // btn_Form1
            // 
            this.btn_Form1.Location = new System.Drawing.Point(27, 25);
            this.btn_Form1.Name = "btn_Form1";
            this.btn_Form1.Size = new System.Drawing.Size(81, 57);
            this.btn_Form1.TabIndex = 1;
            this.btn_Form1.Text = "页面1";
            this.btn_Form1.UseVisualStyleBackColor = true;
            this.btn_Form1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Form2
            // 
            this.btn_Form2.Location = new System.Drawing.Point(27, 119);
            this.btn_Form2.Name = "btn_Form2";
            this.btn_Form2.Size = new System.Drawing.Size(81, 57);
            this.btn_Form2.TabIndex = 2;
            this.btn_Form2.Text = "页面2";
            this.btn_Form2.UseVisualStyleBackColor = true;
            this.btn_Form2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Form3
            // 
            this.btn_Form3.Location = new System.Drawing.Point(27, 224);
            this.btn_Form3.Name = "btn_Form3";
            this.btn_Form3.Size = new System.Drawing.Size(81, 57);
            this.btn_Form3.TabIndex = 3;
            this.btn_Form3.Text = "页面3";
            this.btn_Form3.UseVisualStyleBackColor = true;
            this.btn_Form3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_SaveIni
            // 
            this.btn_SaveIni.Location = new System.Drawing.Point(8, 524);
            this.btn_SaveIni.Name = "btn_SaveIni";
            this.btn_SaveIni.Size = new System.Drawing.Size(54, 39);
            this.btn_SaveIni.TabIndex = 4;
            this.btn_SaveIni.Text = "保存配置";
            this.btn_SaveIni.UseVisualStyleBackColor = true;
            this.btn_SaveIni.Click += new System.EventHandler(this.btnSaveIni_Click);
            // 
            // btn_LoadIni
            // 
            this.btn_LoadIni.Location = new System.Drawing.Point(68, 524);
            this.btn_LoadIni.Name = "btn_LoadIni";
            this.btn_LoadIni.Size = new System.Drawing.Size(54, 39);
            this.btn_LoadIni.TabIndex = 5;
            this.btn_LoadIni.Text = "加载配置";
            this.btn_LoadIni.UseVisualStyleBackColor = true;
            this.btn_LoadIni.Click += new System.EventHandler(this.btnLoadIni_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 575);
            this.Controls.Add(this.btn_LoadIni);
            this.Controls.Add(this.btn_SaveIni);
            this.Controls.Add(this.btn_Form3);
            this.Controls.Add(this.btn_Form2);
            this.Controls.Add(this.btn_Form1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "Data Transceiver Center";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Form1;
        private System.Windows.Forms.Button btn_Form2;
        private System.Windows.Forms.Button btn_Form3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SaveIni;
        private System.Windows.Forms.Button btn_LoadIni;
    }
}