using System;
using System.Linq.Expressions;

namespace XDataAccess.QueryBuilder.CrudOperation.Interfaces {
    public interface IUpdate : ICrudOperation {
        IResult CompileUpdate<TEntity> (TEntity entity) where TEntity : class;
        IResult CompileUpdate<TEntity> (TEntity entity, Expression<Func<TEntity, bool>> exp) where TEntity : class;
    }
}