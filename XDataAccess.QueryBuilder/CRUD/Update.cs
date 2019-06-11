using System;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Compilers.Databases;

namespace XDataAccess.QueryBuilder.CRUD {
    public sealed class Update : ICrudOperation {
        private IResult updateResult;
        public ICompiler Compiler { get; private set; }
        internal Update (ICompiler compiler) {
            Compiler = compiler;
        }

        internal Update CompileUpdate<TEntity> (TEntity entity) where TEntity : class {
            updateResult = Compiler.CompileUpdate<TEntity> (entity);
            return this;
        }
        public IResult Compile () {
            return updateResult;
        }
        public IResult Where<TEntity> (Expression<Func<TEntity, bool>> exp) where TEntity : class {
            var result = updateResult as DbCompileResult;
            var whereResult = Compiler.CompileWhere<TEntity> (exp) as DbCompileResult;

            return result.Merge (whereResult, Compiler.Dialect);
        }
    }
}