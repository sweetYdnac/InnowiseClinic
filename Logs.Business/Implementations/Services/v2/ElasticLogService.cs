using AutoMapper;
using Logs.Business.Interfaces.Services.v2;
using Logs.Data.DTOs;
using Logs.Data.Entities;
using Logs.Data.Interfaces.Repositories.v2;
using MongoDB.Bson;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Logs;

namespace Logs.Business.Implementations.Services.v2
{
    public class ElasticLogService : IElasticLogService
    {
        private readonly IElasticLogRepository _logRepository;
        private readonly IMapper _mapper;

        public ElasticLogService(IElasticLogRepository logRepository, IMapper mapper) => (_logRepository, _mapper) = (logRepository, mapper);

        public async Task<LogResponse> GetByIdAsync(ObjectId id)
        {
            var entity = await _logRepository.GetByIdAsync(id);

            return entity is null
                ? throw new NotFoundException($"Log with id = {id} does not exist.")
                : _mapper.Map<LogResponse>(entity);
        }

        public async Task<PagedResponse<LogResponse>> GetPagedAsync(GetLogsDTO filters)
        {
            var response = await _logRepository.GetPagedAsync(filters);

            return new PagedResponse<LogResponse>(
                _mapper.Map<IEnumerable<LogResponse>>(response.Items),
                filters.CurrentPage,
                filters.PageSize,
                response.TotalCount);
        }

        public async Task CreateAsync(CreateLogDTO dto) => await _logRepository.AddAsync(_mapper.Map<Log>(dto));

    }
}
