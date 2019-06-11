using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions.Databases
{
    public sealed class MySqlExpressionResolver : BaseDbExpressionResolver
    {
        public MySqlExpressionResolver(IDialect dialect) : base(dialect)
        {
            DataType = DataType.MySql;
        }
    }
}
