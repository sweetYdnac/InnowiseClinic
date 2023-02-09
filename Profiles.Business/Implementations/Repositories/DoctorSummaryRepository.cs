using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.DoctorSummary;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
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
                            (@Id, @SpecializationName, @OfficeAddress);
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("SpecializationName", dto.SpecializationName, DbType.String);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);

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
                                OfficeAddress = @OfficeAddress
                            WHERE Id = @Id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("SpecializationName", dto.SpecializationName, DbType.String);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);

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
    }
}
