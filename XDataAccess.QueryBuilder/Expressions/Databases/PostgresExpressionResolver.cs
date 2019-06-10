﻿using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions
{
    public sealed class PostgresExpressionResolver : BaseExpressionResolver
    {
        public PostgresExpressionResolver(IDialect dialect) : base(dialect)
        {

        }
    }
}