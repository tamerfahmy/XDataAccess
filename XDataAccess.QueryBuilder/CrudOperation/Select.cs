using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CrudOperation.Interfaces;

namespace XDataAccess.QueryBuilder.CrudOperation
{
    public sealed class Select : ISelect
    {
        public ICompiler Compiler { get; private set; }
        public Select(ICompiler compiler)
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