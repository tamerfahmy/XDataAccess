using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers
{
    public interface ICompiler
    {
        IExpressionResolver Resolver { get; }

        IDialect Dialect { get; }

        IResult CompilerInsert<TEntity>(TEntity entity) where TEntity : class;

        IResult CompilerDelete<TEntity>() where TEntity : class;

        IResult CompilerDelete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class;
    }
}