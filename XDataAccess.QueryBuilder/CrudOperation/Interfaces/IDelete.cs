using System;
using System.Linq.Expressions;

namespace XDataAccess.QueryBuilder.CrudOperation.Interfaces {
    public interface IDelete : ICrudOperation {
        IResult Compile<TEntity> () where TEntity : class;
        IResult Compile<TEntity> (Expression<Func<TEntity, bool>> exp) where TEntity : class;
    }
}