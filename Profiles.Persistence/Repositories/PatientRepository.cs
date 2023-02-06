using Dapper;
using Profiles.Application.Features.Patient.Queries;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;
using Profiles.Persistence.Contexts;
using Serilog;
using Shared.Exceptions;
using System.Data;

namespace Profiles.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
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

        public async Task<PatientEntity> GetByIdAsync(Guid id)
        {
            var query = """
                            SELECT FirstName, LastName, MiddleName, DateOfBirth
                            FROM Patients
                            WHERE Id = @Id;
                        """;

            using (var connection = _db.CreateConnection())
            {
                var patient = await connection.QueryFirstOrDefaultAsync<PatientEntity>(query, new { Id = id });

                return patient is null 
                    ? throw new NotFoundException($"Patient profile with id = {id} doesn't exist.") 
                    : patient;
            }
        }

        public async Task<PatientEntity> GetMatch(GetMatchedPatientQuery request)
        {
            var query = """
                            SELECT FirstName, LastName, MiddleName, DateOfBirth,
                                (
                                    (case when FirstName = @FirstName then 5 else 0 end) +
                                    (case when LastName = @LastName then 5 else 0 end) +
                                    (case when MiddleName = @MiddleName then 5 else 0 end) +
                                    (case when DateOfBirth = @DateOfBirth then 3 else 0 end)
                                ) AS Weight
                            FROM Patients
                            WHERE IsLinkedToAccount = FALSE AND Weight >= 13
                            ORDER BY Weight DESC;
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", request.Firstname, DbType.String);
            parameters.Add("LastName", request.LastName, DbType.String);
            parameters.Add("MiddleName", request.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", request.DateOfBirth, DbType.Date);

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PatientEntity>(query, parameters);
            }
        }

        //    public Task<int> DeleteAsync(Guid id)
        //    {

        //    }

        //    public Task<IEnumerable<PatientEntity>> GetAllAsync()
        //    {

        //    }

        //    public Task<(IEnumerable<PatientEntity> data, int totalCount)> GetByPageAsync(PagedParameters parameters)
        //    {

        //    }

        //    public Task<int> UpdateAsync(PatientEntity entity)
        //    {

    }
}
