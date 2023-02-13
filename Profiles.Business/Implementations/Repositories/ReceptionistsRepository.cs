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
                            SELECT FirstName, LastName, MiddleName, PhotoId, OfficeAddress
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
                            SELECT Receptionists.Id,
                                   CONCAT(FirstName,' ', LastName, ' ', MiddleName) AS FullName,
                                   OfficeAddress,
                                   Status
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

        public async Task<int> CreateAsync(CreateReceptionistDTO dto)
        {
            var query = """
                            INSERT Receptionists
                            VALUES
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @OfficeId, @PhotoId)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("AccountId", dto.AccountId, DbType.Guid);
            parameters.Add("OfficeId", dto.OfficeId, DbType.Guid);
            parameters.Add("PhotoId", dto.PhotoId, DbType.Guid);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdateReceptionistDTO dto)
        {
            var query = """
                            UPDATE Receptionists
                            SET FirstName = @FirstName,
                                LastName = @LastName,
                                MiddleName = @MiddleName,
                                OfficeId = @OfficeId
                            WHERE Id = @Id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("OfficeId", dto.OfficeId, DbType.Guid);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var query = """
                            DELETE Receptionists
                            WHERE Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
