using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 打印助手类（单例模式）
    /// 负责处理ZPL模板加载、打印指令生成和打印机通信
    /// </summary>
    public class PrintingHelper
    {
        // 单例模式实现
        private static readonly Lazy<PrintingHelper> _instance = new Lazy<PrintingHelper>(() => new PrintingHelper());
        public static PrintingHelper Instance => _instance.Value;

        // 私有构造函数禁止外部实例化
        private PrintingHelper() { }

        /// <summary>
        /// 异步执行打印流程
        /// </summary>
        /// <param name="printCode">打印内容</param>
        /// <param name="zplTemplatePath">ZPL模板路径</param>
        /// <param name="printerPath">打印机路径</param>
        /// <returns>打印结果信息</returns>
        public async Task<(bool Success, string Message)> ExecutePrintAsync(string printCode, string zplTemplatePath, string printerPath)
        {
            try
            {
                // 输入参数校验
                if (string.IsNullOrEmpty(printCode))
                    return (false, "打印内容不能为空");

                if (string.IsNullOrEmpty(zplTemplatePath))
                    return (false, "ZPL模板路径未设置");

                if (string.IsNullOrEmpty(printerPath))
                    return (false, "打印机路径未设置");

                // 1. 加载ZPL模板
                var loadResult = await LoadZplTemplateAsync(zplTemplatePath);
                if (!loadResult.Success)
                    return (false, loadResult.Message);

                // 2. 生成ZPL指令
                var zplContent = ReplaceZplVariable(loadResult.Data, printCode);
                if (string.IsNullOrEmpty(zplContent))
                    return (false, "生成ZPL指令指令失败");

                // 3. 保存ZPL文件
                string tempFilePath = Path.Combine(Path.GetTempPath(), "print_temp.zpl");
                var saveResult = await SaveZplFileAsync(tempFilePath, zplContent);
                if (!saveResult.Success)
                    return (false, saveResult.Message);

                // 4. 发送到打印机
                var printResult = await SendToPrinterAsync(tempFilePath, printerPath);
                if (!printResult.Success)
                    return (false, printResult.Message);

                return (true, $"打印成功: {printCode}");
            }
            catch (Exception ex)
            {
                return (false, $"打印过程异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 异步加载ZPL模板
        /// </summary>
        private async Task<(bool Success, string Data, string Message)> LoadZplTemplateAsync(string templatePath)
        {
            try
            {
                if (!File.Exists(templatePath))
                    return (false, null, $"模板文件不存在: {templatePath}");

                string content = await Task.Run(() => File.ReadAllText(templatePath));

                // 验证ZPL模板合法性
                if (!content.Contains("^FD") || !content.Contains("^XZ"))
                    return (false, null, "ZPL模板格式错误，缺少^FD或^XZ指令");

                return (true, content, "模板加载成功");
            }
            catch (Exception ex)
            {
                return (false, null, $"加载模板失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 替换ZPL模板中的变量
        /// </summary>
        private string ReplaceZplVariable(string zplTemplate, string printCode)
        {
            try
            {
                int fdIndex = zplTemplate.IndexOf("^FD") + 3;
                int fsIndex = zplTemplate.IndexOf("^FS");

                if (fdIndex <= 3 || fsIndex <= 0 || fdIndex >= fsIndex)
                    return null;

                // 替换^FD和^FS之间的内容
                var zplBuilder = new StringBuilder(zplTemplate);
                zplBuilder.Remove(fdIndex, fsIndex - fdIndex);
                zplBuilder.Insert(fdIndex, printCode);

                return zplBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 异步保存ZPL文件到临时目录
        /// </summary>
        private async Task<(bool Success, string Message)> SaveZplFileAsync(string filePath, string content)
        {
            try
            {
                await Task.Run(() => File.WriteAllText(filePath, content));
                return (true, $"ZPL文件已保存: {filePath}");
            }
            catch (Exception ex)
            {
                return (false, $"保存ZPL文件失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 异步发送文件到打印机
        /// </summary>
        private async Task<(bool Success, string Message)> SendToPrinterAsync(string filePath, string printerPath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (!File.Exists(filePath))
                        return (false, $"临时文件不存在: {filePath}");

                    File.Copy(filePath, printerPath, true);
                    return (true, $"已发送到打印机: {printerPath}");
                }
                catch (Exception ex)
                {
                    return (false, $"发送到打印机失败: {ex.Message}");
                }
            });
        }
    }
}