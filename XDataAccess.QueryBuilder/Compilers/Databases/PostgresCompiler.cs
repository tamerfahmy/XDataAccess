using System.Text;
using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;
using XDataAccess.QueryBuilder.Expressions.Databases;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class PostgresCompiler : BaseDbCompiler
    {
        public PostgresCompiler()
        {
            Dialect = new PostgresDialect();
            Resolver = new PostgresExpressionResolver(Dialect);
        }
    }
}