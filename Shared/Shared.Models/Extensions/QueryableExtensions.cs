using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shared.Models.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includes) where T : class
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public static IQueryable<T> FilterMany<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, bool>>> filters)
        {
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public static IQueryable<T> FilterByPage<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            return query
                .Skip(pageSize * (currentPage - 1))
                .Take(pageSize);
        }

        public static IQueryable<T> SortMany<T>(this IQueryable<T> query, IDictionary<Expression<Func<T, object>>, bool> sorts)
        {
            foreach (var sort in sorts)
            {
                query = sort.Value
                    ? query.SortBy(sort.Key)
                    : query.SortByDescending(sort.Key);
            }

            return query;
        }

        private static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> query, Expression<Func<T, object>> keySelector)
        {
            if (query.IsOrdered())
            {
                var orderedQuery = query as IOrderedQueryable<T>;
                return orderedQuery.ThenBy(keySelector);
            }
            else
            {
                return query.OrderBy(keySelector);
            }
        }

        private static IOrderedQueryable<T> SortByDescending<T>(this IQueryable<T> query, Expression<Func<T, object>> keySelector)
        {
            if (query.IsOrdered())
            {
                var orderedQuery = query as IOrderedQueryable<T>;
                return orderedQuery.ThenByDescending(keySelector);
            }
            else
            {
                return query.OrderByDescending(keySelector);
            }
        }

        private static bool IsOrdered<T>(this IQueryable<T> query)
        {
            if (query.Expression.NodeType.Equals(ExpressionType.Call))
            {
                var methodCallExpression = (MethodCallExpression)query.Expression;
                var method = methodCallExpression.Method;

                if (method.Name.ToLower().Contains("order"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
