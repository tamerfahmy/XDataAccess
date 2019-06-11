using System;
using System.Linq.Expressions;
using System.Text;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Databases;
using XDataAccess.QueryBuilder.Expressions.Interceptors.Databases;

namespace XDataAccess.QueryBuilder.Expressions.Interceptors.Databases
{
    public class BaseDbStringContainsInterceptor : BaseDbMethodCallInterceptor
    {
        public BaseDbStringContainsInterceptor(IDialect dialect) : base(dialect)
        {

        }

        public override bool CanIntercept(IExpressionResolver resolver, MethodCallExpression expr)
        {
            return base.CanIntercept(resolver, expr) &&
                   (DataType == resolver.DataType) &&
                   expr.Method.DeclaringType == typeof(string) &&
                   expr.Method.Name == "Contains";
        }

        public override void Intercept(IResult result, MethodCallExpression expr, Func<Expression, Expression> visit)
        {
            var dbResolveResult = result as DbResolveResult;

            var left = expr.Object;
            var right = expr.Arguments[0];

            dbResolveResult.Builder.Append(Dialect.StartGroup);
            visit(left);
            dbResolveResult.Builder.Append($" {Dialect.Like} {Dialect.Quote}{Dialect.Wildchar}{Dialect.Quote} {Dialect.AppendParameter} ");
            visit(right);
            dbResolveResult.Builder.Append($" {Dialect.AppendParameter} {Dialect.Quote}{Dialect.Wildchar}{Dialect.Quote}{Dialect.EndGroup}");
        }
    }
}