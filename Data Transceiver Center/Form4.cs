using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;


namespace Data_Transceiver_Center
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 将生成的条码设置到pictureBox
        /// </summary>
        /// <param name="barCodeStr">条形码对应的字符串</param>

        public static Image SetBarCode128(string barCodeStr, int height=40)
        {
            try
            {
                EncodingOptions encodingOptions = new EncodingOptions();
                //encodingOptions.Width = width;
                encodingOptions.Height = 40;
                ZXing.BarcodeWriter wr = new ZXing.BarcodeWriter();
                wr.Options = encodingOptions;
                wr.Format = ZXing.BarcodeFormat.CODE_128;
                Bitmap barCodeImg = wr.Write(barCodeStr);
                return barCodeImg;
            }
            catch (Exception)
            {
                // MessageBox.Show("barCode转换出错，存在无法转换的字符");
                Console.WriteLine("barCode转换出错，存在无法转换的字符");
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string barCodeStr = prtCode_txtBox.Text;
            //int width = Convert.ToUInt16(this.scnCode_txtBox.Text);
            int height = Convert.ToUInt16(this.chckResult_txtBox.Text);
            Image img = SetBarCode128(barCodeStr);
            pictureBox2.Image = img;
        }
    }
}
