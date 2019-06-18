using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CrudOperation.Interfaces;

namespace XDataAccess.QueryBuilder.CrudOperation
{
    public sealed class Delete : IDelete
    {
        public ICompiler Compiler { get; private set; }

        public Delete(ICompiler compiler)
        {
            Compiler = compiler;
        }

        public IResult Compile<TEntity>() where TEntity : class
        {
            return Compiler.CompileDelete<TEntity>();
        }

        public IResult Compile<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            return Compiler.CompileDelete<TEntity>(exp);
        }
    }
}