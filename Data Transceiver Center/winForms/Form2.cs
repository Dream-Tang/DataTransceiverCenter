using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{

    public partial class Form2 : Form, IPLCService
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

            Task.Run(() =>
            {
                try
                {
                    // 用Invoke替代BeginInvoke，确保同步更新UI
                    this.Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            // 逐个读取并捕获具体异常
                            try
                            {
                                // 读取相机寄存器值（适配可空类型）
                                var camValue = ReadDeviceRandom(CommunicationProtocol.camRegister);
                                if (!camValue.HasValue)
                                {
                                    throw new Exception($"相机寄存器读取失败 | 地址：{CommunicationProtocol.camRegister}");
                                }
                                rd_camRegisterValue = camValue.Value;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"相机寄存器读取失败: {ex.Message}");
                            }
                            try
                            {
                                // 读取打印寄存器值（适配可空类型）
                                var prtValue = ReadDeviceRandom(CommunicationProtocol.prtRegister);
                                if (!prtValue.HasValue)
                                {
                                    throw new Exception($"打印寄存器读取失败 | 地址：{CommunicationProtocol.prtRegister}");
                                }
                                rd_prtRegisterValue = prtValue.Value;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"打印寄存器读取失败: {ex.Message}");
                            }
                            try
                            {
                                // 读取扫描寄存器值（适配可空类型）
                                var scannerValue = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
                                if (!scannerValue.HasValue)
                                {
                                    throw new Exception($"扫描寄存器读取失败 | 地址：{CommunicationProtocol.scannerRegister}");
                                }
                                rd_scannerRegisterValue = scannerValue.Value;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"扫描寄存器读取失败: {ex.Message}");
                            }
                        }
                        catch (Exception ex)
                        {
                            timer1.Enabled = false;
                            checkBox1.Checked = false;
                            txt_LogicalStationNumber.Enabled = true;
                            Console.WriteLine($"PLC通信异常: {ex.Message}"); // 输出具体异常
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
                    }));
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
            checkBox1.Checked = false;
            timer1.Enabled = false;
            ClosePlcConnection();
        }

        public void btn_Open_Click(object sender, EventArgs e)
        {
            OpenPlcConnection();
        }

        public void ClosePlcConnection()
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

        public void OpenPlcConnection()
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

        /// <summary>
        /// 线程安全的PLC寄存器读取方法（读取单个寄存器值）
        /// </summary>
        /// <param name="szDeviceName">PLC寄存器地址（如D100、M0等）</param>
        /// <returns>成功返回寄存器的short类型值；失败返回null（表示读取失败）</returns>
        private short? ReadDeviceRandom(string szDeviceName)
        {
            // 跨线程校验：若当前线程非UI线程，切换到UI线程执行（ActiveX控件必须在UI线程操作）
            if (this.InvokeRequired)
            {
                return (short?)this.Invoke(new Func<string, short?>(ReadDeviceRandom), szDeviceName);
            }

            // 局部变量初始化
            int iReturnCode; // PLC通信返回码（0表示成功）
            int iNumberOfData = 1; // 读取的数据长度（固定为1，读取单个寄存器）
            short arrDeviceValue1; // 存储读取到的寄存器值
            szDeviceName = String.Join("\n", szDeviceName); // 格式化寄存器地址（适配PLC通信要求）

            try
            {
                // 调用ActiveX控件读取PLC寄存器（核心操作，必须在UI线程执行）
                iReturnCode = axActUtlType1.ReadDeviceRandom2(szDeviceName, iNumberOfData, out arrDeviceValue1);

                // 更新UI显示返回码（16进制格式）
                txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode);

                // 判断读取结果
                if (iReturnCode == 0)
                {
                    // 读取成功：更新数据显示框，返回读取到的数值
                    txt_Data.Text = arrDeviceValue1.ToString();
                    return arrDeviceValue1;
                }
                else
                {
                    // 读取失败：输出日志，返回null标识失败
                    Console.WriteLine($"PLC寄存器读取失败 | 寄存器地址：{szDeviceName} | 返回码：{iReturnCode}");
                    return null;
                }
            }
            catch (Exception exception)
            {
                // 捕获通信异常：弹窗提示，返回null标识失败
                MessageBox.Show(exception.Message, Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #endregion "读取PLC寄存器数据"

        #region "写入PLC寄存器数据"

        private void WriteDeviceRandom(string szDeviceName, short arrDeviceValue)
        {
            // 先判断是否需要跨线程调用，确保axActUtlType1在UI线程操作
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, short>(WriteDeviceRandom), szDeviceName, arrDeviceValue);
                return;
            }

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
                //The return code of the method is displayed by the hexadecimal.
                txt_ReturnCode.Text = String.Format("0x{0:x8}", iReturnCode); // 仅UI线程更新控件
            }

            //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            if (iSizeOfShortArray == 0) { MessageBox.Show("写入数据为空，请设置数据"); return false; }
            ;

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
            if (!IsPlcConnected())
            {
                timer1.Stop(); // 若已断开，直接停止定时器
                timer1.Enabled = false;
                return;
            }
            // 在定时器触发时先检查是否正在刷新，避免重复触发任务
            lock (_lockObj)
            {
                if (_isRefreshing)
                {
                    Console.WriteLine("前一次刷新未完成，跳过本次触发");
                    return;
                }
            }
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}: PLC寄存器读取定时器触发");
            ReflashControlBox();
        }

        // 检查PLC连接状态
        private bool IsPlcConnected()
        {
            // 连接成功时，txt_LogicalStationNumber会被禁用（参考OpenPlcConnection逻辑）
            return !txt_LogicalStationNumber.Enabled && axActUtlType1 != null;
        }

        #region "redioButton 控制按钮，点击则给PLC发送数据"

        private void txt_LogicStation_EnableChanged(object sender, EventArgs e)
        {
            if (txt_LogicalStationNumber.Enabled == false)
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
                // 读取寄存器并更新标签（适配可空类型）
                var camValue = ReadDeviceRandom(CommunicationProtocol.camRegister);
                label11.Text = camValue.HasValue ? camValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_CamOK_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamOK.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.camRegister, CommunicationProtocol.camOK);
                // 读取寄存器并更新标签（适配可空类型）
                var camValue = ReadDeviceRandom(CommunicationProtocol.camRegister);
                label11.Text = camValue.HasValue ? camValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_CamNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CamNG.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.camRegister, CommunicationProtocol.camNG);
                // 读取寄存器并更新标签（适配可空类型）
                var camValue = ReadDeviceRandom(CommunicationProtocol.camRegister);
                label11.Text = camValue.HasValue ? camValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_PrtReady_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PrtReady.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.prtRegister, CommunicationProtocol.prtReady);
                // 读取寄存器并更新标签（适配可空类型）
                var prtValue = ReadDeviceRandom(CommunicationProtocol.prtRegister);
                label12.Text = prtValue.HasValue ? prtValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_PrtComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PrtComplete.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.prtRegister, CommunicationProtocol.prtComplete);
                // 读取寄存器并更新标签（适配可空类型）
                var prtValue = ReadDeviceRandom(CommunicationProtocol.prtRegister);
                label12.Text = prtValue.HasValue ? prtValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_ScannerStart_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ScannerStart.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.scannerStart);
                // 读取寄存器并更新标签（适配可空类型）
                var scnValue = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
                label13.Text = scnValue.HasValue ? scnValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_ScannerComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_ScannerComplete.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.scannerComplete);
                // 读取寄存器并更新标签（适配可空类型）
                var scnValue = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
                label13.Text = scnValue.HasValue ? scnValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_checkOK_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_checkOK.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.checkOK);
                // 读取寄存器并更新标签（适配可空类型）
                var scnValue = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
                label13.Text = scnValue.HasValue ? scnValue.Value.ToString() : "读取失败";
            }
        }

        private void rd_checkNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_checkNG.Checked)
            {
                WriteDeviceRandom(CommunicationProtocol.scannerRegister, CommunicationProtocol.checkNG);
                // 读取寄存器并更新标签（适配可空类型）
                var scnValue = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
                label13.Text = scnValue.HasValue ? scnValue.Value.ToString() : "读取失败";
            }
        }

        #endregion 

        // 读取寄存器数值
        public Tuple<short, short, short> ReadPlc()
        {
            if (!IsPlcConnected())
            {
                Console.WriteLine("与PLC连接未打开，请先进行连接");
                checkBox1.Checked = false;
                return new Tuple<short, short, short>(-1, -1, -1);
            }
            try
            {
                // 读取相机寄存器
                var camValue = ReadDeviceRandom(CommunicationProtocol.camRegister);
                if (!camValue.HasValue)
                {
                    throw new Exception("读取PLC相机寄存器失败");
                }
                short cam = camValue.Value;

                // 读取打印寄存器
                var prtValue = ReadDeviceRandom(CommunicationProtocol.prtRegister);
                if (!prtValue.HasValue)
                {
                    throw new Exception("读取PLC打印寄存器失败");
                }
                short prt = prtValue.Value;

                // 读取扫描寄存器
                var scnValue = ReadDeviceRandom(CommunicationProtocol.scannerRegister);
                if (!scnValue.HasValue)
                {
                    throw new Exception("读取PLC扫描寄存器失败");
                }
                short scn = scnValue.Value;

                return new Tuple<short, short, short>(cam, prt, scn);
            }
            catch (Exception)
            {
                return new Tuple<short, short, short>(-1, -1, -1);
            }
        }

        #region 线程安全的PLC读写方法
        /// <summary>
        /// 线程安全的PLC读取方法
        /// </summary>
        /// <returns>包含cam、prt、scn值的元组</returns>
        public Tuple<short, short, short> SafeReadPlc()
        {
            if (this.InvokeRequired)
            {
                // 跨线程时通过Invoke切换到UI线程执行
                return (Tuple<short, short, short>)this.Invoke(
                    new Func<Tuple<short, short, short>>(SafeReadPlc));
            }
            else
            {
                // 非跨线程时直接调用原读取逻辑
                return ReadPlc();
            }
        }
        // 用于不跨线程时写PLC的方法（本页面调用）
        public void WritePlc(short camValue, short prtValue, short scnValue)
        {
            if (!IsPlcConnected())
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
            catch (Exception)
            {
                return;
            }
        }

        // 用于跨线程调用的写PLC方法（其他页面调用）
        /// <summary>
        /// 线程安全的PLC写入方法
        /// </summary>
        /// <param name="camValue">相机状态值</param>
        /// <param name="prtValue">打印状态值</param>
        /// <param name="scnValue">扫描状态值</param>
        public void SafeWritePlc(short camValue, short prtValue, short scnValue)
        {
            if (this.InvokeRequired)
            {
                // 跨线程时通过Invoke切换到UI线程执行
                this.Invoke(new Action<short, short, short>(SafeWritePlc),
                    camValue, prtValue, scnValue);
            }
            else
            {
                // 非跨线程时直接调用原写入逻辑
                WritePlc(camValue, prtValue, scnValue);
            }
        }

        #region "读取指定位置寄存器值"
        /// <summary>
        /// 读取指定地址的PLC寄存器值（线程安全）
        /// </summary>
        /// <param name="deviceName">寄存器地址（如"D100"）</param>
        /// <returns>成功返回short值，失败返回null</returns>
        public short? ReadSpecificPlcRegister(string deviceName)
        {
            if (this.InvokeRequired)
            {
                // 跨线程调用时切换到UI线程
                return (short?)this.Invoke(new Func<string, short?>(ReadSpecificPlcRegister), deviceName);
            }

            // 检查PLC连接状态
            if (txt_LogicalStationNumber.Enabled)
            {
                Console.WriteLine("PLC未连接，无法读取寄存器");
                return null;
            }

            try
            {
                // 直接复用已有的ReadDeviceRandom方法
                return ReadDeviceRandom(deviceName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取指定寄存器失败: {ex.Message}");
                return null;
            }
        }
        #endregion

        #endregion
        public string GetReturnCode()
        {
            string returnCode = txt_ReturnCode.Text;
            return returnCode;
        }

        #region 实现IPLCService接口的方法，解除代码耦合
        // 新增重载方法，支持只修改指定寄存器
        //允许只传入需要修改的寄存器（如SafeWritePlc(cam: 11)），其他寄存器保持当前值，避免误写。
        public void SafeWritePlc(short? cam = null, short? prt = null, short? scn = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<short?, short?, short?>(SafeWritePlc), cam, prt, scn);
                return;
            }
            // 读取当前值，未指定的参数用当前值填充
            var current = ReadPlc();
            short newCam = cam ?? current.Item1;
            short newPrt = prt ?? current.Item2;
            short newScn = scn ?? current.Item3;
            WritePlc(newCam, newPrt, newScn); // 调用原写入方法
        }

        // 暴露InvokeRequired属性（Form本身已实现）
        bool IPLCService.InvokeRequired => this.InvokeRequired;

        // 暴露Invoke方法（Form本身已实现）
        object IPLCService.Invoke(Delegate method) => this.Invoke(method);

        #endregion
    }
}