using Logs.Data.DTOs;
using Logs.Data.Entities;
using MongoDB.Bson;
using Shared.Models;

namespace Logs.Data.Interfaces.Repositories.v1
{
    public interface IMongoDbLogRepository
    {
        Task<Log> GetByIdAsync(ObjectId id);
        Task<PagedResult<Log>> GetPagedAsync(GetLogsDTO filters);
        Task AddAsync(Log entity);
        Task UpdateAsync(ObjectId id, UpdateLogDTO dto);
        Task RemoveAsync(ObjectId id);
    }
}
