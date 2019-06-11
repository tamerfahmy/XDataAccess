using System.Text;
using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;
using XDataAccess.QueryBuilder.Expressions.Databases;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class SqlServerCompiler : BaseDbCompiler
    {
        public SqlServerCompiler()
        {
            Dialect = new SqlServerDialect();
            Resolver = new SqlServerExpressionResolver(Dialect);
        }
    }
}