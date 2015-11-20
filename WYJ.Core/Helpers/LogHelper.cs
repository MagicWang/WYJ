using System.Diagnostics;

namespace WYJ.Windows
{
    /// <summary>
    /// 日志帮助器
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 配置文件中名为LogLevel的跟踪开关
        /// </summary>
        public static TraceSwitch DefaultSwitch = new TraceSwitch("LogLevel", "");
        /// <summary>
        /// 刷新输出缓冲区，并使放入缓冲区中的数据写入 System.Diagnostics.Trace.Listeners。
        /// </summary>
        public static void Flush()
        {
            System.Diagnostics.Trace.Flush();
        }
        /// <summary>
        /// 根据跟踪开关和消息级别决定是否向监听器写入消息。
        /// </summary>
        /// <param name="ts">跟踪开关</param>
        /// <param name="level">消息级别</param>
        /// <param name="message">消息</param>
        public static void Trace(TraceSwitch ts, TraceLevel level, string message)
        {
            switch (level)
            {
                case TraceLevel.Error:
                    TraceError(ts, message);
                    break;
                case TraceLevel.Info:
                    TraceInformation(ts, message);
                    break;
                case TraceLevel.Off:
                    break;
                case TraceLevel.Verbose:
                    TraceLine(ts, message);
                    break;
                case TraceLevel.Warning:
                    TraceWarning(ts, message);
                    break;
                default:
                    break;
            }
        }
        public static void Trace(TraceSwitch ts, TraceLevel level, string format, params object[] args)
        {
            Trace(ts, level, string.Format(format, args));
        }
        /// <summary>
        /// 根据跟踪开关决定是否向监听器写入错误消息。
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="message"></param>
        public static void TraceError(TraceSwitch ts, string message)
        {
            if (ts.TraceError)
                System.Diagnostics.Trace.TraceError(message);
        }
        public static void TraceError(TraceSwitch ts, string format, params object[] args)
        {
            TraceError(ts, string.Format(format, args));
        }
        /// <summary>
        /// 根据跟踪开关决定是否向监听器写入警告消息。
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="message"></param>
        public static void TraceWarning(TraceSwitch ts, string message)
        {
            if (ts.TraceWarning)
                System.Diagnostics.Trace.TraceWarning(message);
        }
        public static void TraceWarning(TraceSwitch ts, string format, params object[] args)
        {
            TraceWarning(ts, string.Format(format, args));
        }
        /// <summary>
        /// 根据跟踪开关决定是否向监听器写入信息性消息。
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="message"></param>
        public static void TraceInformation(TraceSwitch ts, string message)
        {
            if (ts.TraceInfo)
                System.Diagnostics.Trace.TraceInformation(message);
        }
        public static void TraceInformation(TraceSwitch ts, string format, params object[] args)
        {
            TraceInformation(ts, string.Format(format, args));
        }
        /// <summary>
        /// 根据跟踪开关决定是否向监听器写入消息。
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="message"></param>
        public static void TraceLine(TraceSwitch ts, string message)
        {
            System.Diagnostics.Trace.WriteLineIf(ts.TraceVerbose, message);
        }
        public static void TraceLine(TraceSwitch ts, string format, params object[] args)
        {
            TraceLine(ts, string.Format(format, args));
        }
    }
}
