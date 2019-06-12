using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;

namespace XDataAccess.QueryBuilder.Expressions.Databases
{
    public sealed class DbResolveResult : IResult
    {
        public IDictionary<string, object> QueryParameters { get; set; }

        public string SqlQuery { get { return Builder.ToString(); } }

        public StringBuilder Builder { get; set; }

        public DbResolveResult()
        {
            QueryParameters = new Dictionary<string, object>();
            Builder = new StringBuilder();
        }

        public string AddParameter(IDialect dialect, object value)
        {
            var paramName = $"{dialect.ParameterPrefix}P{QueryParameters.Count}";
            QueryParameters.Add(paramName, value);

            return paramName;
        }
    }
}