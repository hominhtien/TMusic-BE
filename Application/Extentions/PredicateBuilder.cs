﻿using System.Linq.Expressions;
using System.Text;

namespace Application.Extentions
{
    /// <summary>
    /// The Predicate builder
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// Build predicate filter
        /// </summary>
        /// <typeparam name="T">Type of query</typeparam>
        /// <param name="propertyName">Property name</param>
        /// <param name="comparison">Comparison</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Build<T>(string propertyName, string comparison, string value)
        {
            // any name
            string parameterName = "x";
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            var body = MakeComparison(left, comparison, value);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression MakeComparison(Expression left, string comparison, string value)
        {
            return comparison switch
            {
                "==" => MakeBinary(ExpressionType.Equal, left, value),
                "!=" => MakeBinary(ExpressionType.NotEqual, left, value),
                ">" => MakeBinary(ExpressionType.GreaterThan, left, value),
                ">=" => MakeBinary(ExpressionType.GreaterThanOrEqual, left, value),
                "<" => MakeBinary(ExpressionType.LessThan, left, value),
                "<=" => MakeBinary(ExpressionType.LessThanOrEqual, left, value),
                "Contains" or "StartsWith" or "EndsWith" => Expression.Call(MakeString(left), comparison, Type.EmptyTypes, Expression.Constant(value, typeof(string))),
                "In" => MakeList(left, value.Split(',')),
                _ => throw new NotSupportedException($"Invalid comparison operator '{comparison}'."),
            };
        }

        private static Expression MakeList(Expression left, IEnumerable<string> codes)
        {
            var objValues = codes.Cast<object>().ToList();
            var type = typeof(List<object>);
            var methodInfo = type.GetMethod("Contains", new Type[] { typeof(object) });
            var list = Expression.Constant(objValues);
            var body = Expression.Call(list, methodInfo, left);
            return body;
        }

        private static Expression MakeString(Expression source)
        {
            return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
        }

        private static Expression MakeBinary(ExpressionType type, Expression left, string value)
        {
            object typedValue = value;
            if (left.Type != typeof(string))
            {
                if (string.IsNullOrEmpty(value))
                {
                    typedValue = null;
                    if (Nullable.GetUnderlyingType(left.Type) == null)
                        left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
                }
                else
                {
                    var valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                    typedValue = valueType.IsEnum ? Enum.Parse(valueType, value) :
                        valueType == typeof(Guid) ? Guid.Parse(value) :
                        Convert.ChangeType(value, valueType);
                }
            }
            var right = Expression.Constant(typedValue, left.Type);
            return Expression.MakeBinary(type, left, right);
        }
    }
}
