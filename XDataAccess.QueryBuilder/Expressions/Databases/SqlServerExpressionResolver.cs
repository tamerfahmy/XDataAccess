using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions
{
    public sealed class SqlServerExpressionResolver : BaseExpressionResolver
    {
        public SqlServerExpressionResolver(IDialect dialect) : base(dialect)
        {

        }
    }
}
