using Dapper;
using Offices.Application.Features.Office.Queries;
using Offices.Application.Interfaces.Repositories;
using Offices.Domain.Entities;
using Offices.Persistence.Contexts;
using System.Data;

namespace Offices.Persistence.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficesDbContext _db;

        public OfficeRepository(OfficesDbContext db) => _db = db;

        public async Task<(IEnumerable<OfficeEntity> offices, int totalCount)> GetPagedOffices(GetOfficesQuery request)
        {
            var query =
                """
                    SELECT "Id", "Address", "RegistryPhoneNumber", "IsActive"
                    FROM "Offices"
                    ORDER BY "Id"
                        OFFSET @Offset ROWS
                        FETCH FIRST @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM "Offices"
                """;

            var parameters = new DynamicParameters();
            var t = request.PageSize * (request.PageNumber - 1);
            parameters.Add("Offset", t, DbType.Int32);
            parameters.Add("PageSize", request.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, parameters))
                {
                    var offices = await multi.ReadAsync<OfficeEntity>();
                    var count = await multi.ReadFirstAsync<int>();

                    return (offices, count);
                }
            }
        }
    }
}
