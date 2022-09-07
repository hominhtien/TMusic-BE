
using System.Linq.Expressions;

namespace Application.Extentions
{
    public static class Queryable
    {
        /// <summary>
        /// Filter by condition
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }
            else
            {
                return source;
            }
        }

        /// <summary>
        /// Get query paged
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="source">Source query</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Query items of page</returns>
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int? page, int? pageSize)
        {
            return page.HasValue && pageSize.HasValue ? source.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value) : source;
        }

        /// <summary>
        /// Generic filter with comparison
        /// </summary>
        /// <typeparam name="T">Source type</typeparam>
        /// <param name="source">Source query</param>
        /// <param name="propertyName">Name property filter</param>
        /// <param name="comparison">Comparison filter</param>
        /// <param name="value">Value filter</param>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string propertyName, string comparison, string value)
        {
            if (string.IsNullOrEmpty(value) || !typeof(T).HasProperty(propertyName))
            {
                return source;
            }
            return source.Where(PredicateBuilder.Build<T>(propertyName, comparison, value));
        }
    }
}
