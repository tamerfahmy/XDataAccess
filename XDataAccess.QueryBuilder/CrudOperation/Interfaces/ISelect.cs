using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;

namespace XDataAccess.QueryBuilder.CrudOperation.Interfaces {
    public interface ISelect : ICrudOperation {
        IResult CompileSelect<TEntity> (Expression<Func<TEntity, bool>> exp) where TEntity : class;
        IResult CompileSelect<TEntity> () where TEntity : class;
    }
}