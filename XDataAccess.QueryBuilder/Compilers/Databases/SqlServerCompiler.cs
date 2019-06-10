using System.Text;
using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class SqlServerCompiler : BaseCompiler
    {
        public SqlServerCompiler() : base(new SqlServerExpressionResolver(new SqlServerDialect()))
        {

        }
    }
}