using Dapper;
using Profiles.Application.Features.Doctor.Queries;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;
using Profiles.Persistence.Contexts;
using Shared.Models.Response.Profiles.Doctor;
using System.Data;

namespace Profiles.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorRepository(ProfilesDbContext db) => _db = db;

        public async Task<(IEnumerable<DoctorPreviewResponse> doctors, int totalCount)> GetDoctors(GetDoctorsQuery request)
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
                    var doctors = await multi.ReadAsync<DoctorPreviewResponse>();
                    var totalCount = await multi.ReadFirstAsync<int>();

                    return (doctors, totalCount);
                }
            }
        }

        public Task<Guid?> AddAsync(DoctorEntity entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<DoctorEntity> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task UpdateAsync(DoctorEntity entity) => throw new NotImplementedException();
    }
}
