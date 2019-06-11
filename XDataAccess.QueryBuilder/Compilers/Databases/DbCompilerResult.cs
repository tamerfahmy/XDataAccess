using System.Collections.Generic;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public class DbCompilerResult : ICompileResult
    {
        public IDictionary<string, object> QueryParameters { get; set; }

        public string SqlQuery { get; set; }

        public DbCompilerResult()
        {
            QueryParameters = new Dictionary<string, object>();
        }
    }
}