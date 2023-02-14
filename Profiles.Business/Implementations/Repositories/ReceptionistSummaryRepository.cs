using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.ReceptionistSummary;
using Shared.Core.Enums;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class ReceptionistSummaryRepository : IReceptionistSummaryRepository
    {
        private readonly ProfilesDbContext _db;

        public ReceptionistSummaryRepository(ProfilesDbContext db) => _db = db;

        public async Task<int> AddAsync(CreateReceptionistSummaryDTO dto)
        {
            var query = """
                            INSERT ReceptionistsSummary
                            VALUES
                            (@Id, @OfficeAddress, @Status)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);
            parameters.Add("Status", dto.Status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdateReceptionistSummaryDTO dto)
        {
            var query = """
                            UPDATE ReceptionistsSummary
                            SET OfficeAddress = @OfficeAddress,
                                Status = @Status
                            WHERE Id = @id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);
            parameters.Add("Status", dto.Status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var query = """
                            DELETE ReceptionistsSummary
                            WHERE Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<int> ChangeStatus(Guid id, AccountStatuses status)
        {
            var query = """
                            UPDATE ReceptionistsSummary
                            SET Status = @Status
                            WHERE Id = @Id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Status", status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
