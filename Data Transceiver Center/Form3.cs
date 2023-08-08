﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{
    public partial class Form3 : Form
    {
        public Form1 f1;    // 创建窗口1 窗口变量
        public Form2 f2;    // 创建窗口2 窗口变量

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            f1 = new Form1();   // 实例化f1
            f2 = new Form2();   // 实例化f2
            f1.TopLevel = false;
            f2.TopLevel = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f1);    // 将窗体1加入容器panel1
            f1.Show();      // 将窗口1进行显示
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f2);    // 将窗体1加入容器panel1
            f2.Show();      // 将窗口1进行显示
        }
    }
}