using Dapper;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.Doctor;
using Profiles.Data.Interfaces.Repositories;
using Shared.Core.Enums;
using Shared.Models.Response.Profiles.Doctor;
using System.Data;

namespace Profiles.Data.Implementations.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorsRepository(ProfilesDbContext db) => _db = db;

        public async Task<DoctorResponse> GetByIdAsync(Guid id)
        {
            var query = """
                            SELECT FirstName, LastName, MiddleName, DateOfBirth, SpecializationName, OfficeAddress, CareerStartYear, PhotoId, Status
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
            var statusFilter = dto.OnlyAtWork
                ? $"AND Status = {(int)AccountStatuses.AtWork}"
                : string.Empty;

            var query = $"""
                            SELECT Doctors.Id,
                                   CONCAT(FirstName,' ', LastName, ' ', MiddleName) AS FullName,
                                   SpecializationName,
                                   OfficeAddress,
                                   DATEDIFF(YEAR, CareerStartYear, GETDATE()) + 1 AS Experience,
                                   Status,
                                   PhotoId
                            FROM Doctors
                            JOIN DoctorsSummary On Doctors.Id = DoctorsSummary.Id
                            WHERE (FirstName LIKE @FullName OR
                                  LastName LIKE @FullName OR
                                  MiddleName LIKE @FullName) AND
                                  SpecializationId LIKE @SpecializationId AND
                                  OfficeId LIKE @OfficeId
                                  {statusFilter}
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
                                  {statusFilter}
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
                            (@Id, @FirstName, @LastName, @MiddleName, @AccountId, @DateOfBirth, @SpecializationId, @OfficeId, @CareerStartYear, @PhotoId);
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
            parameters.Add("PhotoId", dto.PhotoId, DbType.Guid);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdateDoctorDTO dto)
        {
            var query = """
                            UPDATE Doctors
                            SET FirstName = @FirstName,
                                LastName = @LastName,
                                MiddleName = @MiddleName,
                                DateOfBirth = @DateOfBirth,
                                SpecializationId = @SpecializationId,
                                OfficeId = @OfficeId,
                                CareerStartYear = @CareerStartYear
                            WHERE Id = @Id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("FirstName", dto.FirstName, DbType.String);
            parameters.Add("LastName", dto.LastName, DbType.String);
            parameters.Add("MiddleName", dto.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", dto.DateOfBirth, DbType.Date);
            parameters.Add("SpecializationId", dto.SpecializationId, DbType.Guid);
            parameters.Add("OfficeId", dto.OfficeId, DbType.Guid);
            parameters.Add("CareerStartYear", dto.CareerStartYear, DbType.Date);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var query = """
                            DELETE Doctors
                            WHERE Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Guid> GetPhotoIdAsync(Guid id)
        {
            var query = """
                            SELECT PhotoId
                            FROM Doctors
                            WHERE Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Guid>(query, new { id });
            }
        }

        public async Task<Guid> GetAccountIdAsync(Guid id)
        {
            var query = """
                            SELECT AccountId
                            FROM Doctors
                            WHERE Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Guid>(query, new { id });
            }
        }

        public async Task<int> SetInactiveStatusAsync(Guid specializationId)
        {
            var query = """
                            UPDATE DoctorsSummary
                            SET Status = @Status
                            FROM DoctorsSummary
                                JOIN Doctors ON Doctors.Id = DoctorsSummary.Id
                            WHERE SpecializationId = @SpecializationId
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("SpecializationId", specializationId, DbType.Guid);
            parameters.Add("Status", AccountStatuses.Inactive, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> UpdateSpecializationName(Guid specializationId, string specializationName)
        {
            var query = """
                            UPDATE DoctorsSummary
                            SET SpecializationName = @SpecializationName
                            FROM DoctorsSummary
                                JOIN Doctors ON Doctors.Id = DoctorsSummary.Id
                            WHERE SpecializationId = @SpecializationId
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("SpecializationId", specializationId, DbType.Guid);
            parameters.Add("SpecializationName", specializationName, DbType.String);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }

        }
    }
}
