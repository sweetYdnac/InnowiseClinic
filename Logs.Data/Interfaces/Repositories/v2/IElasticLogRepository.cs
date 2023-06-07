using Logs.Data.DTOs;
using Logs.Data.Entities;
using MongoDB.Bson;
using Shared.Models;

namespace Logs.Data.Interfaces.Repositories.v2
{
    public interface IElasticLogRepository
    {
        Task<Log> GetByIdAsync(ObjectId id);
        Task<PagedResult<Log>> GetPagedAsync(GetLogsDTO filters);
        Task AddAsync(Log entity);
    }
}
