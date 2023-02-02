﻿using Dapper;
using Offices.Application.DTOs;
using Offices.Application.Interfaces.Repositories;
using Offices.Domain.Entities;
using Offices.Persistence.Contexts;
using Serilog;
using System.Data;

namespace Offices.Persistence.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficesDbContext _db;

        public OfficeRepository(OfficesDbContext db) => _db = db;

        public async Task<Guid?> CreateAsync(CreateOfficeDTO dto)
        {
            var query =
                """
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
                    return id;
                }
            }
        }

        public async Task<OfficeEntity> GetByIdAsync(Guid id)
        {
            var query =
                """
                    SELECT "PhotoId", "Address", "IsActive", "RegistryPhoneNumber"
                    FROM "Offices"
                    WHERE "Id" = @Id;
                """;

            using (var connection = _db.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<OfficeEntity>(query, new { Id = id });
            }
        }

        public async Task<(IEnumerable<OfficeEntity> offices, int totalCount)> GetPagedOfficesAsync(GetPagedOfficesDTO dto)
        {
            var query =
                """
                    SELECT "Id", "Address", "RegistryPhoneNumber", "IsActive"
                    FROM "Offices"
                    ORDER BY "Id"
                        OFFSET @Offset ROWS
                        FETCH FIRST @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM "Offices"
                """;

            var parameters = new DynamicParameters();
            parameters.Add("Offset", dto.PageSize * (dto.PageNumber - 1), DbType.Int32);
            parameters.Add("PageSize", dto.PageSize, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, parameters))
                {
                    var offices = await multi.ReadAsync<OfficeEntity>();
                    var count = await multi.ReadFirstAsync<int>();

                    return (offices, count);
                }
            }
        }
    }
}
