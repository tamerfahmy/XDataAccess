using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Databases;

namespace XDataAccess.QueryBuilder.Expressions.Interceptors.Databases
{
    public abstract class BaseDbMethodCallInterceptor : IMethodCallInterceptor
    {
        public IDialect Dialect { get; private set; }

        public virtual DataType? DataType { get; }

        public BaseDbMethodCallInterceptor(IDialect dialect)
        {
            Dialect = dialect;
        }

        public virtual bool CanIntercept(IExpressionResolver resolver, MethodCallExpression expr)
        {
            return resolver.GetType().IsSubclassOf(typeof(BaseDbExpressionResolver));
        }

        public abstract void Intercept(IResult result, MethodCallExpression expr, Func<Expression, Expression> visit);
    }
}