using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions.Databases
{
    public sealed class PostgresExpressionResolver : BaseDbExpressionResolver
    {
        public PostgresExpressionResolver(IDialect dialect) : base(dialect)
        {
            DataType = DataType.PostgresSQL;
        }
    }
}
