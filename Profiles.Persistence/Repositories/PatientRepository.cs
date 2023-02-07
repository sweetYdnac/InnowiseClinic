using Dapper;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Features.Patient.Queries;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;
using Profiles.Persistence.Contexts;
using Serilog;
using Shared.Exceptions;
using System.Data;
using static Dapper.SqlMapper;

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
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @DateOfBirth, @IsLinkedToAccount)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("MiddleName", entity.MiddleName, DbType.String);
            parameters.Add("AccountId", entity.AccountId, DbType.Guid);
            parameters.Add("DateOfBirth", entity.DateOfBirth, DbType.Date);
            parameters.Add("IsLinkedToAccount", entity.IsLinkedToAccount, DbType.Boolean);

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

        public async Task DeleteAsync(Guid id)
        {
            var query = """
                            DELETE Patients
                            WHERE Id = @id;
                        """;

            using (var connection = _db.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { id });

                if (result == 0)
                {
                    Log.Information("Entity with {id} wasn't remove", id);
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

        public async Task<PatientEntity> GetMatchAsync(GetMatchedPatientQuery request)
        {
            var query = """
                            WITH Response AS(
                            SELECT FirstName, LastName, MiddleName, DateOfBirth,
                        	    (
                        		    (case when FirstName = @FirstName then 5 else 0 end) +
                        		    (case when LastName = @LastName then 5 else 0 end) +
                        		    (case when MiddleName = @MiddleName then 5 else 0 end) +
                        		    (case when DateOfBirth = @DateOfBirth then 3 else 0 end)
                        	    ) AS Weight
                        	FROM Patients
                        	WHERE IsLinkedToAccount = 0)

                            SELECT FirstName, LastName, MiddleName, DateOfBirth
                            FROM Response
                            WHERE Weight >= 13
                            ORDER BY Weight DESC
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

        public async Task<(IEnumerable<PatientEntity> patients, int totalCount)> GetPatients(GetPatientsQuery request)
        {
            var query = """
                            SELECT FirstName, LastName, MiddleName
                            FROM Patients
                            WHERE FirstName LIKE @FullName OR 
                                  LastName LIKE @FullName OR 
                                  MiddleName LIKE @FullName
                            ORDER BY Id
                                OFFSET @Offset ROWS
                                FETCH FIRST @PageSize ROWS ONLY;

                            SELECT COUNT(*)
                            FROM Patients
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("FullName", $"%{request.FullName}%", DbType.String);
            parameters.Add("Offset", request.PageSize * (request.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", request.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, parameters))
                {
                    var offices = await multi.ReadAsync<PatientEntity>();
                    var count = await multi.ReadFirstAsync<int>();

                    return (offices, count);
                }
            }
        }

        public async Task LinkToAccount(LinkToAccountCommand request)
        {
            var query = """
                            UPDATE Patients
                            SET AccountId = @accountId,
                                IsLinkedToAccount = 1
                            WHERE Id = @id;
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("id", request.Id, DbType.Guid);
            parameters.Add("AccountId", request.AccountId, DbType.Guid);

            using (var connection = _db.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);

                if (result == 0)
                {
                    Log.Information("Patient wasn't linked to account. {@request}", request);
                }
            }
        }

        public async Task UpdateAsync(PatientEntity entity)
        {
            var query = """
                            UPDATE Patients
                            SET FirstName = @firstName,
                                LastName = @lastName,
                                MiddleName = @middleName,
                                DateOfBirth = @dateOfBirth
                            WHERE Id = @id;
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("firstName", entity.FirstName, DbType.String);
            parameters.Add("lastName", entity.LastName, DbType.String);
            parameters.Add("middleName", entity.MiddleName, DbType.String);
            parameters.Add("dateOfBirth", entity.DateOfBirth, DbType.Date);
            parameters.Add("id", entity.Id, DbType.Guid);

            using (var connection = _db.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);

                if (result == 0)
                {
                    Log.Information("Patient wasn't update. {@entity}", entity);
                }
            }
        }
    }
}
