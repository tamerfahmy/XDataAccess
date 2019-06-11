using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions.Databases
{
    public sealed class OracleExpressionResolver : BaseDbExpressionResolver
    {
        public OracleExpressionResolver(IDialect dialect) : base(dialect)
        {
            DataType = DataType.Oracle;
        }
    }
}
