

using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Databases;

namespace XDataAccess.QueryBuilder.Expressions.Interceptors.Databases
{
    public class PostgresStringContainsInterceptor : BaseDbStringContainsInterceptor
    {
        public PostgresStringContainsInterceptor(IDialect dialect) : base(dialect)
        {
        }

        public override DataType? DataType { get => XDataAccess.QueryBuilder.DataType.PostgresSQL; }
    }
}