using System;

namespace XDataAccess.QueryBuilder
{
    public enum DataType
    {
        MySql,
        SqlServer,
        PostgresSQL,
        Oracle,
        Sqlite
    }

    public enum CrudOperationType
    {
        Database,
        CDS
    }
}