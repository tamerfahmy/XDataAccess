using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers
{
    public interface ICompiler
    {
        IExpressionResolver Resolver { get; }

    }
}