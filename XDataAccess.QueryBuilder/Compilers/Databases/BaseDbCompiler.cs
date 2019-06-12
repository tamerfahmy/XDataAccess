using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XDataAccess.QueryBuilder.CRUD;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions;
using XDataAccess.QueryBuilder.Expressions.Databases;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public abstract class BaseDbCompiler : ICompiler
    {
        public IExpressionResolver Resolver { get; protected set; }

        public IDialect Dialect { get; protected set; }

        public BaseDbCompiler()
        {

        }

        public virtual IResult CompileInsert<TEntity>(TEntity entity) where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse(entity);

            var sb = new StringBuilder();

            var result = new DbCompileResult();

            sb.Append($"{Dialect.Insert} {entityMetadata.EntityName} {Dialect.StartGroup}");

            var attNameIdx = 0;
            foreach (var attribute in entityMetadata.AttributesForInsertOrUpdate)
            {
                if (attNameIdx > 0)
                    sb.Append(Dialect.Comma);

                sb.Append($"{Dialect.OpeningIdentifier}{attribute.Name}{Dialect.ClosingIdentifier}");
                ++attNameIdx;
            }

            sb.Append($"{Dialect.EndGroup} {Dialect.Values} {Dialect.StartGroup}");

            attNameIdx = 0;
            foreach (var attribute in entityMetadata.AttributesForInsertOrUpdate)
            {
                if (attNameIdx > 0)
                    sb.Append(Dialect.Comma);

                var parameterName = $"{Dialect.ParameterPrefix}P{attNameIdx}";

                sb.Append(parameterName);
                result.QueryParameters.Add(parameterName, attribute.Value);

                ++attNameIdx;
            }

            sb.Append(Dialect.EndGroup);

            result.SqlQuery = sb.ToString();

            return result;
        }

        public virtual IResult CompileDelete<TEntity>() where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse<TEntity>();

            var sb = new StringBuilder();

            var result = new DbCompileResult();

            sb.Append($"{Dialect.Delete} {entityMetadata.EntityName}");

            result.SqlQuery = sb.ToString();

            return result;
        }

        public virtual IResult CompileDelete<TEntity>(Expression<Func<TEntity, bool>> whereExpression) where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse<TEntity>();

            var sb = new StringBuilder();

            var result = new DbCompileResult();

            var where = Resolver.Resolve<TEntity>(whereExpression.Body) as DbResolveResult;

            sb.Append($"{Dialect.Delete} {entityMetadata.EntityName} {Dialect.Where} {where.SqlQuery}");

            result.SqlQuery = sb.ToString();
            result.QueryParameters = where.QueryParameters;

            return result;
        }

        public virtual IResult CompileUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse(entity);
            if (!entityMetadata.HasIdentityAttribute)
                throw new ArgumentOutOfRangeException($"Can't update entity of type {typeof(TEntity)} because it has no Identity attribute specified.");
            else if (!entityMetadata.IsValidForInsertOrUpdate)
                throw new ArgumentOutOfRangeException($"Can't update entity of type {typeof(TEntity)} because there are no attributes to update.");
            else
            {
                var sb = new StringBuilder();
                var result = new DbCompileResult();
                sb.Append($"{Dialect.Update} {entityMetadata.EntityName} {Dialect.Set} ");

                var attNameIdx = 0;
                foreach (var attribute in entityMetadata.AttributesForInsertOrUpdate)
                {
                    if (attNameIdx > 0)
                        sb.Append(Dialect.Comma);

                    var parameterName = $"{Dialect.ParameterPrefix}P{attNameIdx}";
                    result.QueryParameters.Add(parameterName, attribute.Value);

                    sb.Append($"{Dialect.OpeningIdentifier}{attribute.Name}{Dialect.ClosingIdentifier} {Dialect.Equal} {parameterName}");
                    ++attNameIdx;
                }

                sb.Append($" {Dialect.Where} ");
                var IdentityIndex = 0;
                foreach (var attribute in entityMetadata.IdentityAttributes)
                {
                    if (IdentityIndex > 0)
                        sb.Append(Dialect.And);

                    var parameterName = $"{Dialect.ParameterPrefix}P{attNameIdx}";
                    result.QueryParameters.Add(parameterName, attribute.Value);

                    sb.Append($"{Dialect.OpeningIdentifier}{attribute.Name}{Dialect.ClosingIdentifier} {Dialect.Equal} {parameterName}");
                    ++attNameIdx;
                    ++IdentityIndex;
                }

                result.SqlQuery = sb.ToString();

                return result;
            }
        }

        public virtual IResult CompileUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse(entity);

            if (!entityMetadata.IsValidForInsertOrUpdate)
                throw new ArgumentOutOfRangeException($"Can't update entity of type {typeof(TEntity)} because there are no attributes to update.");
            else
            {
                var sb = new StringBuilder();
                var result = new DbCompileResult();

                sb.Append($"{Dialect.Update} {entityMetadata.EntityName} {Dialect.Set} ");

                var attNameIdx = 0;
                foreach (var attribute in entityMetadata.AttributesForInsertOrUpdate)
                {
                    if (attNameIdx > 0)
                        sb.Append(Dialect.Comma);

                    var parameterName = $"{Dialect.ParameterPrefix}P{attNameIdx}";
                    result.QueryParameters.Add(parameterName, attribute.Value);

                    sb.Append($"{Dialect.OpeningIdentifier}{attribute.Name}{Dialect.ClosingIdentifier} {Dialect.Equal} {parameterName}");
                    ++attNameIdx;
                }
                result.SqlQuery = sb.ToString();

                var where = Resolver.Resolve<TEntity>(whereExpression.Body) as DbResolveResult;

                return result.Merge(where, Dialect);
            }
        }

        public IResult CompileSelect<TEntity>(Expression<Func<TEntity, bool>> whereExpression) where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse<TEntity>();

            var sb = new StringBuilder();
            var result = new DbCompileResult();

            sb.Append($"{Dialect.Select} * {Dialect.From} {entityMetadata.EntityName}");

            var where = Resolver.Resolve<TEntity>(whereExpression.Body) as DbResolveResult;

            sb.Append($" {Dialect.Where} {where.SqlQuery}");

            result.QueryParameters = where.QueryParameters;
            result.SqlQuery = sb.ToString();

            return result;
        }

        public IResult CompileSelect<TEntity>() where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse<TEntity>();

            var sb = new StringBuilder();
            var result = new DbCompileResult();

            sb.Append($"{Dialect.Select} * {Dialect.From} {entityMetadata.EntityName}");

            result.SqlQuery = sb.ToString();

            return result;
        }
    }
}