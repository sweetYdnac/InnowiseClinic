using Dapper;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.DoctorSummary;
using Profiles.Data.Interfaces.Repositories;
using Shared.Core.Enums;
using System.Data;

namespace Profiles.Data.Implementations.Repositories
{
    public class DoctorSummaryRepository : IDoctorSummaryRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorSummaryRepository(ProfilesDbContext db) => _db = db;

        public async Task<int> AddAsync(CreateDoctorSummaryDTO dto)
        {
            var query = """
                            INSERT DoctorsSummary
                            VALUES
                            (@Id, @SpecializationName, @OfficeAddress, @Status);
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("SpecializationName", dto.SpecializationName, DbType.String);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);
            parameters.Add("Status", dto.Status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdateDoctorSummaryDTO dto)
        {
            var query = """
                            UPDATE DoctorsSummary
                            SET SpecializationName = @SpecializationName,
                                OfficeAddress = @OfficeAddress,
                                Status = @Status
                            WHERE Id = @Id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("SpecializationName", dto.SpecializationName, DbType.String);
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
                            DELETE DoctorsSummary
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
                            UPDATE DoctorsSummary
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