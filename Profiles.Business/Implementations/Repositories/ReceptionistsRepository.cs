using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.Receptionist;
using Shared.Models.Response.Profiles.Receptionist;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class ReceptionistsRepository : IReceptionistsRepository
    {
        private readonly ProfilesDbContext _db;

        public ReceptionistsRepository(ProfilesDbContext db) => _db = db;

        public async Task<ReceptionistResponse> GetByIdAsync(Guid id)
        {
            var query = """
                            SELECT FirstName, LastName, MiddleName, OfficeAddress
                            FROM Receptionists
                            JOIN ReceptionistsSummary On Receptionists.Id = ReceptionistsSummary.Id
                            WHERE Receptionists.Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ReceptionistResponse>(query, new { id });
            }
        }

        public async Task<(IEnumerable<ReceptionistInformationResponse> receptionists, int totalCount)> GetPagedAsync(GetReceptionistsDTO dto)
        {
            var query = $"""
                            SELECT CONCAT(FirstName,' ', LastName, ' ', MiddleName) AS FullName,
                                   OfficeAddress
                            FROM Receptionists
                            JOIN ReceptionistsSummary On Receptionists.Id = ReceptionistsSummary.Id
                            ORDER BY Receptionists.Id
                                OFFSET @Offset ROWS
                                FETCH FIRST @PageSize ROWS ONLY;
                        
                            SELECT COUNT(*)
                            FROM Receptionists
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Offset", dto.PageSize * (dto.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", dto.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, parameters))
                {
                    var receptionists = await multi.ReadAsync<ReceptionistInformationResponse>();
                    var totalCount = await multi.ReadFirstAsync<int>();

                    return (receptionists, totalCount);
                }
            }
        }
    }
}
