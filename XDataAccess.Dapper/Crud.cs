using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Dapper;
using XDataAccess.QueryBuilder;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Logger;

namespace XDataAccess.Dapper
{
    public class Crud : IDisposable
    {
        private Func<IDbConnection> _connectionCallback;
        public ILogger Logger { get; private set; }
        public ICompiler Compiler { get; private set; }
        public Crud(ICompiler compiler, ILogger logger, Func<IDbConnection> connectionCallback) : this(compiler, connectionCallback)
        {
            Logger = logger;
        }

        public Crud(ICompiler compiler, Func<IDbConnection> connectionCallback)
        {
            Compiler = compiler;
            _connectionCallback = connectionCallback;
        }

        public IEnumerable<TEntity> Select<TEntity>(IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            AddDapperCustomAttributesMapper<TEntity>();

            var queryBuilder = new QueryBuilder<TEntity>(Compiler, Logger);
            var query = queryBuilder.Select() as DbCompileResult;

            return _connectionCallback().Query<TEntity>(
                sql: query.SqlQuery,
                param: query.QueryParameters,
                transaction: transaction,
                commandTimeout: commandTimeout);
        }

        public IEnumerable<TEntity> Select<TEntity>(Expression<Func<TEntity, bool>> where, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            AddDapperCustomAttributesMapper<TEntity>();

            var queryBuilder = new QueryBuilder<TEntity>(Compiler, Logger);
            var query = queryBuilder.Select(where) as DbCompileResult;


            return _connectionCallback().Query<TEntity>(
                sql: query.SqlQuery,
                param: query.QueryParameters,
                transaction: transaction,
                commandTimeout: commandTimeout);
        }

        public int Insert<TEntity>(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(Compiler, Logger);
            var query = queryBuilder.Insert(entity) as DbCompileResult;

            return _connectionCallback().Execute(
                sql: query.SqlQuery,
                param: query.QueryParameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                commandType: CommandType.Text);
        }

        public int Update<TEntity>(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(Compiler, Logger);
            var query = queryBuilder.Update(entity) as DbCompileResult;

            return _connectionCallback().Execute(
                sql: query.SqlQuery,
                param: query.QueryParameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                commandType: CommandType.Text);
        }

        public int Update<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(Compiler, Logger);
            var query = queryBuilder.Update(entity, where) as DbCompileResult;

            return _connectionCallback().Execute(
                sql: query.SqlQuery,
                param: query.QueryParameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                commandType: CommandType.Text);
        }

        public int Delete<TEntity>(Expression<Func<TEntity, bool>> where, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(Compiler, Logger);
            var query = queryBuilder.Delete(where) as DbCompileResult;

            return _connectionCallback().Execute(
                sql: query.SqlQuery,
                param: query.QueryParameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                commandType: CommandType.Text);
        }

        private void AddDapperCustomAttributesMapper<TEntity>() where TEntity : class
        {
            SqlMapper.SetTypeMap(typeof(TEntity), new ColumnAttributeTypeMapper<TEntity>());
        }

        public void Dispose()
        {
            Logger.Dispose();
        }
    }
}