using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class MySqlCompiler : BaseCompiler
    {
        public MySqlCompiler() : base(new MySqlExpressionResolver(new MySqlDialect()))
        {

        }
    }
}