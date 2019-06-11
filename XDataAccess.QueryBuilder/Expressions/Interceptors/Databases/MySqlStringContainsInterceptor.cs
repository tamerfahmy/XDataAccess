using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Databases;

namespace XDataAccess.QueryBuilder.Expressions.Interceptors.Databases
{
    public class MySqlStringContainsInterceptor : BaseDbStringContainsInterceptor
    {
        public MySqlStringContainsInterceptor(IDialect dialect) : base(dialect)
        {
        }

        public override DataType? DataType { get => XDataAccess.QueryBuilder.DataType.MySql; }

        public override void Intercept(IResult result, MethodCallExpression expr, Func<Expression, Expression> visit)
        {
            var dbResolveResult = result as DbResolveResult;

            var left = expr.Object;
            var right = expr.Arguments[0];

            dbResolveResult.Builder.Append(Dialect.StartGroup);
            visit(left);
            dbResolveResult.Builder.Append($" {Dialect.Like} CONCAT({Dialect.Quote}{Dialect.Wildchar}{Dialect.Quote} {Dialect.AppendParameter} ");
            visit(right);
            dbResolveResult.Builder.Append($" {Dialect.AppendParameter} {Dialect.Quote}{Dialect.Wildchar}{Dialect.Quote}){Dialect.EndGroup}");
        }
    }
}