using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CrudOperation.Interfaces;

namespace XDataAccess.QueryBuilder.CrudOperation
{
    public sealed class Update : IUpdate
    {
        public ICompiler Compiler { get; private set; }
        public Update(ICompiler compiler)
        {
            Compiler = compiler;
        }

        public IResult CompileUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            return Compiler.CompileUpdate<TEntity>(entity);
        }

        public IResult CompileUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            return Compiler.CompileUpdate<TEntity>(entity, exp);
        }
    }
}