using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Compilers.Databases;

namespace XDataAccess.QueryBuilder.CRUD
{
    public sealed class Update : ICrudOperation
    {
        public ICompiler Compiler { get; private set; }
        internal Update(ICompiler compiler)
        {
            Compiler = compiler;
        }

        internal IResult CompileUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            return Compiler.CompileUpdate<TEntity>(entity);
        }

        public IResult CompileUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            return Compiler.CompileUpdate<TEntity>(entity, exp);
        }
    }
}