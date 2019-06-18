using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CrudOperation.Interfaces;

namespace XDataAccess.QueryBuilder.CrudOperation {
    public sealed class Insert : IInsert {
        public ICompiler Compiler { get; private set; }
        public Insert (ICompiler compiler) {
            Compiler = compiler;
        }
        public IResult Compile<TEntity> (TEntity entity) where TEntity : class {
            return Compiler.CompileInsert (entity);
        }
    }
}