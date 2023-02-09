using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Shared.Models.Response.Profiles.Receptionist;

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
    }
}
