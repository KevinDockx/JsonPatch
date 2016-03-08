// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Marvin.JsonPatch.Helpers
{
    internal static class ExpressionHelpers
    {
        public static string GetPath<T, TProp>(Expression<Func<T, TProp>> expr, IJsonPatchPropertyResolver resolver) where T : class
        {
            return "/" + GetPath(expr.Body, true, resolver);
        }


        private static string GetPath(Expression expr, bool firstTime, IJsonPatchPropertyResolver resolver)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.ArrayIndex:
                    var binaryExpression = (BinaryExpression)expr;

                    if (ContinueWithSubPath(binaryExpression.Left.NodeType, false))
                    {
                        var leftFromBinaryExpression = GetPath(binaryExpression.Left, false, resolver);
                        return leftFromBinaryExpression + "/" + binaryExpression.Right.ToString();
                    }
                    else
                    {
                        return binaryExpression.Right.ToString();
                    }

                case ExpressionType.Call:
                    var methodCallExpression = (MethodCallExpression)expr;

                    if (ContinueWithSubPath(methodCallExpression.Object.NodeType, false))
                    {
                        var leftFromMemberCallExpression = GetPath(methodCallExpression.Object, false, resolver);
                        return leftFromMemberCallExpression + "/" +
                            GetIndexerInvocation(methodCallExpression.Arguments[0]);
                    }
                    else
                    {
                        return GetIndexerInvocation(methodCallExpression.Arguments[0]);
                    }

                case ExpressionType.Convert:
                    return GetPath(((UnaryExpression)expr).Operand, false, resolver);

                case ExpressionType.MemberAccess:
                    var memberExpression = expr as MemberExpression;

                    if (ContinueWithSubPath(memberExpression.Expression.NodeType, false))
                    {
                        var left = GetPath(memberExpression.Expression, false, resolver);
                        return left + "/" + resolver.GetName(memberExpression.Member);
                    }
                    else
                    {
                        return resolver.GetName(memberExpression.Member);
                    }

                case ExpressionType.Parameter:
                    // Fits "x => x" (the whole document which is "" as JSON pointer)
                    return firstTime ? "" : null;

                default:
                    return "";
            }
        }

        private static bool ContinueWithSubPath(ExpressionType expressionType, bool firstTime)
        {
            if (firstTime)
            {
                return (expressionType == ExpressionType.ArrayIndex
                       || expressionType == ExpressionType.Call
                       || expressionType == ExpressionType.Convert
                       || expressionType == ExpressionType.MemberAccess
                       || expressionType == ExpressionType.Parameter);
            }
            else
            {
                return (expressionType == ExpressionType.ArrayIndex
                    || expressionType == ExpressionType.Call
                    || expressionType == ExpressionType.Convert
                    || expressionType == ExpressionType.MemberAccess);
            }
        }

        private static string GetIndexerInvocation(Expression expression)
        {
            var converted = Expression.Convert(expression, typeof(object));
            var fakeParameter = Expression.Parameter(typeof(object), null);
            var lambda = Expression.Lambda<Func<object, object>>(converted, fakeParameter);
            Func<object, object> func;

            func = lambda.Compile();

            return Convert.ToString(func(null), CultureInfo.InvariantCulture);
        }
    }
}
