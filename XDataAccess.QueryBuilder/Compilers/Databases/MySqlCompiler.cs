using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class MySqlCompiler : BaseDbCompiler
    {
        public MySqlCompiler()
        {
            Dialect = new MySqlDialect();
            Resolver = new MySqlExpressionResolver(Dialect);
        }
    }
}