using Dapper;
using Offices.Business.Interfaces.Repositories;
using Offices.Data.Contexts;
using Offices.Data.DTOs;
using Serilog;
using Shared.Models.Response;
using Shared.Models.Response.Offices;
using System.Data;

namespace Offices.Business.Implementations.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficesDbContext _db;

        public OfficeRepository(OfficesDbContext db) => _db = db;

        public async Task<int> ChangeStatusAsync(ChangeOfficeStatusDTO dto)
        {
            var query = """
                            UPDATE "Offices"
                            SET "IsActive" = @IsActive
                            WHERE "Id" = @Id;
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("IsActive", dto.IsActive, DbType.Boolean);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Status201Response> CreateAsync(CreateOfficeDTO dto)
        {
            var query = """
                            INSERT INTO "Offices"
                            VALUES
                            (@Id, @Address, @RegistryPhoneNumber, @PhotoId, @IsActive)
                        """;

            var id = Guid.NewGuid();
            var address = $"{dto.City}, {dto.Street}, {dto.HouseNumber}, {dto.OfficeNumber}";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Address", address, DbType.String);
            parameters.Add("RegistryPhoneNumber", dto.RegistryPhoneNumber, DbType.String);
            parameters.Add("PhotoId", dto.PhotoId, DbType.Guid);
            parameters.Add("IsActive", dto.IsActive, DbType.Boolean);

            using (var connection = _db.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);

                if (result == 0)
                {
                    Log.Information("Office wasn't created; {@dto}", dto);
                    return null;
                }
                else
                {
                    return new Status201Response { Id = id };
                }
            }
        }

        public async Task<OfficeResponse> GetByIdAsync(Guid id)
        {
            var query = """
                            SELECT "PhotoId", "Address", "IsActive", "RegistryPhoneNumber"
                            FROM "Offices"
                            WHERE "Id" = @Id;
                        """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<OfficeResponse>(query, new { Id = id });
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdateOfficeDTO dto)
        {
            var query = """
                            UPDATE "Offices"
                            SET "Address" = @Address,
                                "RegistryPhoneNumber" = @RegistryPhoneNumber,
                                "IsActive" = @IsActive
                            WHERE "Id" = @Id;
                        """;

            var address = $"{dto.City}, {dto.Street}, {dto.HouseNumber}, {dto.OfficeNumber}";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Address", address, DbType.String);
            parameters.Add("RegistryPhoneNumber", dto.RegistryPhoneNumber, DbType.String);
            parameters.Add("IsActive", dto.IsActive, DbType.Boolean);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
