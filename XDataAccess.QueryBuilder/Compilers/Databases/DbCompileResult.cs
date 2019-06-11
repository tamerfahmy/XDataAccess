using System.Collections.Generic;

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
    }
}