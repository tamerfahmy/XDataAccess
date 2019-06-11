using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions;
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

        public virtual ICompileResult CompilerInsert<TEntity>(TEntity entity) where TEntity : class
        {
            var entityMetadata = EntityMetadata.Parse(entity);

            var sb = new StringBuilder();

            var result = new DbCompilerResult();

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
    }
}