using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.CRUD;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers
{
    public interface ICompiler
    {
        IExpressionResolver Resolver { get; }

        IDialect Dialect { get; }

        IResult CompileInsert<TEntity>(TEntity entity) where TEntity : class;

        IResult CompileDelete<TEntity>() where TEntity : class;

        IResult CompileDelete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class;

        IResult CompileSelect<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class;

        IResult CompileSelect<TEntity>() where TEntity : class;

        IResult CompileUpdate<TEntity>(TEntity entity) where TEntity : class;

        IResult CompileUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> exp) where TEntity : class;
    }
}