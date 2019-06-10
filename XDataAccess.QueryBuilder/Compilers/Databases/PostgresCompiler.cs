using System.Text;
using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class PostgresCompiler : BaseCompiler
    {
        public PostgresCompiler() : base(new PostgresExpressionResolver(new PostgresDialect()))
        {

        }
    }
}