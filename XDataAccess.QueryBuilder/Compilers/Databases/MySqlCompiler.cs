using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;
using XDataAccess.QueryBuilder.Expressions.Databases;

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