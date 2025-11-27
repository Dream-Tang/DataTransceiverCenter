using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Data_Transceiver_Center
{
    
    public partial class Form2 : Form
    {

         // 声明变量，存储信号值。定时刷新时，根据此变量设定rediobox
        private short rd_camRegisterValue;
        private short rd_prtRegisterValue;
        private short rd_scannerRegisterValue;

        // 在Form2类中添加私有变量（线程安全的标志位）
        private readonly object _lockObj = new object();
        private bool _isRefreshing = false;

        public Form2()
        {
            InitializeComponent();
        }

        #region "刷新ControlBox组件"，lock控制线程数量
        private void ReflashControlBox()
        {
            // 检查是否正在刷新，若正在执行则直接返回（避免重复创建线程）
            lock (_lockObj)
            {
                if (_isRefreshing)
                    return;
                _isRefreshing = true;
            }

            Task.Run(() => {
                try
                {
                    MethodInvoker mi = new MethodInvoker(() =>
                    {
                        try
                        {
                            rd_camRegisterValue = Convert.ToInt16(ReadDeviceRandom(CommunicationProtocol.camRegister));
                            rd_prtRegisterValue = Convert.ToInt16(ReadDeviceRandom(CommunicationProtocol.prtRegister));
                            rd_scannerRegisterValue = Convert.ToInt16(ReadDeviceRandom(CommunicationProtocol.scannerRegister));
                        }
                        catch (Exception)
                        {
                            timer1.Enabled = false;
                            checkBox1.Checked = false;
                            txt_LogicalStationNumber.Enabled = true;
                            Console.WriteLine("PLC掉线");
                            return;
                        }

                        rd_CamAllow.Checked = false;
                        rd_CamOK.Checked = false;
                        rd_CamNG.Checked = false;
                        switch (rd_camRegisterValue)
                        {
                            case CommunicationProtocol.camAllow:
                                rd_CamAllow.Checked = true;
                                break;

                            case CommunicationProtocol.camOK:
                                rd_CamOK.Checked = true;
                                break;

                            case CommunicationProtocol.camNG:
                                rd_CamNG.Checked = true;
                                break;
                        }

                        rd_PrtReady.Checked = false;
                        rd_PrtComplete.Checked = false;
                        switch (rd_prtRegisterValue)
                        {
                            case CommunicationProtocol.prtReady:
                                rd_PrtReady.Checked = true;
                                break;

                            case CommunicationProtocol.prtComplete:
                                rd_PrtComplete.Checked = true;
                                break;
                        }

                        rd_ScannerStart.Checked = false;
                        rd_ScannerComplete.Checked = false;
                        rd_checkOK.Checked = false;
                        rd_checkNG.Checked = false;
                        switch (rd_scannerRegisterValue)
                        {
                            case CommunicationProtocol.scannerStart:
                                rd_ScannerStart.Checked = true;
                                break;

                            case CommunicationProtocol.scannerComplete:
                                rd_ScannerComplete.Checked = true;
                                break;

                            case CommunicationProtocol.checkOK:
                                rd_checkOK.Checked = true;
                                break;

                            case CommunicationProtocol.checkNG:
                                rd_checkNG.Checked = true;
                                break;
                        }

                        label11.Text = rd_camRegisterValue.ToString();
                        label12.Text = rd_prtRegisterValue.ToString();
                        label13.Text = rd_scannerRegisterValue.ToString();
                    });
                    BeginInvoke(mi);
                }
                finally 
                {
                    // 任务执行完成后，重置标志位（无论成功失败都需释放）
                    lock (_lockObj)
                    {
                        _isRefreshing = false;
                    }
                }  
            });
        }

        #endregion "刷新寄存器rediobutton"

        public void btn_Close_Click(object sender, EventArgs e)
        {
            OpenPlcConnection();
        }

        public void btn_Open_Click(object sender, EventArgs e)
        {
            ClosePlcConnection();
        }

        public void OpenPlcConnection()
        {
            int iReturnCode;    //Return code
            checkBox1.Checked = false;
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

        public void ClosePlcConnection() 
        {
            int iReturnCode;				//Return code
            int iLogicalStationNumber;      //LogicalStationNumber for ActUtlType

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

        #region "Processing of ReadDeviceRandom2 button"

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

        #endregion "Processing of ReadDeviceRandom2 button"

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

        #endregion "Processing of WriteDeviceRandom2 button"

        #region "读取PLC寄存器数据"

        // 优化读取寄存器的函数，确保线程安全
        private string ReadDeviceRandom(string szDeviceName)
        {
            // 若当前线程不是UI线程，通过Invoke切换到UI线程执行
            if (this.InvokeRequired)
            {
                // 使用Func委托接收返回值
                return (string)this.Invoke(new Func<string, string>(ReadDeviceRandom), szDeviceName);
            }

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
                // 执行PLC读取操作（axActUtlType1可能需要在UI线程访问）
                iReturnCode = axActUtlType1.ReadDeviceRandom2(szDeviceName,
                                                                iNumberOfData,
                                                                out arrDeviceValue1);
                
                //The return code of the method is displayed by the hexadecimal.
                txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);

                //
                //Display the read data
                //
                //When the ReadDeviceRandom2 method is succeeded, display the read data.
                // 读取成功时返回数据
                if (iReturnCode == 0)
                {
                    arrData = arrDeviceValue1.ToString();
                    txt_Data.Text = arrData; // 更新数据显示
                }
                else
                {
                    arrData = "读取失败";
                }
            }

            //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "读取出错";
            }
            return arrData;
        }

        #endregion "读取PLC寄存器数据"

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

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, short>(WriteDeviceRandom), szDeviceName, arrDeviceValue);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);
        }

        #endregion "写入PLC寄存器数据"

        #region "Processing of clean display TextBox"

        private void ClearDisplay()
        {
            //Clear TextBox of 'ReturnCode','Data'
            txt_ReturnCode.Text = "";
            txt_Data.Text = "";
        }

        #endregion "Processing of clean display TextBox"

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

        #endregion "Processing of getting 32bit integer from TextBox"

        #region "Processing of getting ShortType array from StringType array of multiline TextBox"

        private bool GetShortArray(TextBox lptxt_SourceOfShortArray, out short[] lplpshShortArrayValue)
        {
            int iSizeOfShortArray;		//Size of ShortType array
            int iNumber;				//Loop counter

            //Get the size of ShortType array.
            iSizeOfShortArray = lptxt_SourceOfShortArray.Lines.Length;
            lplpshShortArrayValue = new short[iSizeOfShortArray];

            if (iSizeOfShortArray==0) { MessageBox.Show("写入数据为空，请设置数据");return false; };

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

        #endregion "Processing of getting ShortType array from StringType array of multiline TextBox"

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

        #endregion "Processing of OnDeviceStatus for ActUtlType Controle"

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
                    // 校验最小间隔（例如最小100ms，根据实际通信耗时调整）
                    if (timer1Interval < 100)
                    {
                        MessageBox.Show("刷新间隔过小，可能导致线程累积，请设置更大值（建议≥100ms）");
                        checkBox1.Checked = false;
                        return;
                    }
                    timer1.Interval = timer1Interval;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message + "\r\n刷新间隔设置有误");
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff"));
            Console.WriteLine("PLC寄存器读取 定时器 触发");
            ReflashControlBox();
        }

        #region "redioButton 控制按钮，点击则给PLC发送数据"

        private void txt_LogicStation_EnableChanged(object sender, EventArgs e)
        {
            if (txt_LogicalStationNumber.Enabled==false)
            {
                rd_CamAllow.Enabled = true;
                rd_CamNG.Enabled = true;
                rd_CamOK.Enabled = true;

                rd_PrtComplete.Enabled = true;
                rd_PrtReady.Enabled = true;

                rd_checkNG.Enabled = true;
                rd_checkOK.Enabled = true;

                rd_ScannerComplete.Enabled = true;
                rd_ScannerStart.Enabled = true;
            }
            else
            {
                rd_CamAllow.Enabled = false;
                rd_CamNG.Enabled = false;
                rd_CamOK.Enabled = false;

                rd_PrtComplete.Enabled = false;
                rd_PrtReady.Enabled = false;

                rd_checkNG.Enabled = false;
                rd_checkOK.Enabled = false;

                rd_ScannerComplete.Enabled = false;
                rd_ScannerStart.Enabled = false;
            }
        }

        private void rd_CamAllow_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamAllow.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.camRegister, CommunicationProtocol.camAllow);
                label11.Text = ReadDeviceRandom(CommunicationProtocol.camRegister);
            }
        }

        private void rd_CamOK_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamOK.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.camRegister, CommunicationProtocol.camOK);
                label11.Text = ReadDeviceRandom(CommunicationProtocol.camRegister);
            }
        }

        private void rd_CamNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamNG.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.camRegister, CommunicationProtocol.camNG);
                label11.Text = ReadDeviceRandom(CommunicationProtocol.camRegister);
            }
        }

        private void rd_PrtReady_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PrtReady.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.prtRegister, CommunicationProtocol.prtReady);
                label12.Text = ReadDeviceRandom(CommunicationProtocol.prtRegister);
            }
        }

        private void rd_PrtComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PrtComplete.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.prtRegister, CommunicationProtocol.prtComplete);
                label12.Text = ReadDeviceRandom(CommunicationProtocol.prtRegister);
            }
        }

        private void rd_ScannerStart_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ScannerStart.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.scannerStart);
                label13.Text = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
            }
        }

        private void rd_ScannerComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ScannerComplete.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.scannerComplete);
                label13.Text = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
            }
        }

        private void rd_checkOK_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_checkOK.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.checkOK);
                label13.Text = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
            }
        }

        private void rd_checkNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_checkNG.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.checkNG);
                label13.Text = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
            }
        }

        #endregion 

        // 发送数据
        public Tuple<short,short,short> ReadPlc()
        {
            if (txt_LogicalStationNumber.Enabled)
            {
                MessageBox.Show("与PLC连接未打开，请先进行连接");
                checkBox1.Checked = false;
                return new Tuple<short,short,short>(-1, -1, -1);
            }
            try
            {
               short rd_camValue = Convert.ToInt16(ReadDeviceRandom(CommunicationProtocol.camRegister));
               short rd_prtValue = Convert.ToInt16(ReadDeviceRandom(CommunicationProtocol.prtRegister));
               short rd_scnValue = Convert.ToInt16(ReadDeviceRandom(CommunicationProtocol.scannerRegister));

                return new Tuple<short, short, short>(rd_camValue, rd_prtValue, rd_scnValue);
            }
            catch (Exception)
            {
                return new Tuple<short, short, short>(-1, -1, -1);
            }
        }

        // 用于不跨线程时写PLC的方法（本页面调用）
        public void WritePlc(short camValue, short prtValue, short scnValue)
        {
            if (txt_LogicalStationNumber.Enabled)
            {
                MessageBox.Show("与PLC连接未打开，请先进行连接");
                checkBox1.Checked = false;
                return ;
            }
            try
            {
                WriteDeviceRandom(CommunicationProtocol.camRegister, camValue);
                WriteDeviceRandom(CommunicationProtocol.prtRegister,prtValue);
                WriteDeviceRandom(CommunicationProtocol.scannerRegister,scnValue);
            }
            catch (Exception)
            {
                return;
            }
        }

        // 用于跨线程调用的写PLC方法（其他页面调用）
        public void SafeWritePlc(short camValue, short prtValue, short scnValue)
        {
            if (this.InvokeRequired)
            {
                // 跨线程时通过Invoke切换到UI线程
                this.Invoke(new Action<short, short, short>(SafeWritePlc), camValue, prtValue, scnValue);
                return;
            }
            else
            {
                // 实际写入逻辑（原WritePlc的内容）
                if (txt_LogicalStationNumber.Enabled)
                {
                    MessageBox.Show("与PLC连接未打开，请先进行连接");
                    checkBox1.Checked = false;
                    return;
                }
                try
                {
                    WriteDeviceRandom(CommunicationProtocol.camRegister, camValue);
                    WriteDeviceRandom(CommunicationProtocol.prtRegister, prtValue);
                    WriteDeviceRandom(CommunicationProtocol.scannerRegister, scnValue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"写入PLC失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string GetReturnCode()
        {
            string returnCode = txt_ReturnCode.Text;
            return returnCode;
        }

    }
}