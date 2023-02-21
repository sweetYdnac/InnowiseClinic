using Dapper;
using Profiles.Data.Contexts;
using Profiles.Data.Interfaces.Repositories;
using Shared.Core.Enums;
using System.Data;

namespace Profiles.Data.Implementations.Repositories
{
    public class ProfilesRepository : IProfilesRepository
    {
        private readonly ProfilesDbContext _db;

        public ProfilesRepository(ProfilesDbContext db) => _db = db;

        public async Task<int> SetInactiveStatusToPersonalAsync(Guid officeId)
        {
            var query = """
                            UPDATE DoctorsSummary
                            SET Status = @Status
                            FROM DoctorsSummary
                                JOIN Doctors ON Doctors.Id = DoctorsSummary.Id
                            WHERE OfficeId = @OfficeId

                            UPDATE ReceptionistsSummary
                            SET Status = @Status
                            FROM ReceptionistsSummary
                                JOIN Receptionists ON Receptionists.Id = ReceptionistsSummary.Id
                            WHERE OfficeId = @OfficeId
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("OfficeId", officeId, DbType.Guid);
            parameters.Add("Status", AccountStatuses.Inactive, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> UpdateOfficeAddressAsync(Guid officeId, string officeAddress)
        {
            var query = """
                            UPDATE DoctorsSummary
                            SET OfficeAddress = @OfficeAddress
                            FROM DoctorsSummary
                                JOIN Doctors ON Doctors.Id = DoctorsSummary.Id
                            WHERE OfficeId = @OfficeId

                            UPDATE ReceptionistsSummary
                            SET OfficeAddress = @OfficeAddress
                            FROM ReceptionistsSummary
                                JOIN Receptionists ON Receptionists.Id = ReceptionistsSummary.Id
                            WHERE OfficeId = @OfficeId
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("OfficeId", officeId, DbType.Guid);
            parameters.Add("OfficeAddress", officeAddress, DbType.String);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
