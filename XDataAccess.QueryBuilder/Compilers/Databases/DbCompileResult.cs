using System.Collections.Generic;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Compilers.Databases {
    public class DbCompileResult : IResult {
        public IDictionary<string, object> QueryParameters { get; set; }

        public string SqlQuery { get; set; }

        public DbCompileResult () {
            QueryParameters = new Dictionary<string, object> ();
        }

        public DbCompileResult Merge (DbCompileResult result, IDialect dialect) {
            int lastParamIndex = QueryParameters.Count;
            var query = result.SqlQuery;

            foreach (var param in result.QueryParameters) {
                var newParamName = $"{dialect.ParameterPrefix}P{lastParamIndex}";

                QueryParameters.Add (newParamName, param.Value);
                query.Replace (param.Key, newParamName);

                lastParamIndex++;
            }

            var sqlQuery = $"{SqlQuery} {query}";

            return this;
        }
    }
}