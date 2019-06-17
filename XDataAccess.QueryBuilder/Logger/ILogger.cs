using System;

namespace XDataAccess.QueryBuilder.Logger
{
    public interface ILogger : IDisposable
    {

        void Info(string msg);
        void Debug(string msg);
        void Warning(string msg);
        void Error(string msg);
        void Error(Exception ex);
    }
}
