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
            var result = await _client.SearchAsync<Log>(s => s
                .Query(q => q
                    .Ids(ids => ids
                        .Values(id.ToString())
                    )
                )
            );

            return result.Documents.FirstOrDefault();
        }

        public async Task<PagedResult<Log>> GetPagedAsync(GetLogsDTO filters)
        {
            var count = new CountDescriptor<Log>();
            var search = new SearchDescriptor<Log>();

            if (filters.Date.HasValue)
            {
                var fromDate = filters.Date.Value.Date.ToUniversalTime();
                var toDate = fromDate.AddDays(1);

                Func<QueryContainerDescriptor<Log>, QueryContainer> filter = q => q
                    .DateRange(dr => dr
                        .Field(f => f.DateTime)
                        .GreaterThanOrEquals(fromDate)
                        .LessThan(toDate));

                search = search.Query(filter);
                count = count.Query(filter);
            }

            if (!string.IsNullOrWhiteSpace(filters.ApiName))
            {
                Func<QueryContainerDescriptor<Log>, QueryContainer> filter = q => q
                    .Wildcard(w => w
                        .Field(f => f.ApiName)
                        .Value($"*{filters.ApiName.ToLowerInvariant()}*")
                    );

                search = search.Query(filter);
                count = count.Query(filter);
            }

            if (filters.Code.HasValue)
            {
                Func<QueryContainerDescriptor<Log>, QueryContainer> filter = q => q
                    .Term(t => t
                        .Field(f => f.Code)
                        .Value((int)filters.Code.Value));

                search = search.Query(filter);
                count = count.Query(filter);
            }

            var countResponse = await _client.CountAsync(count);

            if (countResponse.Count == 0)
            {
                return new PagedResult<Log>
                {
                    Items = Enumerable.Empty<Log>(),
                    TotalCount = (int)countResponse.Count,
                };
            }

            search = search
                .Skip((filters.CurrentPage - 1) * filters.PageSize)
                .Take(filters.PageSize);

            var itemsResponse = await _client.SearchAsync<Log>(search);

            return new PagedResult<Log>
            {
                Items = itemsResponse.Documents.ToArray(),
                TotalCount = (int)countResponse.Count,
            };
        }

        public async Task AddAsync(Log entity) =>
            await _client.IndexDocumentAsync(entity);
    }
}
