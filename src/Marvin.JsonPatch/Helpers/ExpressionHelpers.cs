// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Marvin.JsonPatch.Helpers
{
    /// <summary>
    /// Expression helpers. These can be used when implementing custom ObjectAdapters.
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Returns the path from a given expression.  Takes JsonProperty into account.
        /// </summary>
        /// <typeparam name="T">Class type</typeparam>
        /// <typeparam name="TProp">Property type on class</typeparam>
        /// <param name="expr">Expression the path must be returned from</param>
        /// <returns>Property path</returns>
        public static string GetPath<T, TProp>(Expression<Func<T, TProp>> expr) where T : class
        {        
            return "/" + GetPath(expr.Body, true);
        }
 
        private static string GetPath(Expression expr, bool firstTime)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.ArrayIndex:
                    var binaryExpression = (BinaryExpression)expr;

                    if (ContinueWithSubPath(binaryExpression.Left.NodeType, false))
                    {
                        var leftFromBinaryExpression = GetPath(binaryExpression.Left, false);
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
                        var leftFromMemberCallExpression = GetPath(methodCallExpression.Object, false);
                        return leftFromMemberCallExpression + "/" +
                            GetIndexerInvocation(methodCallExpression.Arguments[0]);
                    }
                    else
                    {
                        return GetIndexerInvocation(methodCallExpression.Arguments[0]);
                    }

                case ExpressionType.Convert:
                    return GetPath(((UnaryExpression)expr).Operand, false);

                case ExpressionType.MemberAccess:
                    var memberExpression = expr as MemberExpression;

                    if (ContinueWithSubPath(memberExpression.Expression.NodeType, false))
                    {
                        var left = GetPath(memberExpression.Expression, false);

                        // if there's a JsonProperty attribute, we must return the PropertyName
                        // from the attribute rather than the member name 

                        var jsonPropertyAttribute =
                            memberExpression.Member.GetCustomAttributes(
                            typeof(JsonPropertyAttribute), false);

                        if (jsonPropertyAttribute.Length > 0)
                        {
                            // get value
                            var castedAttribrute = jsonPropertyAttribute[0] as JsonPropertyAttribute;
                            return left + "/" + castedAttribrute.PropertyName;
                        }

                        return left + "/" + memberExpression.Member.Name;
                    }
                    else
                    {
                        // Same here: if there's a JsonProperty attribute, we must return the PropertyName
                        // from the attribute rather than the member name 

                        var jsonPropertyAttribute =
                            memberExpression.Member.GetCustomAttributes(
                            typeof(JsonPropertyAttribute), false);

                        if (jsonPropertyAttribute.Length > 0)
                        {
                            // get value
                            var castedAttribrute = jsonPropertyAttribute[0] as JsonPropertyAttribute;
                            return castedAttribrute.PropertyName;
                        }

                        return memberExpression.Member.Name;
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
