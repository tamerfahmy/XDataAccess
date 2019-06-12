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

        public IResult Insert(TEntity entity)
        {
            var insert = new Insert(_compiler);
            return insert.Compile(entity);
        }

        public IResult Delete()
        {
            var delete = new Delete(_compiler);
            return delete.Compile<TEntity>();
        }

        public IResult Delete(Expression<Func<TEntity, bool>> where)
        {
            var delete = new Delete(_compiler);
            return delete.Compile(where);
        }

        public IResult Update(TEntity entity)
        {
            var update = new Update(_compiler);
            return update.CompileUpdate(entity);
        }

        public IResult Update(TEntity entity, Expression<Func<TEntity, bool>> exp)
        {
            var update = new Update(_compiler);
            return update.CompileUpdate(entity, exp);
        }

        public IResult Select(Expression<Func<TEntity, bool>> exp)
        {
            var Select = new Select(_compiler);
            return Select.CompileSelect(exp);
        }

        public IResult Select()
        {
            var Select = new Select(_compiler);
            return Select.CompileSelect<TEntity>();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}