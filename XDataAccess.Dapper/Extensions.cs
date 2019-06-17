using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using XDataAccess.QueryBuilder;
using XDataAccess.QueryBuilder.Attributes;
using XDataAccess.QueryBuilder.Compilers;
using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Logger;

namespace XDataAccess.Dapper
{
    public static class Extensions
    {
        public static ILogger Logger { get; private set; }

        public static void SetLogger(this IDbConnection conneciton, ILogger logger)
        {
            Logger = logger;
        }

        public static IEnumerable<TEntity> Select<TEntity>(this IDbConnection conneciton, ICompiler compiler, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            AddDapperCustomAttributesMapper<TEntity>();

            var queryBuilder = new QueryBuilder<TEntity>(compiler, Logger);
            var query = queryBuilder.Select() as DbCompileResult;
            return conneciton.Query<TEntity>(query.SqlQuery, transaction: transaction, commandTimeout: commandTimeout);
        }

        public static IEnumerable<TEntity> Select<TEntity>(this IDbConnection conneciton, ICompiler compiler, Expression<Func<TEntity, bool>> where, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            AddDapperCustomAttributesMapper<TEntity>();

            var queryBuilder = new QueryBuilder<TEntity>(compiler, Logger);
            var query = queryBuilder.Select(where) as DbCompileResult;
            return conneciton.Query<TEntity>(query.SqlQuery, transaction: transaction, commandTimeout: commandTimeout);
        }

        public static int Insert<TEntity>(this IDbConnection conneciton, ICompiler compiler, TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(compiler, Logger);
            var query = queryBuilder.Insert(entity) as DbCompileResult;

            return conneciton.Execute(sql: query.SqlQuery,
                               param: query.QueryParameters,
                               transaction: transaction,
                               commandTimeout: commandTimeout,
                               commandType: CommandType.Text);
        }

        public static int Update<TEntity>(this IDbConnection conneciton, ICompiler compiler, TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(compiler, Logger);
            var query = queryBuilder.Update(entity) as DbCompileResult;

            return conneciton.Execute(sql: query.SqlQuery,
                               param: query.QueryParameters,
                               transaction: transaction,
                               commandTimeout: commandTimeout,
                               commandType: CommandType.Text);
        }

        public static int Update<TEntity>(this IDbConnection conneciton, ICompiler compiler, TEntity entity, Expression<Func<TEntity, bool>> where, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(compiler, Logger);
            var query = queryBuilder.Update(entity, where) as DbCompileResult;

            return conneciton.Execute(sql: query.SqlQuery,
                               param: query.QueryParameters,
                               transaction: transaction,
                               commandTimeout: commandTimeout,
                               commandType: CommandType.Text);
        }

        public static int Delete<TEntity>(this IDbConnection conneciton, ICompiler compiler, Expression<Func<TEntity, bool>> where, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var queryBuilder = new QueryBuilder<TEntity>(compiler, Logger);
            var query = queryBuilder.Delete(where) as DbCompileResult;

            return conneciton.Execute(sql: query.SqlQuery,
                               param: query.QueryParameters,
                               transaction: transaction,
                               commandTimeout: commandTimeout,
                               commandType: CommandType.Text);
        }


        private static void AddDapperCustomAttributesMapper<TEntity>() where TEntity : class
        {
            if (SqlMapper.GetTypeMap(typeof(TEntity)) == null)
                SqlMapper.SetTypeMap(typeof(TEntity), new ColumnAttributeTypeMapper<TEntity>());
        }
    }
}
