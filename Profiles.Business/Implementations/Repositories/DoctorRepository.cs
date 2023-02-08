using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.Entities;
using Serilog;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorRepository(ProfilesDbContext db) => _db = db;

        public async Task<Guid?> AddAsync(DoctorEntity entity)
        {
            var query = """
                            INSERT Doctors
                            VALUES
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @DateOfBirth, @SpecializationId, @OfficeId, @CareerStartYear);
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("MiddleName", entity.MiddleName, DbType.String);
            parameters.Add("AccountId", entity.AccountId, DbType.Guid);
            parameters.Add("DateOfBirth", entity.DateOfBirth, DbType.Date);
            parameters.Add("SpecializationId", entity.SpecializationId, DbType.Guid);
            parameters.Add("OfficeId", entity.OfficeId, DbType.Guid);
            parameters.Add("CareerStartYear", entity.CareerStartYear, DbType.Date);

            using (var connection = _db.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);

                if (result == 0)
                {
                    Log.Information("Doctor wasn't added. {@entity}", entity);
                    return null;
                }
                else
                {
                    return entity.Id;
                }
            }
        }
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<DoctorEntity> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task UpdateAsync(DoctorEntity entity) => throw new NotImplementedException();
    }
}
