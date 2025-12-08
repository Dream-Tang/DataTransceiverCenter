using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 日志助手类（单例模式 + 线程安全）
    /// 负责统一处理日志输出、文件写入和日志切割
    /// </summary>
    public class LogHelper
    {
        // 单例模式
        private static readonly Lazy<LogHelper> _instance = new Lazy<LogHelper>(() => new LogHelper());
        public static LogHelper Instance => _instance.Value;

        // 日志配置参数
        private long _maxLogSize = 5 * 1024 * 1024; // 默认5MB
        private int _maxHistoryLogs = 10; // 默认保留10个历史日志
        private string _logFilePath = "AutoRun.log"; // 默认日志路径

        // 关键：添加文件操作锁（保证同一时间只有一个线程操作日志文件）
        private readonly object _fileLock = new object();

        // 私有构造函数（初始化日志路径）
        private LogHelper()
        {
            // 可从配置文件加载日志参数（预留扩展）
        }

        /// <summary>
        /// 设置日志配置（允许外部调整参数）
        /// </summary>
        public void SetLogConfig(string logFilePath = null, long? maxLogSize = null, int? maxHistoryLogs = null)
        {
            lock (_fileLock) // 配置修改也加锁，避免并发冲突
            {
                if (!string.IsNullOrEmpty(logFilePath))
                    _logFilePath = logFilePath;

                if (maxLogSize.HasValue)
                    _maxLogSize = maxLogSize.Value;

                if (maxHistoryLogs.HasValue)
                    _maxHistoryLogs = maxHistoryLogs.Value;
            }
        }
        /// <summary>
        /// 写入日志（对外接口）
        /// </summary>
        public void Log(string module, string level, string message)
        {
            string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{module}] [{level}] {message}";

            // 控制台输出
            Console.WriteLine(log);

            // 异步写入文件（不阻塞主线程）
            _ = WriteLogToFileAsync(log);
        }

        /// <summary>
        /// 异步写入日志（内部核心方法，加锁保证线程安全）
        /// </summary>
        private async Task WriteLogToFileAsync(string logContent)
        {
            // 异步等待 + 同步锁：既保证线程安全，又不阻塞线程池
            await Task.Run(() =>
            {
                lock (_fileLock) // 核心：同一时间只有一个线程能执行文件操作
                {
                    try
                    {
                        // 1. 先检查日志大小，切割完成后再写入（原子操作）
                        CheckAndSplitLogFile();

                        // 2. 写入日志（同步写入，已加锁，无并发冲突）
                        File.AppendAllText(_logFilePath, logContent + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"日志写入失败: {ex.Message}");
                    }
                }
            });
        }

        /// <summary>
        /// 检查日志文件大小，超过限制则切割
        /// </summary>
        private void CheckAndSplitLogFile()
        {
            // 若文件不存在，直接返回（后续AppendAllText会自动创建）
            if (!File.Exists(_logFilePath))
                return;

            FileInfo logFile = new FileInfo(_logFilePath);

            // 若日志文件超过最大限制，进行切割
            if (logFile.Length > _maxLogSize)
            {
                try
                {
                    // 生成带时间戳的历史日志文件名
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"); // 加毫秒，避免重名
                    string historyLogPath = $"{Path.GetFileNameWithoutExtension(_logFilePath)}_{timestamp}.log";

                    // 移动当前日志到历史日志
                    File.Move(_logFilePath, historyLogPath);

                    // 清理过期历史日志
                    CleanupHistoryLogs();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"日志切割失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 清理过期日志（已加锁）
        /// </summary>
        private void CleanupHistoryLogs()
        {
            string logDir = Path.GetDirectoryName(_logFilePath) ?? AppDomain.CurrentDomain.BaseDirectory;
            string logPrefix = Path.GetFileNameWithoutExtension(_logFilePath);

            try
            {
                var historyLogs = Directory.GetFiles(logDir, $"{logPrefix}_*.log")
                    .OrderByDescending(f => File.GetCreationTime(f))
                    .ToList();

                if (historyLogs.Count > _maxHistoryLogs)
                {
                    for (int i = _maxHistoryLogs; i < historyLogs.Count; i++)
                    {
                        // 先检查文件是否存在，再删除
                        if (File.Exists(historyLogs[i]))
                        {
                            File.Delete(historyLogs[i]);
                            Console.WriteLine($"已删除过期日志: {historyLogs[i]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 调试阶段显示错误信息
                System.Windows.Forms.MessageBox.Show($"日志写入失败: {ex.Message}\n路径: {_logFilePath}");
                Console.WriteLine($"日志写入失败: {ex.Message}");
                Console.WriteLine($"清理过期日志失败: {ex.Message}");
            }
        }
    }
}