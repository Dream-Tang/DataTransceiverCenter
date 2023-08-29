
namespace Data_Transceiver_Center
{
    partial class Form4
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
            this.label1_6 = new System.Windows.Forms.Label();
            this.scnCode_txtBox = new System.Windows.Forms.TextBox();
            this.label1_5 = new System.Windows.Forms.Label();
            this.prtCode_txtBox = new System.Windows.Forms.TextBox();
            this.chckResult_txtBox = new System.Windows.Forms.TextBox();
            this.label3_4 = new System.Windows.Forms.Label();
            this.label1_4 = new System.Windows.Forms.Label();
            this.visionCode_txtBox = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.hightBox = new System.Windows.Forms.TextBox();
            this.widthBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1_6
            // 
            this.label1_6.AutoSize = true;
            this.label1_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_6.Location = new System.Drawing.Point(315, 59);
            this.label1_6.Name = "label1_6";
            this.label1_6.Size = new System.Drawing.Size(43, 14);
            this.label1_6.TabIndex = 61;
            this.label1_6.Text = "扫码枪";
            // 
            // scnCode_txtBox
            // 
            this.scnCode_txtBox.Location = new System.Drawing.Point(314, 75);
            this.scnCode_txtBox.Name = "scnCode_txtBox";
            this.scnCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.scnCode_txtBox.TabIndex = 60;
            // 
            // label1_5
            // 
            this.label1_5.AutoSize = true;
            this.label1_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_5.Location = new System.Drawing.Point(174, 59);
            this.label1_5.Name = "label1_5";
            this.label1_5.Size = new System.Drawing.Size(43, 14);
            this.label1_5.TabIndex = 59;
            this.label1_5.Text = "打印码";
            // 
            // prtCode_txtBox
            // 
            this.prtCode_txtBox.Location = new System.Drawing.Point(173, 75);
            this.prtCode_txtBox.Name = "prtCode_txtBox";
            this.prtCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.prtCode_txtBox.TabIndex = 58;
            this.prtCode_txtBox.TextChanged += new System.EventHandler(this.button2_Click);
            // 
            // chckResult_txtBox
            // 
            this.chckResult_txtBox.Location = new System.Drawing.Point(455, 75);
            this.chckResult_txtBox.Name = "chckResult_txtBox";
            this.chckResult_txtBox.Size = new System.Drawing.Size(167, 21);
            this.chckResult_txtBox.TabIndex = 57;
            // 
            // label3_4
            // 
            this.label3_4.AutoSize = true;
            this.label3_4.Location = new System.Drawing.Point(453, 59);
            this.label3_4.Name = "label3_4";
            this.label3_4.Size = new System.Drawing.Size(53, 12);
            this.label3_4.TabIndex = 56;
            this.label3_4.Text = "校验结果";
            // 
            // label1_4
            // 
            this.label1_4.AutoSize = true;
            this.label1_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1_4.Location = new System.Drawing.Point(30, 59);
            this.label1_4.Name = "label1_4";
            this.label1_4.Size = new System.Drawing.Size(55, 14);
            this.label1_4.TabIndex = 55;
            this.label1_4.Text = "视觉读码";
            // 
            // visionCode_txtBox
            // 
            this.visionCode_txtBox.Location = new System.Drawing.Point(29, 75);
            this.visionCode_txtBox.Name = "visionCode_txtBox";
            this.visionCode_txtBox.Size = new System.Drawing.Size(135, 21);
            this.visionCode_txtBox.TabIndex = 54;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(174, 102);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(134, 59);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 35);
            this.button2.TabIndex = 65;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // hightBox
            // 
            this.hightBox.Location = new System.Drawing.Point(327, 111);
            this.hightBox.Name = "hightBox";
            this.hightBox.Size = new System.Drawing.Size(49, 21);
            this.hightBox.TabIndex = 66;
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(381, 140);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(135, 21);
            this.widthBox.TabIndex = 67;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 551);
            this.Controls.Add(this.widthBox);
            this.Controls.Add(this.hightBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1_6);
            this.Controls.Add(this.scnCode_txtBox);
            this.Controls.Add(this.label1_5);
            this.Controls.Add(this.prtCode_txtBox);
            this.Controls.Add(this.chckResult_txtBox);
            this.Controls.Add(this.label3_4);
            this.Controls.Add(this.label1_4);
            this.Controls.Add(this.visionCode_txtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1_6;
        private System.Windows.Forms.TextBox scnCode_txtBox;
        private System.Windows.Forms.Label label1_5;
        private System.Windows.Forms.TextBox prtCode_txtBox;
        private System.Windows.Forms.TextBox chckResult_txtBox;
        private System.Windows.Forms.Label label3_4;
        private System.Windows.Forms.Label label1_4;
        private System.Windows.Forms.TextBox visionCode_txtBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox hightBox;
        private System.Windows.Forms.TextBox widthBox;
    }
}