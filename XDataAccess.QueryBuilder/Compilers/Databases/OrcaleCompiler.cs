using System.Text;
using XDataAccess.QueryBuilder.Dialects.Databases;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers.Databases
{
    public sealed class OrcaleCompiler : BaseCompiler
    {
        public OrcaleCompiler() : base(new OracleExpressionResolver(new OracleDialect()))
        {
        }
    }
}