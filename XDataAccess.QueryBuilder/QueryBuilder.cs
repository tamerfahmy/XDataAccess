using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CRUD;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder
{
    public sealed class QueryBuilder<TEntity> where TEntity : class
    {
        private EntityMetadata _entityMetadata;
        private ICompiler _compiler;

        public EntityMetadata EntityMetadata
        {
            get
            {
                if (_entityMetadata == null)
                    _entityMetadata = EntityMetadata.Parse<TEntity>();

                return _entityMetadata;
            }
        }

        public QueryBuilder(ICompiler compiler)
        {
            _compiler = compiler;
        }

        public Insert Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TEntity, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}