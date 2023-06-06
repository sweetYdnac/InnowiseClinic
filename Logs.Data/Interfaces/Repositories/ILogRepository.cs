using Logs.Data.DTOs;
using Logs.Data.Entities;
using MongoDB.Bson;
using Shared.Models;

namespace Logs.Data.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<Log> GetByIdAsync(ObjectId id);
        Task<PagedResult<Log>> GetPagedAsync(GetLogsDTO filters);
        Task AddAsync(Log entity);
        Task UpdateAsync(ObjectId id, UpdateLogDTO dto);
        Task RemoveAsync(ObjectId id);
    }
}
