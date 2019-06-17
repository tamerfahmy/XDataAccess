using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.CrudOperation;
using XDataAccess.QueryBuilder.Logger;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder
{
    public sealed class QueryBuilder<TEntity> where TEntity : class
    {
        private EntityMetadata _entityMetadata;
        private ICompiler _compiler;

        public ILogger Logger { get; set; }

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

        public QueryBuilder(ICompiler compiler, ILogger logger)
        {
            _compiler = compiler;
            Logger = logger;
        }

        public IResult Insert(TEntity entity)
        {
            var insert = new Insert(_compiler);
            var result = insert.Compile(entity);
            LogResult(result);
            return result;
        }

        public IResult Delete()
        {
            var delete = new Delete(_compiler);
            var result = delete.Compile<TEntity>();
            LogResult(result);
            return result;
        }

        public IResult Delete(Expression<Func<TEntity, bool>> where)
        {
            var delete = new Delete(_compiler);
            var result = delete.Compile(where);
            LogResult(result);
            return result;
        }

        public IResult Update(TEntity entity)
        {
            var update = new Update(_compiler);
            var result = update.CompileUpdate(entity);
            LogResult(result);
            return result;
        }

        public IResult Update(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            var update = new Update(_compiler);
            var result = update.CompileUpdate(entity, where);
            LogResult(result);
            return result;
        }

        public IResult Select(Expression<Func<TEntity, bool>> exp)
        {
            var Select = new Select(_compiler);
            var result = Select.CompileSelect(exp);
            LogResult(result);
            return result;
        }

        public IResult Select()
        {
            var Select = new Select(_compiler);
            var result = Select.CompileSelect<TEntity>();
            LogResult(result);
            return result;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        private void LogResult(IResult result)
        {
            if (Logger != null && result != null)
                Logger.Debug(result.ToString());
        }
    }
}