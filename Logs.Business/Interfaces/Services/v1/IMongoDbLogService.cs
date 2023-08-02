using Logs.Data.DTOs;
using MongoDB.Bson;
using Shared.Models.Response;
using Shared.Models.Response.LogsAPI;

namespace Logs.Business.Interfaces.Services.v1
{
    public interface IMongoDbLogService
    {
        Task<LogResponse> GetByIdAsync(ObjectId id);
        Task<PagedResponse<LogResponse>> GetPagedAsync(GetLogsDTO filters);
        Task CreateAsync(CreateLogDTO dto);
        Task UpdateAsync(ObjectId id, UpdateLogDTO dto);
        Task RemoveAsync(ObjectId id);
    }
}
