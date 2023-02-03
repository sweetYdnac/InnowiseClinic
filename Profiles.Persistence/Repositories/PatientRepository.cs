using Dapper;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;
using Profiles.Persistence.Contexts;
using Serilog;
using System.Data;

namespace Profiles.Persistence.Repositories
{
    public class PatientRepository : IGenericRepository<PatientEntity>
    {
        private readonly ProfilesDbContext _db;

        public PatientRepository(ProfilesDbContext db) => _db = db;

        public async Task<Guid?> AddAsync(PatientEntity entity)
        {
            var query = """
                            INSERT Patients
                            VALUES
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @DateOfBirth, DEFAULT)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("MiddleName", entity.MiddleName, DbType.String);
            parameters.Add("AccountId", entity.AccountId, DbType.Guid);
            parameters.Add("DateOfBirth", entity.DateOfBirth, DbType.Date);

            using (var connection = _db.CreateConnection())
            {
                var result = await  connection.ExecuteAsync(query, parameters);

                if (result == 0)
                {
                    Log.Information("Entity wasn't add. {@entity}", entity);
                    return null;
                }
                else
                {
                    return entity.Id;
                }
            }
        }

        //    public Task<int> DeleteAsync(Guid id)
        //    {

        //    }

        //    public Task<IEnumerable<PatientEntity>> GetAllAsync()
        //    {

        //    }

        //    public Task<PatientEntity> GetByIdAsync(Guid id)
        //    {

        //    }

        //    public Task<(IEnumerable<PatientEntity> data, int totalCount)> GetByPageAsync(PagedParameters parameters)
        //    {

        //    }

        //    public Task<int> UpdateAsync(PatientEntity entity)
        //    {

    }
}
