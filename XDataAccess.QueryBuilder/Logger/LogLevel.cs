using System;

namespace XDataAccess.QueryBuilder.Logger
{
    [Flags]
    public enum LogLevel
    {
        Info = 1,
        Debug = 2,
        Warning = 4,
        Error = 8
    }
}