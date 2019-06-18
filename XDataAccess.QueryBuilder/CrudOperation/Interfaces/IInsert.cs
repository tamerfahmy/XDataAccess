using System;

namespace XDataAccess.QueryBuilder.CrudOperation.Interfaces {
    public interface IInsert : ICrudOperation {
        IResult Compile<TEntity> (TEntity entity) where TEntity : class;
    }
}