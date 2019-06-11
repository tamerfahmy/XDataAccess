using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions
{
    public interface IMethodCallInterceptor
    {
        DataType? DataType { get; }
        IDialect Dialect { get; }
        bool CanIntercept(IExpressionResolver resolver, MethodCallExpression expr);
        void Intercept(IResult result, MethodCallExpression expr, Func<Expression, Expression> visit);
    }
}