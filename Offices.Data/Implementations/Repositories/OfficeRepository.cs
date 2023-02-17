using Dapper;
using Offices.Data.Contexts;
using Offices.Data.DTOs;
using Offices.Data.Interfaces.Repositories;
using Shared.Models;
using Shared.Models.Extensions;
using Shared.Models.Response.Offices;
using System.Data;

namespace Offices.Data.Implementations.Repositories
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

        public async Task<int> CreateAsync(CreateOfficeDTO dto)
        {
            var query = """
                            INSERT INTO "Offices"
                            VALUES
                            (@Id, @Address, @RegistryPhoneNumber, @PhotoId, @IsActive)
                        """;

            var address = $"{dto.City}, {dto.Street}, {dto.HouseNumber}, {dto.OfficeNumber}";

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("Address", address, DbType.String);
            parameters.Add("RegistryPhoneNumber", dto.RegistryPhoneNumber, DbType.String);
            parameters.Add("PhotoId", dto.PhotoId, DbType.Guid);
            parameters.Add("IsActive", dto.IsActive, DbType.Boolean);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
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

        public async Task<PagedResult<OfficeInformationResponse>> GetPagedOfficesAsync(GetPagedOfficesDTO dto)
        {
            var query = """
                            SELECT "Id", "Address", "RegistryPhoneNumber", "IsActive"
                            FROM "Offices"
                            ORDER BY "Id"
                                OFFSET @Offset ROWS
                                FETCH FIRST @PageSize ROWS ONLY;

                            SELECT COUNT(*)
                            FROM "Offices"
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Offset", dto.PageSize * (dto.CurrentPage - 1), DbType.Int32);
            parameters.Add("PageSize", dto.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryPagedResultAsync<OfficeInformationResponse>(query, parameters);
            }
        }
    }
}
