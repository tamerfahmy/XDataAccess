using System.Text;
using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class OrcaleCompiler : BaseDbCompiler
    {
        public OrcaleCompiler()
        {
            Dialect = new OracleDialect();
            Resolver = new OracleExpressionResolver(Dialect);
        }
    }
}