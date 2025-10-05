using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Data_Transceiver_Center
{
    public partial class Form3 : Form
    {
        // 通过POST发送给Mes的Json数据
        private MesPostRoot _MesPostRoot;

        // 提供公共属性，让外部可以读取数据
        public MesPostRoot MesPostRoot
        {
            get { return _MesPostRoot; }
            private set { _MesPostRoot = FormToJson(); }
        }

        public Form3()
        {
            InitializeComponent();
        }


        // 保存配置按钮
        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            // 选择文件夹路径
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            // 提示信息
            dialog.Description = "请选择json保存位置";
            string jsonPath = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                jsonPath = dialog.SelectedPath + "\\MesSettings.json";
            }

            _MesPostRoot =  FormToJson();

            try
            {
                SaveToJsonFile(_MesPostRoot, jsonPath);
                if (jsonPath != "")
                {
                    MessageBox.Show("已保存，文件位置：" + jsonPath);
                }
                else
                {
                    MessageBox.Show("未保存json配置");
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        // 加载配置按钮
        public void btnLoadJson_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();   // 选择文件
            dialog.Multiselect = false; // 是否可以选择多个 文件
            dialog.Title = "请选择MesSettings.json文件";
            dialog.Filter = "json文件(*.json)|*.json";
            string file = "";
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // 读取Json文件的路径
                    file = dialog.FileName;
                    // 读取JSON文件并转换为Json对象
                    _MesPostRoot = ReadFromJsonFile<MesPostRoot>(file);

                    JsonDataToForm();
                }
            }
            catch (Exception)
            { return; }
        }

        // 将JsonData的数据写入页面文本框
        private void JsonDataToForm()
        {
            txtBox_mesUrl.Text = _MesPostRoot.MesUrl;
            txtBox_stepId.Text = _MesPostRoot.MesData.input.stepId;
            txtBox_lineId.Text = _MesPostRoot.MesData.input.lineId;
            txtBox_eqpId.Text = _MesPostRoot.MesData.input.eqpId;
            //txtBox_panelId.Text = _MesPostRoot.MesData.input.panelId;
            txtBox_fixture.Text = _MesPostRoot.MesData.input.fixture;
        }

        // 将页面文本框中的数据，存入JsonData中
        public MesPostRoot FormToJson()
        {
            // 从页面的文本框保存Json数据
            MesInputJson mesInputData = new MesInputJson
            {
                stepId = txtBox_stepId.Text,
                lineId = txtBox_lineId.Text,
                eqpId = txtBox_eqpId.Text,
                panelId = txtBox_panelId.Text,
                fixture = txtBox_fixture.Text
            };

            MsePostData mesPostData = new MsePostData
            {
                input = mesInputData
            };


            MesPostRoot MesPostRoot = new MesPostRoot
            {
                MesUrl = txtBox_mesUrl.Text,
                MesData = mesPostData
            };

            return MesPostRoot;
        }

        // 保存配置
        public void SaveToJsonFile<T>(T data, string filePath)
        {
            try
            {
                // 序列化对象为JSON字符串（带格式化）
                string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

                // 写入文件
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"JSON数据已成功保存到: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存JSON文件时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 从JSON文件读取数据并反序列化为指定类型
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="filePath">JSON文件路径</param>
        /// <returns>反序列化后的对象</returns>
        public T ReadFromJsonFile<T>(string filePath)
        {
            try
            {
                // 检查文件是否存在
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"错误：文件不存在 - {filePath}");
                    return default(T);
                }

                // 读取文件内容
                string jsonContent = File.ReadAllText(filePath);

                // 反序列化JSON到对象
                T result = JsonConvert.DeserializeObject<T>(jsonContent);

                Console.WriteLine($"成功从 {filePath} 读取并解析JSON数据");
                return result;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON格式错误：{ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"文件读写错误：{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误：{ex.Message}");
            }

            return default(T);
        }


        // 提供公共方法：允许外部设置文本框内容
        // 用于接收从MainForm同步过来的数据
        public void UpdateIDText(string text)
        {
            // 线程安全检查：确保在UI线程操作控件
            if (txtBox_panelId.InvokeRequired)
            {
                // 如果当前线程不是UI线程，通过Invoke切换到UI线程
                txtBox_panelId.Invoke(new Action<string>(UpdateIDText), text);
            }
            else
            {
                // 仅在文本不同时更新，减少不必要的UI刷新
                if (txtBox_panelId.Text != text)
                {
                    txtBox_panelId.Text = text;

                    // 保持光标在文本末尾，提升用户体验
                    txtBox_panelId.SelectionStart = txtBox_panelId.TextLength;
                    txtBox_panelId.ScrollToCaret();
                }
            }
            FormToJson();
        }

        // 测试按钮
        private void btn_apiTest_Click(object sender, EventArgs e)
        {
            // 将页面数据存入Json中
            _MesPostRoot =  FormToJson();

            string JsonUrl = txtBox_mesUrl.Text;
            // 取出Json中的input字段内容
            string JsonData = JsonConvert.SerializeObject(_MesPostRoot.MesData);

            // 页面显示
            txtBox_postData.Text = "Post Data:\r\n" + JsonData;
            
            // 使用多线程处理http事件
            Task t1 = new Task(()=>
            { 
                string getJson = HttpUitls.PostJson(JsonUrl, JsonData);

                // 跨线程修改UI
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    txtBox_responseData.Text = "Response Data:\r\n" + getJson;
                });
                this.BeginInvoke(mi);
            });
            t1.Start();

        }

        // 存储panelID
        private void txtBox_panelId_TextChanged(object sender, EventArgs e)
        {
            this.MesPostRoot = FormToJson();
        }
    }

}
