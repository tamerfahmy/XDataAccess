using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Compilers;

namespace XDataAccess.QueryBuilder.CRUD
{
    public sealed class Insert : ICrudOperation
    {
        public ICompiler Compiler { get; private set; }
        public Insert(ICompiler compiler)
        {
            Compiler = compiler;
        }
        public ICompileResult Compile<TEntity>(TEntity entity) where TEntity : class
        {
            return Compiler.CompilerInsert(entity);
        }
    }
}
