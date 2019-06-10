using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using XDataAccess.QueryBuilder.Attributes;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers
{
    public abstract class BaseCompiler : ICompiler
    {
        public IExpressionResolver Resolver { get; private set; }

        public BaseCompiler(IExpressionResolver resolver)
        {
            Resolver = resolver;
        }

        internal IDictionary<string, object> QueryParameters { get; set; }

        internal string SqlQuery { get; set; }

        //internal virtual void CompilerInsert<TEntity>(TEntity entity) where TEntity : class
        //{
        //    var entityMetadata = Utilities.GetEntityMetadata(entity);

        //    var sb = new StringBuilder();

        //    sb.Append($"INSERT INTO {entityMetadata.EntityName} (");

        //    var attNameIdx = 0;
        //    foreach (var attribute in entityMetadata.AttributesForInsertOrUpdate)
        //    {
        //        if (attNameIdx > 0)
        //            sb.Append(", ");

        //        sb.Append($"{OpeningIdentifier}{attribute.Name}{ClosingIdentifier}");
        //        ++attNameIdx;
        //    }

        //    sb.Append(") VALUES (");

        //    attNameIdx = 0;
        //    foreach (var attribute in entityMetadata.AttributesForInsertOrUpdate)
        //    {
        //        if (attNameIdx > 0)
        //            sb.Append(", ");

        //        var parameterName = $"{ParameterPrefix}{attNameIdx}";

        //        sb.Append(parameterName);
        //        QueryParameters.Add(parameterName, attribute.Value);

        //        ++attNameIdx;
        //    }

        //    sb.Append(")");

        //    SqlQuery = sb.ToString();
        //}

        //internal virtual void CompilerDelete<TEntity>() where TEntity : class
        //{
        //    var entityMetadata = Utilities.GetEntityMetadata<TEntity>();

        //    var sb = new StringBuilder();

        //    sb.Append($"DELETE FROM {entityMetadata.EntityName}");

        //    SqlQuery = sb.ToString();
        //}

        //internal void CompilerDelete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class
        //{
        //    var entityMetadata = Utilities.GetEntityMetadata<TEntity>();

        //    var sb = new StringBuilder();

        //    sb.Append($"DELETE FROM {entityMetadata.EntityName}");

        //    var where = ExpressionConverter.Convert(exp.Body);

        //    SqlQuery = sb.ToString();
        //}
    }
}