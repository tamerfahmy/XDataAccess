using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Interceptors;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder.Expressions
{
    public interface IExpressionResolver
    {
        DataType DataType { get; }
        EntityMetadata EntityMetadata { get; }
        IResult Result { get; }
        IDialect Dialect { get; }
        InterceptorFactory InterceptorFactory { get; }
        IResult Resolve<TEntity>(Expression expression) where TEntity : class;
    }
}
