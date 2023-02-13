using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.Patient;
using Shared.Models.Response.Profiles.Patient;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ProfilesDbContext _db;

        public PatientsRepository(ProfilesDbContext db) => _db = db;

        public async Task<PatientResponse> GetByIdAsync(Guid id)
        {
            var query = """
                            SELECT FirstName, LastName, MiddleName, DateOfBirth, PhotoId
                            FROM Patients
                            WHERE Id = @id;
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PatientResponse>(query, new { id });
            }
        }

        public async Task<(IEnumerable<PatientInformationResponse> patients, int totalCount)> GetPatients(GetPatientsDTO dto)
        {
            var query = """
                            SELECT Id,
                                   CONCAT(FirstName,' ', LastName, ' ', MiddleName) AS FullName,
                                   PhoneNumber
                            FROM Patients
                            WHERE FirstName LIKE @FullName OR 
                                  LastName LIKE @FullName OR 
                                  MiddleName LIKE @FullName
                            ORDER BY Id
                                OFFSET @Offset ROWS
                                FETCH FIRST @PageSize ROWS ONLY;

                            SELECT COUNT(*)
                            FROM Patients
                            WHERE FirstName LIKE @FullName OR 
                                      LastName LIKE @FullName OR 
                                      MiddleName LIKE @FullName
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("FullName", $"%{dto.FullName}%", DbType.String);
            parameters.Add("Offset", dto.PageSize * (dto.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", dto.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, parameters))
                {
                    var offices = await multi.ReadAsync<PatientInformationResponse>();
                    var count = await multi.ReadFirstAsync<int>();

                    return (offices, count);
                }
            }
        }

        public async Task<int> AddAsync(CreatePatientDTO dto)
        {
            var query = """
                            INSERT Patients
                            VALUES
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @DateOfBirth, @IsLinkedToAccount, @PhotoId, @PhoneNumber)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("AccountId", dto.AccountId, DbType.Guid);
            parameters.Add("PhotoId", dto.PhotoId, DbType.Guid);
            parameters.Add("PhoneNumber", dto.PhoneNumber, DbType.String);
            parameters.Add("DateOfBirth", dto.DateOfBirth, DbType.Date);
            parameters.Add("IsLinkedToAccount", dto.IsLinkedToAccount, DbType.Boolean);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<PatientResponse> GetMatchAsync(GetMatchedPatientDTO dto)
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
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", dto.DateOfBirth, DbType.Date);

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PatientResponse>(query, parameters);
            }
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var query = """
                            DELETE Patients
                            WHERE Id = @id;
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdatePatientDTO dto)
        {
            var query = """
                            UPDATE Patients
                            SET FirstName = @FirstName,
                                LastName = @LastName,
                                MiddleName = @MiddleName,
                                DateOfBirth = @DateOfBirth,
                                PhoneNumber = @PhoneNumber
                            WHERE Id = @Id;
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", dto.DateOfBirth, DbType.Date);
            parameters.Add("PhoneNumber", dto.PhoneNumber, DbType.String);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> LinkToAccount(Guid id, Guid accountId)
        {
            var query = """
                            UPDATE Patients
                            SET AccountId = @AccountId,
                                IsLinkedToAccount = 1
                            WHERE Id = @Id;
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("AccountId", accountId, DbType.Guid);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
