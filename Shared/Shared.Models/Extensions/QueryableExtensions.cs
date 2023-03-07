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

        public static IQueryable<T> SortMany<T>(this IQueryable<T> query, IEnumerable<(Expression<Func<T, object>> keySelector, bool isAscending)> sorts)
        {
            foreach (var sort in sorts)
            {
                query = sort.isAscending
                    ? query.SortBy(sort.keySelector)
                    : query.SortByDescending(sort.keySelector);
            }

            return query;
        }

        private static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> query, Expression<Func<T, object>> keySelector)
        {
            return query.GetType() is IOrderedQueryable<T> orderedQuery
                ? orderedQuery.ThenBy(keySelector)
                : query.OrderBy(keySelector);
        }

        private static IOrderedQueryable<T> SortByDescending<T>(this IQueryable<T> query, Expression<Func<T, object>> keySelector)
        {
            return query.GetType() is IOrderedQueryable<T> orderedQuery
                ? orderedQuery.ThenByDescending(keySelector)
                : query.OrderByDescending(keySelector);
        }
    }
}
