using System.Collections.Generic;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Databases;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public class DbCompileResult : IResult
    {
        public IDictionary<string, object> QueryParameters { get; set; }

        public string SqlQuery { get; set; }

        public DbCompileResult()
        {
            QueryParameters = new Dictionary<string, object>();
        }

        public DbCompileResult MergeWhere(DbCompileResult whereResult, IDialect dialect)
        {
            int lastParamIndex = QueryParameters.Count;
            var query = whereResult.SqlQuery;

            foreach (var param in whereResult.QueryParameters)
            {
                var newParamName = $"{dialect.ParameterPrefix}P{lastParamIndex}";

                QueryParameters.Add(newParamName, param.Value);
                query = query.Replace(param.Key, newParamName);

                lastParamIndex++;
            }

            var sqlQuery = $"{SqlQuery} {dialect.Where} {query}";

            return this;
        }

        public DbCompileResult Merge(DbResolveResult whereResult, IDialect dialect)
        {
            int lastParamIndex = QueryParameters.Count;
            var query = whereResult.SqlQuery;

            foreach (var param in whereResult.QueryParameters)
            {
                var newParamName = $"{dialect.ParameterPrefix}P{lastParamIndex}";

                QueryParameters.Add(newParamName, param.Value);
                query = query.Replace(param.Key, newParamName);

                lastParamIndex++;
            }

            SqlQuery = $"{SqlQuery} {dialect.Where} {query}";

            return this;
        }

        public override string ToString()
        {
            return SqlQuery;
        }
    }
}