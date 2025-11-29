using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Data_Transceiver_Center
{
    class IniFile
    {
        private readonly string _filePath;
        private readonly string _defaultSection;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string @default, StringBuilder retVal, int size, string filePath);

        public IniFile(string iniPath = null)
        {
            _defaultSection = Assembly.GetExecutingAssembly().GetName().Name;
            _filePath = iniPath != null
                ? new FileInfo(iniPath).FullName
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{_defaultSection}.ini");
        }

        /// <summary>
        /// 读取字符串值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="section">节名，默认使用程序集名称</param>
        /// <returns>读取到的字符串</returns>
        public string ReadString(string key, string section = null)
        {
            var retVal = new StringBuilder(2048); // 扩大缓冲区大小
            GetPrivateProfileString(section ?? _defaultSection, key, "", retVal, retVal.Capacity, _filePath);
            return retVal.ToString().Trim();
        }

        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="section">节名，默认使用程序集名称</param>
        /// <param name="defaultValue">读取失败时的默认值</param>
        /// <returns>解析后的布尔值</returns>
        public bool ReadBoolean(string key, string section = null, bool defaultValue = false)
        {
            string value = ReadString(key, section);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            // 支持多种常见的布尔值表示形式
            return value.Equals("1", StringComparison.OrdinalIgnoreCase) ||
                   value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                   value.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 写入值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="section">节名，默认使用程序集名称</param>
        public void Write(string key, string value, string section = null)
        {
            WritePrivateProfileString(section ?? _defaultSection, key, value, _filePath);
        }

        /// <summary>
        /// 写入布尔值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">布尔值</param>
        /// <param name="section">节名，默认使用程序集名称</param>
        public void WriteBoolean(string key, bool value, string section = null)
        {
            Write(key, value ? "1" : "0", section);
        }

        /// <summary>
        /// 删除指定键
        /// </summary>
        public void DeleteKey(string key, string section = null)
        {
            Write(key, null, section);
        }

        /// <summary>
        /// 删除指定节
        /// </summary>
        public void DeleteSection(string section = null)
        {
            Write(null, null, section ?? _defaultSection);
        }

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        public bool KeyExists(string key, string section = null)
        {
            return !string.IsNullOrWhiteSpace(ReadString(key, section));
        }
    }
}