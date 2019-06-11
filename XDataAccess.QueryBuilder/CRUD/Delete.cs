using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;

namespace XDataAccess.QueryBuilder.CRUD
{
    public class Delete : ICrudOperation
    {
        public ICompiler Compiler { get; private set; }
        public Delete(ICompiler compiler)
        {
            Compiler = compiler;
        }
        public IResult Compile<TEntity>() where TEntity : class
        {
            return Compiler.CompilerDelete<TEntity>();
        }

        public IResult Compile<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            return Compiler.CompilerDelete<TEntity>(exp);
        }
    }
}