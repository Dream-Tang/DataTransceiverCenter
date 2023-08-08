using System;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private static string camRegister = "D1000";      // 相机动作交互寄存器
        private static string prtRegister = "D1001";        // 打印机动作交互寄存器
        private static string scannerRegister = "D1002";  // 扫码枪动作交互寄存器

        private const short camAllow = 1;      // 允许相机拍照
        private const short camOK = 11;        // 扫描OK 放行
        private const short camNG = 12;        // 扫描NG 报警

        private const short prtReady = 1;      // 取标平台准备好
        private const short prtComplete = 11;  // 标打印完成

        private const short scannerStart = 1;    // 扫码开始
        private const short scannerComplete = 11;  // 扫码完成，标头返回
        private const short checkOK = 12;          // 判定数据OK，放行
        private const short checkNG = 13;          // 判定数据NG，报警

        // 定时刷新时，存储数据用，根绝数据改变rediobox
        private short rd_camRegisterValue;   
        private short rd_prtRegisterValue;      
        private short rd_scannerRegisterValue;

        #region "刷新寄存器rediobutton"
        private void ReflashControlBox()
        {   
            rd_camRegisterValue = Convert.ToInt16(ReadDeviceRandom(camRegister));
            rd_prtRegisterValue = Convert.ToInt16(ReadDeviceRandom(prtRegister));
            rd_scannerRegisterValue = Convert.ToInt16(ReadDeviceRandom(scannerRegister));

            rd_CamAllow.Checked = false;
            rd_CamOK.Checked = false;
            rd_CamNG.Checked = false;
            switch (rd_camRegisterValue)
            {
                case camAllow:
                    rd_CamAllow.Checked = true;
                    break;
                case camOK:
                    rd_CamOK.Checked = true;
                    break;
                case camNG:
                    rd_CamNG.Checked = true;
                    break;
            }

            rd_PrtReady.Checked = false;
            rd_PrtComplete.Checked = false;
            switch(rd_prtRegisterValue)
            {
                case prtReady:
                    rd_PrtReady.Checked = true;
                    break;
                case prtComplete:
                    rd_PrtComplete.Checked = true;
                    break;
            }

            rd_ScannerStart.Checked = false;
            rd_ScannerComplete.Checked = false;
            rd_checkOK.Checked = false;
            rd_checkNG.Checked = false;
            switch (rd_scannerRegisterValue)
            {
                case scannerStart:
                    rd_ScannerStart.Checked = true;
                    break;
                case scannerComplete:
                    rd_ScannerComplete.Checked = true;
                    break;
                case checkOK:
                    rd_checkOK.Checked = true;
                    break;
                case checkNG:
                    rd_checkNG.Checked = true;
                    break;
            }

            label11.Text = rd_camRegisterValue.ToString();
            label12.Text = rd_prtRegisterValue.ToString();
            label13.Text = rd_scannerRegisterValue.ToString();

        }

        #endregion

        private void btn_Open_Click(object sender, EventArgs e)
        {
            int iReturnCode;				//Return code
            int iLogicalStationNumber;		//LogicalStationNumber for ActUtlType
             
            //Displayed output data is cleared.
            ClearDisplay();

            //
            //Processing of Open method
            //
            try
            {
                //Check the 'LogicalStationNumber'.(If succeeded, the value is gotten.)
                if (GetIntValue(txt_LogicalStationNumber, out iLogicalStationNumber) != true)
                {
                    //If failed, this process is end.			
                    return;
                }

                //Set the value of 'LogicalStationNumber' to the property.
                axActUtlType1.ActLogicalStationNumber = iLogicalStationNumber;

                //Set the value of 'Password'.
                //axActUtlType1.ActPassword = txt_Password.Text;

                //The Open method is executed.
                iReturnCode = axActUtlType1.Open();
                //When the Open method is succeeded, disable the TextBox of 'LogocalStationNumber'.
                //When the Open method is succeeded, make the EventHandler of ActUtlType Controle.
                if (iReturnCode == 0)
                {
                    txt_LogicalStationNumber.Enabled = false;
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, 
                                 Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8} [HEX]", iReturnCode);
            switch (iReturnCode)
            {
                case 0:
                    label8.Text = "通信成功";
                    break;
                default:
                    label8.Text = "通信出错";
                    break;
            }

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            int iReturnCode;    //Return code
            
            //Displayed output data is cleared.
            ClearDisplay();

            //
            //Processing of Close method
            //
            try
            {
                //When ActUtlType is selected by the radio button,
                iReturnCode = axActUtlType1.Close();

                //When The Close method is succeeded, enable the TextBox of 'LogocalStationNumber'.
                if (iReturnCode == 0)
                {
                    txt_LogicalStationNumber.Enabled = true;
                }
            }
            //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8} [HEX]", iReturnCode);
            switch (iReturnCode)
            {
                case 0:
                    label8.Text = "通信成功";
                    break;
                default:
                    label8.Text = "通信出错";
                    break;
            }
        }

        #region  "Processing of ReadDeviceRandom2 button"
        private void btn_ReadDeviceRandom2_Click(object sender, EventArgs e)
        {
            int iReturnCode;				//Return code
            String szDeviceName = "";		//List data for 'DeviceName'
            int iNumberOfData = 0;			//Data for 'DeviceSize'
            short[] arrDeviceValue;		    //Data for 'DeviceValue
            int iNumber;					//Loop counter
            System.String[] arrData;	    //Array for 'Data'

            //Displayed output data is cleared.
            ClearDisplay();

            //Get the list of 'DeviceName'.
            //  Join each line(StringType array) of 'DeviceName' by the separator '\n',
            //  and create a joined string data.
            szDeviceName = String.Join("\n", txt_DeviceNameRandom.Lines);

            //Check the 'DeviceSize'.(If succeeded, the value is gotten.)
            if (!GetIntValue(txt_DeviceSizeRandom, out iNumberOfData))
            {
                //If failed, this process is end.
                return;
            }

            //Assign the array for 'DeviceValue'.
            arrDeviceValue = new short[iNumberOfData];

            //
            //Processing of ReadDeviceRandom2 method
            //
            try
            {
                //When ActProgType is selected by the radio button,
               
                {
                     //When ActUtlType is selected by the radio button,
                    //The ReadDeviceRandom2 method is executed.
                    iReturnCode = axActUtlType1.ReadDeviceRandom2(szDeviceName,
                                                                    iNumberOfData,
                                                                    out arrDeviceValue[0]);
                }
            }

            //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);

            //
            //Display the read data
            //
            //When the ReadDeviceRandom2 method is succeeded, display the read data.
            if (iReturnCode == 0)
            {
                //Assign the array for the read data.
                arrData = new System.String[iNumberOfData];

                //Copy the read data to the 'arrData'.
                for (iNumber = 0; iNumber < iNumberOfData; iNumber++)
                {

                    arrData[iNumber] = arrDeviceValue[iNumber].ToString();

                }
                //Set the read data to the 'Data', and display it.
                txt_Data.Lines = arrData;
            }

        }
        #endregion

        #region "Processing of WriteDeviceRandom2 button"
        private void btn_WriteDeviceRandom2_Click(object sender, EventArgs e)
        {
            int iReturnCode;				//Return code
            String szDeviceName = "";		//List data for 'DeviceName'
            int iNumberOfData = 0;			//Data for 'DeviceSize'
            short[] arrDeviceValue;		    //Data for 'DeviceValue'
            int iNumber;					//Loop counter


            //Displayed output data is cleared.
            ClearDisplay();

            //Get the list of 'DeviceName'.
            //  Join each line(StringType array) of 'DeviceName' by the separator '\n',
            //  and create a joined string data.
            szDeviceName = String.Join("\n", txt_DeviceNameRandom.Lines);

            //Check the 'DeviceSize'.(If succeeded, the value is gotten.)
            if (!GetIntValue(txt_DeviceSizeRandom, out iNumberOfData))
            {
                //If failed, this process is end.
                return;
            }

            //Check the 'DeviceValue'.(If succeeded, the value is gotten.)
            arrDeviceValue = new short[iNumberOfData];
            if (!GetShortArray(txt_DeviceDataRandom, out arrDeviceValue))
            {
                //If failed, this process is end.
                return;
            }

            //Set the 'DeviceVale'.
            for (iNumber = 0; iNumber < iNumberOfData; iNumber++)
            {
                arrDeviceValue[iNumber] = arrDeviceValue[iNumber];
            }

            //
            //Processing of WriteDeviceRandom2 method
            //
            try
            {
                //When ActUtlType is selected by the radio button,
               {
                    //The WriteDeviceRandom2 method is executed.
                    iReturnCode = axActUtlType1.WriteDeviceRandom2(szDeviceName,
                                                                  iNumberOfData,
                                                                  ref arrDeviceValue[0]);
                }
            }

            //Exception processing			
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);

        }
        #endregion


        #region "读取PLC寄存器数据"
        private string ReadDeviceRandom(string szDeviceName)
        {
            int iReturnCode;				//Return code
            //String szDeviceName = "";		//List data for 'DeviceName'
            int iNumberOfData = 1;			//Data for 'DeviceSize'
            short arrDeviceValue1;
            System.String arrData;	    //Array for 'Data'

            //Get the list of 'DeviceName'.
            //  Join each line(StringType array) of 'DeviceName' by the separator '\n',
            //  and create a joined string data.
            szDeviceName = String.Join("\n", szDeviceName);

            //
            //Processing of ReadDeviceRandom2 method
            //
            try
            {
                //When ActProgType is selected by the radio button,

                //When ActUtlType is selected by the radio button,
                //The ReadDeviceRandom2 method is executed.
                iReturnCode = axActUtlType1.ReadDeviceRandom2(szDeviceName,
                                                                iNumberOfData,
                                                                out arrDeviceValue1);
            }

            //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "读取出错";
            }

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);

            //
            //Display the read data
            //
            //When the ReadDeviceRandom2 method is succeeded, display the read data.
            if (iReturnCode == 0)
            {
                //Assign the array for the read data.
                arrData = arrDeviceValue1.ToString();
                return arrData;
            }
            else
                return "读取出错";
        }
        #endregion

        #region "写入PLC寄存器数据"
        private void WriteDeviceRandom(string szDeviceName, short arrDeviceValue)
        {
            int iReturnCode;				//Return code
            //String szDeviceName = "";		//List data for 'DeviceName'
            int iNumberOfData = 1;			//Data for 'DeviceSize'
            //short arrDeviceValue;		    //Data for 'DeviceValue'

            //Get the list of 'DeviceName'.
            //  Join each line(StringType array) of 'DeviceName' by the separator '\n',
            //  and create a joined string data.
            szDeviceName = String.Join("\n", szDeviceName);

            //Check the 'DeviceValue'.(If succeeded, the value is gotten.)

            //Set the 'DeviceVale'.

            //
            //Processing of WriteDeviceRandom2 method
            //
            try
            {
                //When ActUtlType is selected by the radio button,
                //The WriteDeviceRandom2 method is executed.
                iReturnCode = axActUtlType1.WriteDeviceRandom2(szDeviceName,
                                                                iNumberOfData,
                                                                ref arrDeviceValue);
            }

            //Exception processing			
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);
        }
        #endregion


        #region "Processing of clean display TextBox"
        private void ClearDisplay()
        {
            //Clear TextBox of 'ReturnCode','Data'
            txt_ReturnCode.Text = "";
            txt_Data.Text = "";
        }

        #endregion

        #region "Processing of getting 32bit integer from TextBox"
        private bool GetIntValue(TextBox lptxt_SourceOfIntValue, out int iGottenIntValue)
        {
            iGottenIntValue = 0;
            //Get the value as 32bit integer from a TextBox
            try
            {
                iGottenIntValue = Convert.ToInt32(lptxt_SourceOfIntValue.Text);
            }

            //When the value is nothing or out of the range, the exception is processed.
            catch (Exception exExcepion)
            {
                MessageBox.Show(exExcepion.Message,
                                  Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Normal End
            return true;
        }
       
        #endregion

        #region "Processing of getting ShortType array from StringType array of multiline TextBox"

        private bool GetShortArray(TextBox lptxt_SourceOfShortArray, out short[] lplpshShortArrayValue)
        {
            int iSizeOfShortArray;		//Size of ShortType array
            int iNumber;				//Loop counter

            //Get the size of ShortType array.
            iSizeOfShortArray = lptxt_SourceOfShortArray.Lines.Length;
            lplpshShortArrayValue = new short[iSizeOfShortArray];

            //Get each element of ShortType array.
            for (iNumber = 0; iNumber < iSizeOfShortArray; iNumber++)
            {
                try
                {
                    lplpshShortArrayValue[iNumber]
                        = Convert.ToInt16(lptxt_SourceOfShortArray.Lines[iNumber]);
                }

                //When the value is nothing or out of the range, the exception is processed.
                catch (Exception exExcepion)
                {
                    MessageBox.Show(exExcepion.Message,
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //Normal End
            return true;
        }

        #endregion

        #region "Processing of OnDeviceStatus for ActUtlType Controle"
        private void ActUtlType1_OnDeviceStatus(System.Object sender, AxActUtlTypeLib._IActUtlTypeEvents_OnDeviceStatusEvent e)
        {
            System.String[] arrData;
            //Assign the array for editing the data of 'Data'.
            arrData = new System.String[txt_Data.Lines.Length + 1];

            //Set the lateset data of 'Data' to arrData.
            Array.Copy(txt_Data.Lines, arrData, txt_Data.Lines.Length);

            //Add the content of new event to arrData.
            arrData[txt_Data.Lines.Length]
            = String.Format("OnDeviceStatus event by ActUtlType [{0}={1}]", e.szDevice, e.lData);

            //The new 'Data' is displayed.
            txt_Data.Lines = arrData;

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8}", e.lReturnCode);
        }


        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff"));
            Console.WriteLine("timer1 触发");
            ReflashControlBox();
        }

        #region "redioButton 控制按钮，点击写入数据"
        private void rd_CamAllow_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamAllow.Checked)
            {
                WriteDeviceRandom(camRegister, camAllow);
                label11.Text = ReadDeviceRandom(camRegister);
            }
        }

        private void rd_CamOK_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamOK.Checked)
            {
                WriteDeviceRandom(camRegister, camOK);
                label11.Text = ReadDeviceRandom(camRegister);
            }
           
        }

        private void rd_CamNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamNG.Checked)
            {
                WriteDeviceRandom(camRegister, camNG);
                label11.Text = ReadDeviceRandom(camRegister);
            }
            
        }

        private void rd_PrtReady_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PrtReady.Checked)
            {
                WriteDeviceRandom(prtRegister, prtReady);
                label12.Text = ReadDeviceRandom(prtRegister);
            }
        }

        private void rd_PrtComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PrtComplete.Checked)
            {
                WriteDeviceRandom(prtRegister, prtComplete);
                label12.Text = ReadDeviceRandom(prtRegister);
            }
        }

        private void rd_ScannerStart_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ScannerStart.Checked)
            {
                WriteDeviceRandom(scannerRegister, scannerStart);
                label13.Text = ReadDeviceRandom(scannerRegister);
            }
           
        }

        private void rd_ScannerComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ScannerComplete.Checked)
            {
                WriteDeviceRandom(scannerRegister, scannerComplete);
                label13.Text = ReadDeviceRandom(scannerRegister);
            }
            
        }

        private void rd_checkOK_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_checkOK.Checked)
            {
                WriteDeviceRandom(scannerRegister, checkOK);
                label13.Text = ReadDeviceRandom(scannerRegister);
            }
            
        }

        private void rd_checkNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_checkNG.Checked)
            {
                WriteDeviceRandom(scannerRegister, checkNG);
                label13.Text = ReadDeviceRandom(scannerRegister);
            }
            
        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                if (txt_LogicalStationNumber.Enabled)
                {
                    MessageBox.Show("与PLC连接未打开，请先进行连接");
                    checkBox1.Checked = false;
                    return;
                }
                MessageBox.Show("自动刷新开启");
                try
                {
                    int timer1Interval = Convert.ToInt32(txt_Timer1Interval.Text);
                    timer1.Interval = timer1Interval;
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message+"\r\n刷新间隔设置有误");
                }
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txt_Timer1Interval.Text = Convert.ToString(timer1.Interval);
        }
    }
}
