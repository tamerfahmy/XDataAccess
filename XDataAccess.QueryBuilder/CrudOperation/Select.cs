using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;

namespace XDataAccess.QueryBuilder.CrudOperation
{
    public sealed class Select : ICrudOperation
    {
        public ICompiler Compiler { get; private set; }
        internal Select(ICompiler compiler)
        {
            Compiler = compiler;
        }

        public IResult CompileSelect<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            return Compiler.CompileSelect<TEntity>(exp);
        }

        public IResult CompileSelect<TEntity>() where TEntity : class
        {
            return Compiler.CompileSelect<TEntity>();
        }
    }
}