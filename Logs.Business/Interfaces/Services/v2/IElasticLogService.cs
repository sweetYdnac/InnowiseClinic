using Logs.Data.DTOs;
using MongoDB.Bson;
using Shared.Models.Response;
using Shared.Models.Response.LogsAPI;

namespace Logs.Business.Interfaces.Services.v2
{
    public interface IElasticLogService
    {
        Task<LogResponse> GetByIdAsync(ObjectId id);
        Task<PagedResponse<LogResponse>> GetPagedAsync(GetLogsDTO filters);
        Task CreateAsync(CreateLogDTO dto);
    }
}
