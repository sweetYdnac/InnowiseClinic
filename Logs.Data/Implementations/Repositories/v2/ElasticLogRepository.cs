using Logs.Data.DTOs;
using Logs.Data.Entities;
using Logs.Data.Interfaces.Repositories.v2;
using MongoDB.Bson;
using MongoDB.Driver;
using Nest;
using Shared.Models;

namespace Logs.Data.Implementations.Repositories.v2
{
    public class ElasticLogRepository : IElasticLogRepository
    {
        private readonly IElasticClient _client;

        public ElasticLogRepository(IElasticClient client) => _client = client;

        public async Task<Log> GetByIdAsync(ObjectId id)
        {
            var result = await _client.SearchAsync<Log>(s => s.Query(l => l.QueryString(d => d.Query(id.ToString()))));
            return result.Documents.FirstOrDefault();
        }

        public async Task<PagedResult<Log>> GetPagedAsync(GetLogsDTO filters)
        {
            //var baseFilter = Builders<Log>.Filter;

            //; var filter = Builders<Log>.Filter.Empty;

            //if (filters.Code is not null)
            //{
            //    filter &= baseFilter.Eq(l => l.Code, filters.Code);
            //}

            //if (filters.Date is not null)
            //{
            //    filter &= baseFilter.Eq(l => l.DateTime, filters.Date.Value);
            //}

            //if (!string.IsNullOrWhiteSpace(filters.ApiName))
            //{
            //    filter &= baseFilter.Regex(l => l.ApiName, new BsonRegularExpression(filters.ApiName, "i"));
            //}

            //var totalCount = await _db.Logs.Find(filter).CountDocumentsAsync();

            //if (totalCount == 0)
            //{
            //    return new PagedResult<Log>
            //    {
            //        Items = Enumerable.Empty<Log>(),
            //        TotalCount = (int)totalCount,
            //    };
            //}

            //var items = await _db.Logs
            //    .Find(filter)
            //    .Skip((filters.CurrentPage - 1) * filters.PageSize)
            //    .Limit(filters.PageSize)
            //    .ToListAsync();

            //return new PagedResult<Log>
            //{
            //    Items = items,
            //    TotalCount = (int)totalCount,
            //};

            throw new NotImplementedException();
        }

        public async Task AddAsync(Log entity)
        {
            await _client.IndexDocumentAsync(entity);
        }

    }
}
