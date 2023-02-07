using Dapper;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;
using Profiles.Persistence.Contexts;
using Serilog;
using System.Data;

namespace Profiles.Persistence.Repositories
{
    public class DoctorSummaryRepository : IGenericRepository<DoctorSummary>
    {
        private readonly ProfilesDbContext _db;

        public DoctorSummaryRepository(ProfilesDbContext db) => _db = db;

        public async Task<Guid?> AddAsync(DoctorSummary entity)
        {
            var query = """
                            INSERT DoctorsSummary
                            VALUES
                            (@Id, @SpecializationName, @OfficeAddress);
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("SpecializationName", entity.SpecializationName, DbType.String);
            parameters.Add("OfficeAddress", entity.OfficeAddress, DbType.String);

            using (var connection = _db.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);

                if (result == 0)
                {
                    Log.Information("DoctorSummary wasn't added. {@entity}", entity);
                    return null;
                }
                else
                {
                    return entity.Id;
                }
            }
        }
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<DoctorSummary> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task UpdateAsync(DoctorSummary entity) => throw new NotImplementedException();
    }
}
