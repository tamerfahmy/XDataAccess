using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Interceptors.Databases;

namespace XDataAccess.QueryBuilder.Expressions.Interceptors
{
    public sealed class InterceptorFactory
    {
        private IDialect _dialect;
        private IExpressionResolver _resolver;
        private IList<IMethodCallInterceptor> _interceptors;

        public InterceptorFactory(IExpressionResolver resolver, IDialect dialect)
        {
            _resolver = resolver;
            _dialect = dialect;

            RegisterInterceptors(_dialect);
        }

        public IMethodCallInterceptor FindInterceptor(MethodCallExpression expr)
        {
            return _interceptors.FirstOrDefault(i => i.CanIntercept(_resolver, expr));
        }

        private void RegisterInterceptors(IDialect dialect)
        {
            _interceptors = new List<IMethodCallInterceptor>()
            {
                new MySqlStringContainsInterceptor(dialect),
                new SqlServerStringContainsInterceptor(dialect),
                new OracleStringContainsInterceptor(dialect),
                new PostgresStringContainsInterceptor(dialect),
            };
        }
    }
}