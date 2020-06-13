using System.IO;

namespace CourseworkTask
{
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warning,
        Exception,
        Fatal
    }

    public class ExceptionLoggerSingleton
    {
        private static readonly ExceptionLoggerSingleton LoggerReference = new ExceptionLoggerSingleton();
        private static readonly string LogFileName = "Exception Log.txt";
        private static readonly string CWD = Directory.GetCurrentDirectory();
        private static readonly string LogFilePath = Path.Combine(CWD, LogFileName);

        private ExceptionLoggerSingleton()
        {
        }

        public static ExceptionLoggerSingleton GetReference()
        {
            return LoggerReference;
        }

        public void MakeReport(LogLevel logLevel, string message)
        {

        }

    }
}
