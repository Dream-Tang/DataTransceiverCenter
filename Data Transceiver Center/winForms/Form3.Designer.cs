
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_mesUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBox_stepId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBox_lineId = new System.Windows.Forms.TextBox();
            this.lable5 = new System.Windows.Forms.Label();
            this.txtBox_panelId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBox_eqpId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBox_fixture = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2_2 = new System.Windows.Forms.Label();
            this.txtBox_responseData = new System.Windows.Forms.TextBox();
            this.label2_1 = new System.Windows.Forms.Label();
            this.txtBox_postData = new System.Windows.Forms.TextBox();
            this.btn_apiTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_SaveJson = new System.Windows.Forms.Button();
            this.btn_LoadJson = new System.Windows.Forms.Button();
            this.pnl_InputParameters = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_InputParameters.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "接口URL";
            // 
            // txtBox_mesUrl
            // 
            this.txtBox_mesUrl.Location = new System.Drawing.Point(12, 72);
            this.txtBox_mesUrl.Name = "txtBox_mesUrl";
            this.txtBox_mesUrl.Size = new System.Drawing.Size(674, 28);
            this.txtBox_mesUrl.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "stepId(站点编号)";
            // 
            // txtBox_stepId
            // 
            this.txtBox_stepId.Location = new System.Drawing.Point(166, 29);
            this.txtBox_stepId.Name = "txtBox_stepId";
            this.txtBox_stepId.Size = new System.Drawing.Size(150, 28);
            this.txtBox_stepId.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(10, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "lineId(线体编号)";
            // 
            // txtBox_lineId
            // 
            this.txtBox_lineId.Location = new System.Drawing.Point(166, 66);
            this.txtBox_lineId.Name = "txtBox_lineId";
            this.txtBox_lineId.Size = new System.Drawing.Size(150, 28);
            this.txtBox_lineId.TabIndex = 5;
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable5.Location = new System.Drawing.Point(29, 107);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(123, 24);
            this.lable5.TabIndex = 10;
            this.lable5.Text = "panelId(条码)";
            // 
            // txtBox_panelId
            // 
            this.txtBox_panelId.Location = new System.Drawing.Point(166, 103);
            this.txtBox_panelId.Name = "txtBox_panelId";
            this.txtBox_panelId.Size = new System.Drawing.Size(488, 28);
            this.txtBox_panelId.TabIndex = 9;
            this.txtBox_panelId.TextChanged += new System.EventHandler(this.txtBox_panelId_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(348, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "eqpId(机台编号)";
            // 
            // txtBox_eqpId
            // 
            this.txtBox_eqpId.Location = new System.Drawing.Point(504, 28);
            this.txtBox_eqpId.Name = "txtBox_eqpId";
            this.txtBox_eqpId.Size = new System.Drawing.Size(150, 28);
            this.txtBox_eqpId.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(348, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 24);
            this.label7.TabIndex = 12;
            this.label7.Text = "fixture(治具)";
            // 
            // txtBox_fixture
            // 
            this.txtBox_fixture.Location = new System.Drawing.Point(504, 62);
            this.txtBox_fixture.Name = "txtBox_fixture";
            this.txtBox_fixture.Size = new System.Drawing.Size(150, 28);
            this.txtBox_fixture.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(14, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 26);
            this.label8.TabIndex = 13;
            this.label8.Text = "input(业务参数)";
            // 
            // label2_2
            // 
            this.label2_2.AutoSize = true;
            this.label2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2_2.Location = new System.Drawing.Point(3, 140);
            this.label2_2.Name = "label2_2";
            this.label2_2.Size = new System.Drawing.Size(140, 25);
            this.label2_2.TabIndex = 57;
            this.label2_2.Text = "收到MES的回复";
            // 
            // txtBox_responseData
            // 
            this.txtBox_responseData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_responseData.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_responseData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_responseData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_responseData.Location = new System.Drawing.Point(3, 168);
            this.txtBox_responseData.Multiline = true;
            this.txtBox_responseData.Name = "txtBox_responseData";
            this.txtBox_responseData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_responseData.Size = new System.Drawing.Size(772, 110);
            this.txtBox_responseData.TabIndex = 56;
            // 
            // label2_1
            // 
            this.label2_1.AutoSize = true;
            this.label2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2_1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2_1.Location = new System.Drawing.Point(3, 0);
            this.label2_1.Name = "label2_1";
            this.label2_1.Size = new System.Drawing.Size(140, 25);
            this.label2_1.TabIndex = 55;
            this.label2_1.Text = "发给MES的消息";
            // 
            // txtBox_postData
            // 
            this.txtBox_postData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_postData.BackColor = System.Drawing.SystemColors.Info;
            this.txtBox_postData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_postData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_postData.Location = new System.Drawing.Point(3, 28);
            this.txtBox_postData.Multiline = true;
            this.txtBox_postData.Name = "txtBox_postData";
            this.txtBox_postData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBox_postData.Size = new System.Drawing.Size(772, 109);
            this.txtBox_postData.TabIndex = 54;
            // 
            // btn_apiTest
            // 
            this.btn_apiTest.Location = new System.Drawing.Point(700, 59);
            this.btn_apiTest.Name = "btn_apiTest";
            this.btn_apiTest.Size = new System.Drawing.Size(90, 50);
            this.btn_apiTest.TabIndex = 58;
            this.btn_apiTest.Text = "通信测试";
            this.btn_apiTest.UseVisualStyleBackColor = true;
            this.btn_apiTest.Click += new System.EventHandler(this.btn_apiTest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(300, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 46);
            this.label2.TabIndex = 59;
            this.label2.Text = "MES设置页面";
            // 
            // btn_SaveJson
            // 
            this.btn_SaveJson.Location = new System.Drawing.Point(700, 276);
            this.btn_SaveJson.Name = "btn_SaveJson";
            this.btn_SaveJson.Size = new System.Drawing.Size(90, 50);
            this.btn_SaveJson.TabIndex = 60;
            this.btn_SaveJson.Text = "保存配置";
            this.btn_SaveJson.UseVisualStyleBackColor = true;
            this.btn_SaveJson.Click += new System.EventHandler(this.btnSaveJson_Click);
            // 
            // btn_LoadJson
            // 
            this.btn_LoadJson.Location = new System.Drawing.Point(700, 155);
            this.btn_LoadJson.Name = "btn_LoadJson";
            this.btn_LoadJson.Size = new System.Drawing.Size(90, 50);
            this.btn_LoadJson.TabIndex = 61;
            this.btn_LoadJson.Text = "加载配置";
            this.btn_LoadJson.UseVisualStyleBackColor = true;
            this.btn_LoadJson.Click += new System.EventHandler(this.btnLoadJson_Click);
            // 
            // pnl_InputParameters
            // 
            this.pnl_InputParameters.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_InputParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_InputParameters.Controls.Add(this.label6);
            this.pnl_InputParameters.Controls.Add(this.txtBox_stepId);
            this.pnl_InputParameters.Controls.Add(this.label3);
            this.pnl_InputParameters.Controls.Add(this.txtBox_lineId);
            this.pnl_InputParameters.Controls.Add(this.label4);
            this.pnl_InputParameters.Controls.Add(this.txtBox_eqpId);
            this.pnl_InputParameters.Controls.Add(this.txtBox_panelId);
            this.pnl_InputParameters.Controls.Add(this.lable5);
            this.pnl_InputParameters.Controls.Add(this.txtBox_fixture);
            this.pnl_InputParameters.Controls.Add(this.label7);
            this.pnl_InputParameters.Location = new System.Drawing.Point(12, 155);
            this.pnl_InputParameters.Name = "pnl_InputParameters";
            this.pnl_InputParameters.Size = new System.Drawing.Size(676, 171);
            this.pnl_InputParameters.TabIndex = 62;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2_1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBox_postData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2_2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBox_responseData, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 357);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 281);
            this.tableLayoutPanel1.TabIndex = 63;
            // 
            // Form3
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pnl_InputParameters);
            this.Controls.Add(this.btn_LoadJson);
            this.Controls.Add(this.btn_SaveJson);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_apiTest);
            this.Controls.Add(this.txtBox_mesUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.Text = "MES设置页面";
            this.pnl_InputParameters.ResumeLayout(false);
            this.pnl_InputParameters.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBox_fixture;
        private System.Windows.Forms.Label lable5;
        private System.Windows.Forms.TextBox txtBox_panelId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBox_eqpId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBox_lineId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBox_stepId;
        private System.Windows.Forms.TextBox txtBox_mesUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2_2;
        private System.Windows.Forms.TextBox txtBox_responseData;
        private System.Windows.Forms.Label label2_1;
        private System.Windows.Forms.TextBox txtBox_postData;
        private System.Windows.Forms.Button btn_apiTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_LoadJson;
        private System.Windows.Forms.Button btn_SaveJson;
        private System.Windows.Forms.Panel pnl_InputParameters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}