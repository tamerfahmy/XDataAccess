using System;

namespace XDataAccess.QueryBuilder.Logger
{
    public class ConsoleLogger : ILogger
    {
        private LogLevel _logLevel;
        public ConsoleLogger(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public void Debug(string msg)
        {
            if (CanLog(LogLevel.Debug))
                Console.WriteLine(msg);
        }

        public void Error(string msg)
        {
            if (CanLog(LogLevel.Error))
                Console.WriteLine(msg);
        }

        public void Error(Exception ex)
        {
            if (CanLog(LogLevel.Error))
                Console.WriteLine(ex.Message);
        }

        public void Info(string msg)
        {
            if (CanLog(LogLevel.Info))
                Console.WriteLine(msg);
        }

        public void Warning(string msg)
        {
            if (CanLog(LogLevel.Warning))
                Console.WriteLine(msg);
        }

        public void Dispose()
        {

        }

        private bool CanLog(LogLevel logLevel)
        {
            return _logLevel.HasFlag(logLevel);
        }
    }
}
