using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.Doctor;
using Shared.Models.Response.Profiles.Doctor;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorsRepository(ProfilesDbContext db) => _db = db;

        public async Task<DoctorResponse> GetByIdAsync(Guid id)
        {
            var query = """
                        SELECT FirstName, LastName, MiddleName, DateOfBirth, SpecializationName, OfficeAddress, CareerStartYear
                        FROM Doctors
                        JOIN DoctorsSummary On Doctors.Id = DoctorsSummary.Id
                        WHERE Doctors.Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<DoctorResponse>(query, new { id });
            }
        }

        public async Task<(IEnumerable<DoctorInformationResponse> doctors, int totalCount)> GetDoctors(GetDoctorsDTO dto)
        {
            var query = """
                            SELECT CONCAT(FirstName,' ', LastName, ' ', MiddleName) AS FullName,
                                   SpecializationName,
                                   OfficeAddress,
                                   DATEDIFF(YEAR, CareerStartYear, GETDATE()) + 1 AS Experience
                            FROM Doctors
                            JOIN DoctorsSummary On Doctors.Id = DoctorsSummary.Id
                            WHERE (FirstName LIKE @FullName OR 
                                  LastName LIKE @FullName OR 
                                  MiddleName LIKE @FullName) AND
                                  SpecializationId LIKE @SpecializationId AND
                                  OfficeId LIKE @OfficeId
                            ORDER BY Doctors.Id
                                OFFSET @Offset ROWS
                                FETCH FIRST @PageSize ROWS ONLY;
                        
                            SELECT COUNT(*)
                            FROM Doctors
                            JOIN DoctorsSummary On Doctors.Id = DoctorsSummary.Id
                            WHERE (FirstName LIKE @FullName OR 
                                  LastName LIKE @FullName OR 
                                  MiddleName LIKE @FullName) AND
                                  SpecializationId LIKE @SpecializationId AND
                                  OfficeId LIKE @OfficeId
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("FullName", $"%{dto.FullName}%", DbType.String);
            parameters.Add("OfficeId", $"%{dto.OfficeId}%", DbType.String);
            parameters.Add("SpecializationId", $"%{dto.SpecializationId}%", DbType.String);
            parameters.Add("Offset", dto.PageSize * (dto.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", dto.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, parameters))
                {
                    var doctors = await multi.ReadAsync<DoctorInformationResponse>();
                    var totalCount = await multi.ReadFirstAsync<int>();

                    return (doctors, totalCount);
                }
            }
        }

        public async Task<int> AddAsync(CreateDoctorDTO dto)
        {
            var query = """
                            INSERT Doctors
                            VALUES
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @DateOfBirth, @SpecializationId, @OfficeId, @CareerStartYear);
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("AccountId", dto.AccountId, DbType.Guid);
            parameters.Add("DateOfBirth", dto.DateOfBirth, DbType.Date);
            parameters.Add("SpecializationId", dto.SpecializationId, DbType.Guid);
            parameters.Add("OfficeId", dto.OfficeId, DbType.Guid);
            parameters.Add("CareerStartYear", dto.CareerStartYear, DbType.Date);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
