using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions
{
    public sealed class MySqlExpressionResolver : BaseExpressionResolver
    {
        public MySqlExpressionResolver(IDialect dialect) : base(dialect)
        {

        }
    }
}
