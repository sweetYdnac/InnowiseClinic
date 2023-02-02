using Dapper;
using Offices.Application.DTOs;
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

        public async Task<(IEnumerable<OfficeEntity> offices, int totalCount)> GetPagedOffices(GetPagedOfficesDTO dto)
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
            parameters.Add("Offset", dto.PageSize * (dto.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", dto.PageSize, DbType.Int32);

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
