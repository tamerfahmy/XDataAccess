using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XDataAccess.QueryBuilder
{
    internal static class Constants
    {
        internal static readonly Lazy<Dictionary<ExpressionType, string>> ExpressionToSQL = new Lazy<Dictionary<ExpressionType, string>>(() => new Dictionary<ExpressionType, string>() {
            { ExpressionType.OrElse, "OR" },
            { ExpressionType.Or, "|" },
            { ExpressionType.AndAlso, "AND" },
            { ExpressionType.And, "&" },
            { ExpressionType.GreaterThan, ">" },
            { ExpressionType.GreaterThanOrEqual, ">=" },
            { ExpressionType.LessThan, "<" },
            { ExpressionType.LessThanOrEqual, "<=" },
            { ExpressionType.NotEqual, "<>" },
            { ExpressionType.Add, "+" },
            { ExpressionType.Subtract, "-" },
            { ExpressionType.Multiply, "*" },
            { ExpressionType.Divide, "/" },
            { ExpressionType.Modulo, "%" },
            { ExpressionType.Equal, "=" },
        });

        internal static Lazy<List<string>> Operators = new Lazy<List<string>>(() => new List<string>()
        {
            "=", "<", ">", "<=", ">=", "<>", "!=", "<=>",
            "like", "not like",
            "ilike", "not ilike",
            "like binary", "not like binary",
            "rlike", "not rlike",
            "regexp", "not regexp",
            "similar to", "not similar to"
        });

        internal static Lazy<List<Type>> NumberTypes = new Lazy<List<Type>>(() => new List<Type>()
        {
            typeof(sbyte),
            typeof(sbyte?),
            typeof(short),
            typeof(short?),
            typeof(int),
            typeof(int?),
            typeof(long),
            typeof(long?),
            typeof(byte),
            typeof(byte?),
            typeof(ushort),
            typeof(ushort?),
            typeof(uint),
            typeof(uint?),
            typeof(ulong),
            typeof(ulong?),
            typeof(double),
            typeof(double?),
            typeof(float),
            typeof(float?),
            typeof(decimal),
            typeof(decimal?)
        });
    }
}