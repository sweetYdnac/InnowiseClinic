using Dapper;
using Profiles.Application.Features.Doctor.Queries;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Shared.Models.Response.Profiles.Doctor;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class DoctorInformationRepository : IDoctorInformationRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorInformationRepository(ProfilesDbContext db) => _db = db;

        public async Task<DoctorInformationResponse> GetByIdAsync(Guid id)
        {
            var query = """
                        SELECT CONCAT(FirstName,' ', LastName, ' ', MiddleName) AS FullName,
                                SpecializationName,
                                OfficeAddress,
                                DATEDIFF(YEAR, CareerStartYear, GETDATE()) + 1 AS Experience
                        FROM Doctors
                        JOIN DoctorsSummary On Doctors.Id = DoctorsSummary.Id
                        WHERE Doctors.Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<DoctorInformationResponse>(query, new { id });
            }
        }

        public async Task<(IEnumerable<DoctorInformationResponse> doctors, int totalCount)> GetDoctors(GetDoctorsInformationQuery request)
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
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("FullName", $"%{request.FullName}%", DbType.String);
            parameters.Add("OfficeId", $"%{request.OfficeId}%", DbType.String);
            parameters.Add("SpecializationId", $"%{request.SpecializationId}%", DbType.String);
            parameters.Add("Offset", request.PageSize * (request.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", request.PageSize, DbType.Int32);

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
    }
}
