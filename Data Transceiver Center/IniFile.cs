using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Data_Transceiver_Center
{
    class IniFile
    {
        //在与可执行文件相同的目录中创建或加载INI文件
        //命名为EXE.ini(其中EXE是可执行文件的名称)
            //var MyIni = new IniFile();
        //或在当前目录中指定一个配置文件的名称
            //var MyIni = new IniFile("Settings.ini");
        // 或者在指定的目录中指定一个配置文件的名称
            //var MyIni = new IniFile(@"C:\Settings.ini");

        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;  // 获取包含当前执行的代码的程序集 名字
        /*
         * DllImport调用系统的kernel32.dll库来进行ini文件的读写操作
         * C#命名空间中没有直接读写INI的类，虽然C#中没有，但是在"kernel32.dll"这个文件中有Win32的API函数–WritePrivateProfileString()和GetPrivateProfileString()
         * 通过 static extern 声明 kernel32中的函数名。
         */
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
        [DllImport("kernel32",CharSet=CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)   // 类构造方法，实例生成时运行
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;  // ?? 称为null合并运算符。如果此运算符左边的操作数不为null，则此运算符返回左操作数；否则返回右操作数
        }

        /*
         * 读Section中Key的值出来，返回string
         */
        public string Read(string Key,string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section??EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        /*
        * 向ini文件中写入内容
        */
        public void Write(string Key, string Vaule, string Section = null)
        {
            WritePrivateProfileString(Section??EXE, Key, Vaule, Path);
        }

        /*
      * 删除一个Key项
      */
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section??EXE);
        }

        /*
      * 删除一个Section节
      */
        public void DeleteSection(string Section =null)
        {
            Write(null, null, Section ?? EXE);
        }

        /*
      * 查看是否存在Key
      */
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

    }
}
