using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XDataAccess.QueryBuilder.Dialects;
using XDataAccess.QueryBuilder.Expressions.Databases;
using XDataAccess.QueryBuilder.Expressions.Interceptors;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder.Expressions.Databases
{
    public abstract class BaseDbExpressionResolver : ExpressionVisitor, IExpressionResolver
    {
        public IDialect Dialect { get; private set; }

        public IResult Result { get; private set; }

        public EntityMetadata EntityMetadata { get; private set; }

        public InterceptorFactory InterceptorFactory { get; private set; }

        public DataType DataType { get; protected set; }

        public BaseDbExpressionResolver(IDialect dialect)
        {
            Dialect = dialect;
            Result = new DbResolveResult();
            InterceptorFactory = new InterceptorFactory(this, dialect);
        }

        public virtual IResult Resolve<TEntity>(Expression expression) where TEntity : class
        {
            EntityMetadata = EntityMetadata.Parse<TEntity>();
            Result = new DbResolveResult();

            if (expression.Type == typeof(bool) && expression.NodeType == ExpressionType.MemberAccess)
                expression = ExpressionsHelper.GetBinaryExpression(expression);

            Visit(expression);

            return Result;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            var left = b.Left;
            var right = b.Right;
            var boolOperators = new[] { ExpressionType.AndAlso, ExpressionType.OrElse };
            if (boolOperators.Contains(b.NodeType))
            {
                left = left.NodeType == ExpressionType.MemberAccess ? ExpressionsHelper.GetBinaryExpression(left) : left;
                right = right.NodeType == ExpressionType.MemberAccess ? ExpressionsHelper.GetBinaryExpression(right) : right;
            }

            var builder = (Result as DbResolveResult).Builder;

            builder.Append(Dialect.StartGroup);
            this.Visit(left);

            switch (b.NodeType)
            {
                case ExpressionType.And:
                    builder.Append($" {Dialect.And} ");
                    break;
                case ExpressionType.AndAlso:
                    builder.Append($" {Dialect.And} ");
                    break;
                case ExpressionType.Or:
                    builder.Append($" {Dialect.Or} ");
                    break;
                case ExpressionType.OrElse:
                    builder.Append($" {Dialect.Or} ");
                    break;
                case ExpressionType.Equal:
                    if (ExpressionsHelper.IsNullConstant(b.Right))
                        builder.Append($" {Dialect.Is} ");
                    else
                        builder.Append($" {Dialect.Equal} ");
                    break;
                case ExpressionType.NotEqual:
                    if (ExpressionsHelper.IsNullConstant(b.Right))
                        builder.Append($" {Dialect.IsNot} ");
                    else
                        builder.Append($" {Dialect.NotEqual} ");
                    break;
                case ExpressionType.LessThan:
                    builder.Append($" {Dialect.LessThan} ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    builder.Append($" {Dialect.LessThanOrEqual} ");
                    break;
                case ExpressionType.GreaterThan:
                    builder.Append($" {Dialect.GreaterThan} ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    builder.Append($" {Dialect.GreaterThanOrEqual} ");
                    break;
                default:
                    throw new NotSupportedException($"The binary operator '{b.NodeType}' is not supported");

            }

            this.Visit(right);
            builder.Append(Dialect.EndGroup);
            return b;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                var builder = (Result as DbResolveResult).Builder;

                var parameterExpression = (ParameterExpression)m.Expression;
                var entityAttribute = EntityMetadata.GetAttribute(m.Member.Name);

                builder.Append($"{Dialect.OpeningIdentifier}{entityAttribute.Name}{Dialect.ClosingIdentifier}");
            }
            else if (m.Expression != null && m.Expression.NodeType == ExpressionType.Constant)
            {
                var eval = ExpressionsHelper.EvaluateMember(m);
                var constantExpression = Expression.Constant(eval, m.Type);
                Visit(constantExpression);
                return constantExpression;
            }
            return m;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            var result = (Result as DbResolveResult);
            var q = c.Value as IQueryable;

            if (q == null && c.Value == null)
                result.Builder.Append("NULL");
            else if (q == null)
            {
                var paramKey = result.AddParameter(Dialect, c.Value);
                result.Builder.Append(paramKey);
            }

            return c;
        }

        protected override Expression VisitMethodCall(MethodCallExpression mExpr)
        {
            var interceptor = InterceptorFactory.FindInterceptor(mExpr);
            if (interceptor != null)
                interceptor.Intercept(Result, mExpr, (expr) => Visit(expr));
            else
            {
                var compiled = Expression.Lambda(mExpr).Compile();
                var constantExpression = Expression.Constant(compiled.DynamicInvoke(), mExpr.Type);
                Visit(constantExpression);
            }

            return mExpr;
        }
    }
}
