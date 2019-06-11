using System;
using System.Linq.Expressions;

namespace XDataAccess.QueryBuilder.Expressions
{
    internal class ExpressionsHelper
    {
        internal static MemberExpression GetMemberExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return expression as MemberExpression;
                case ExpressionType.Convert:
                    return GetMemberExpression((expression as UnaryExpression).Operand);
            }
            throw new ArgumentException("Member expression expected");
        }
        internal static BinaryExpression GetBinaryExpression(Expression expression)
        {
            var binaryExpression = expression as BinaryExpression;
            var body = binaryExpression ?? Expression.MakeBinary(ExpressionType.Equal, expression, Expression.Constant(true));
            return body;
        }

        internal static bool IsNullConstant(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null)
            || (exp.NodeType == ExpressionType.MemberAccess && ((MemberExpression)exp).Expression.NodeType == ExpressionType.Constant
            && EvaluateMember((MemberExpression)exp) == null);
        }

        internal static object EvaluateMember(MemberExpression m)
        {
            var compiled = Expression.Lambda(m).Compile();
            return compiled.DynamicInvoke();
        }
    }
}