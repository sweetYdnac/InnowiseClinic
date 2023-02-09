﻿using Dapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Data.Contexts;
using Profiles.Data.DTOs.DoctorSummary;
using System.Data;

namespace Profiles.Business.Implementations.Repositories
{
    public class DoctorSummaryRepository : IDoctorSummaryRepository
    {
        private readonly ProfilesDbContext _db;

        public DoctorSummaryRepository(ProfilesDbContext db) => _db = db;

        public async Task<int> AddAsync(DoctorSummaryDTO dto)
        {
            var query = """
                            INSERT DoctorsSummary
                            VALUES
                            (@Id, @SpecializationName, @OfficeAddress);
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, DbType.Guid);
            parameters.Add("SpecializationName", dto.SpecializationName, DbType.String);
            parameters.Add("OfficeAddress", dto.OfficeAddress, DbType.String);

            using (var connection = _db.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
