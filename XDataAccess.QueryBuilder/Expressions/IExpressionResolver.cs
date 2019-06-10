using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions
{
    public interface IExpressionResolver
    {
        IDialect Dialect { get;  }
        string Resolve<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    }
}
