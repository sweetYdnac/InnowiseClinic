using Dapper;
using Offices.Business.Interfaces.Repositories;
using Offices.Data.Contexts;
using Offices.Data.DTOs;
using Shared.Models.Response.Offices;
using System.Data;

namespace Offices.Business.Implementations.Repositories
{
    public class OfficeInformationRepository : IOfficeInformationRepository
    {
        private readonly OfficesDbContext _db;

        public OfficeInformationRepository(OfficesDbContext db) => _db = db;

        public async Task<(IEnumerable<OfficeInformationResponse> offices, int totalCount)> GetPagedOfficesAsync(GetPagedOfficesDTO dto)
        {
            var query = """
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
                    var offices = await multi.ReadAsync<OfficeInformationResponse>();
                    var count = await multi.ReadFirstAsync<int>();

                    return (offices, count);
                }
            }
        }
    }
}
