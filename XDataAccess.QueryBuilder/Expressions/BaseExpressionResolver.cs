using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions
{
    public abstract class BaseExpressionResolver : ExpressionVisitor, IExpressionResolver
    {
        public IDialect Dialect { get; private set; }

        public BaseExpressionResolver(IDialect dialect)
        {
            Dialect = dialect;
        }

        public virtual string Resolve<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
