using Logs.Data.Contexts;
using Logs.Data.DTOs;
using Logs.Data.Entities;
using Logs.Data.Interfaces.Repositories.v1;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shared.Models;

namespace Logs.Data.Implementations.Repositories.v1
{
    public class MongoDbLogRepository : IMongoDbLogRepository
    {
        private readonly LogsDbContext _db;

        public MongoDbLogRepository(LogsDbContext db) => _db = db;

        public async Task<Log> GetByIdAsync(ObjectId id) =>
             await _db.Logs
                .AsQueryable()
                .FirstOrDefaultAsync(l => l.Id.Equals(id));

        public async Task<PagedResult<Log>> GetPagedAsync(GetLogsDTO filters)
        {
            var baseFilter = Builders<Log>.Filter;

            ; var filter = Builders<Log>.Filter.Empty;

            if (filters.Code is not null)
            {
                filter &= baseFilter.Eq(l => l.Code, filters.Code);
            }

            if (filters.Date is not null)
            {
                filter &= baseFilter.Eq(l => l.DateTime, filters.Date.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.ApiName))
            {
                filter &= baseFilter.Regex(l => l.ApiName, new BsonRegularExpression(filters.ApiName, "i"));
            }

            var totalCount = await _db.Logs.Find(filter).CountDocumentsAsync();

            if (totalCount == 0)
            {
                return new PagedResult<Log>
                {
                    Items = Enumerable.Empty<Log>(),
                    TotalCount = (int)totalCount,
                };
            }

            var items = await _db.Logs
                .Find(filter)
                .Skip((filters.CurrentPage - 1) * filters.PageSize)
                .Limit(filters.PageSize)
                .ToListAsync();

            return new PagedResult<Log>
            {
                Items = items,
                TotalCount = (int)totalCount,
            };
        }

        public async Task AddAsync(Log entity) => await _db.Logs.InsertOneAsync(entity);

        public async Task UpdateAsync(ObjectId id, UpdateLogDTO dto)
        {
            var filter = Builders<Log>.Filter.Eq(l => l.Id, id);
            var update = Builders<Log>.Update
                .Set(l => l.DateTime, DateTime.Today)
                .Set(l => l.ApiName, dto.ApiName)
                .Set(l => l.Route, dto.Route)
                .Set(l => l.Code, dto.Code)
                .Set(l => l.Message, dto.Message)
                .Set(l => l.Details, dto.Details);

            var result = await _db.Logs.UpdateOneAsync(filter, update);
        }

        public async Task RemoveAsync(ObjectId id) => await _db.Logs.DeleteOneAsync(l => l.Id.Equals(id));
    }
}
