using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.ReceptionistSummary;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class ReceptionistSummaryRepository : IReceptionistSummaryRepository
    {
        private readonly ProfilesDbContext _db;

        public ReceptionistSummaryRepository(ProfilesDbContext db) => _db = db;

        public async Task<int> CreateAsync(CreateReceptionistSummaryDTO dto)
        {
            var query = """
                            INSERT ReceptionistsSummary
                            VALUES
                            (@Id, @OfficeAddress)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
