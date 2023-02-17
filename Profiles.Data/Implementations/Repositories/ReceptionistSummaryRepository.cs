﻿using Dapper;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.ReceptionistSummary;
using Profiles.Data.Interfaces.Repositories;
using Shared.Core.Enums;
using System.Data;

namespace Profiles.Data.Implementations.Repositories
{
    public class ReceptionistSummaryRepository : IReceptionistSummaryRepository
    {
        private readonly ProfilesDbContext _db;

        public ReceptionistSummaryRepository(ProfilesDbContext db) => _db = db;

        public async Task AddAsync(CreateReceptionistSummaryDTO dto)
        {
            var query = """
                            INSERT ReceptionistsSummary
                            VALUES
                            (@Id, @OfficeAddress, @Status)
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);
            parameters.Add("Status", dto.Status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateAsync(Guid id, UpdateReceptionistSummaryDTO dto)
        {
            var query = """
                            UPDATE ReceptionistsSummary
                            SET OfficeAddress = @OfficeAddress,
                                Status = @Status
                            WHERE Id = @id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);
            parameters.Add("Status", dto.Status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var query = """
                            DELETE ReceptionistsSummary
                            WHERE Id = @id
                        """;

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<int> ChangeStatus(Guid id, AccountStatuses status)
        {
            var query = """
                            UPDATE ReceptionistsSummary
                            SET Status = @Status
                            WHERE Id = @Id
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Status", status, DbType.Int32);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
