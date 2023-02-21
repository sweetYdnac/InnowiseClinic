using Dapper;
using System.Data;

namespace Shared.Models.Extensions
{
    public static class DbConnectionExtensions
    {
        public async static Task<PagedResult<T>> QueryPagedResultAsync<T>
            (this IDbConnection connection, string query, DynamicParameters parameters)
        {
            using (var multi = await connection.QueryMultipleAsync(query, parameters))
            {
                var items = await multi.ReadAsync<T>();
                var count = await multi.ReadFirstAsync<int>();

                return new PagedResult<T> { Items = items, TotalCount = count };
            }
        }
    }
}
