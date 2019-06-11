using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions;

namespace XDataAccess.QueryBuilder.Compilers
{
    public interface ICompiler
    {
        IExpressionResolver Resolver { get; }

        IDialect Dialect { get; }

        ICompileResult CompilerInsert<TEntity>(TEntity entity) where TEntity : class;
    }
}