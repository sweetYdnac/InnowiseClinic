﻿using Logs.Data.DTOs;
using MongoDB.Bson;
using Shared.Models.Response;
using Shared.Models.Response.Logs;

namespace Logs.Business.Interfaces.Services
{
    public interface ILogService
    {
        Task<LogResponse> GetByIdAsync(ObjectId id);
        Task<PagedResponse<LogResponse>> GetPagedAsync(GetLogsDTO filters);
        Task CreateAsync(CreateLogDTO dto);
        Task UpdateAsync(ObjectId id, UpdateLogDTO dto);
        Task RemoveAsync(ObjectId id);
    }
}
