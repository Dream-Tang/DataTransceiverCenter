using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// INI配置文件助手类（单例模式）
    /// 负责INI文件的读取、写入及管理操作
    /// </summary>
    public class IniHelper
    {
        // 单例模式实现
        private static readonly Lazy<IniHelper> _instance = new Lazy<IniHelper>(() => new IniHelper());
        public static IniHelper Instance => _instance.Value;

        // 默认节名（使用程序集名称）
        private readonly string _defaultSection;

        // 私有构造函数
        private IniHelper()
        {
            _defaultSection = Assembly.GetExecutingAssembly().GetName().Name;
        }

        // 导入kernel32.dll中的INI操作函数
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string @default,
            StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取字符串值
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="key">键名</param>
        /// <param name="section">节名（默认使用程序集名称）</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>读取到的字符串</returns>
        public string ReadString(string filePath, string key, string section = null, string defaultValue = "")
        {
            ValidateFilePath(filePath);

            var retVal = new StringBuilder(2048);
            GetPrivateProfileString(section ?? _defaultSection, key, defaultValue,
                retVal, retVal.Capacity, filePath);
            return retVal.ToString().Trim();
        }

        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="key">键名</param>
        /// <param name="section">节名（默认使用程序集名称）</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>解析后的布尔值</returns>
        public bool ReadBoolean(string filePath, string key, string section = null, bool defaultValue = false)
        {
            string value = ReadString(filePath, key, section);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            // 支持多种常见的布尔值表示形式
            return value.Equals("1", StringComparison.OrdinalIgnoreCase) ||
                   value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                   value.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 写入字符串值
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        public void WriteString(string filePath, string section, string key, string value)
        {
            ValidateFilePath(filePath);
            EnsureDirectoryExists(filePath);

            WritePrivateProfileString(section ?? _defaultSection, key, value, filePath);
        }

        /// <summary>
        /// 写入布尔值（存储为"1"或"0"）
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="value">布尔值</param>
        public void WriteBoolean(string filePath, string section, string key, bool value)
        {
            WriteString(filePath, section, key, value ? "1" : "0");
        }

        /// <summary>
        /// 删除指定键
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="key">键名</param>
        /// <param name="section">节名</param>
        public void DeleteKey(string filePath, string key, string section = null)
        {
            WriteString(filePath, section, key, null);
        }

        /// <summary>
        /// 删除指定节
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section">节名（默认使用程序集名称）</param>
        public void DeleteSection(string filePath, string section = null)
        {
            WritePrivateProfileString(section ?? _defaultSection, null, null, filePath);
        }

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="key">键名</param>
        /// <param name="section">节名</param>
        /// <returns>是否存在</returns>
        public bool KeyExists(string filePath, string key, string section = null)
        {
            return !string.IsNullOrWhiteSpace(ReadString(filePath, key, section));
        }

        /// <summary>
        /// 验证文件路径有效性
        /// </summary>
        private void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "INI文件路径不能为空");
        }

        /// <summary>
        /// 确保文件所在目录存在
        /// </summary>
        private void EnsureDirectoryExists(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}