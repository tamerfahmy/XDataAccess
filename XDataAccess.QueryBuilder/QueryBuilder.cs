using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CRUD;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder {
    public sealed class QueryBuilder<TEntity> where TEntity : class {
        private EntityMetadata _entityMetadata;
        private ICompiler _compiler;

        public EntityMetadata EntityMetadata {
            get {
                if (_entityMetadata == null)
                    _entityMetadata = EntityMetadata.Parse<TEntity> ();

                return _entityMetadata;
            }
        }

        public QueryBuilder (ICompiler compiler) {
            _compiler = compiler;
        }

        public IResult Insert (TEntity entity) {
            var insert = new Insert (_compiler);
            return insert.Compile (entity);
        }

        public IResult Delete () {
            var delete = new Delete (_compiler);
            return delete.Compile<TEntity> ();
        }

        public IResult Delete (Expression<Func<TEntity, bool>> where) {
            var delete = new Delete (_compiler);
            return delete.Compile (where);
        }

        public Update Update (TEntity entity) {
            var update = new Update (_compiler);
            return update.CompileUpdate (entity);
        }

        public void Dispose () {
            throw new System.NotImplementedException ();
        }
    }
}