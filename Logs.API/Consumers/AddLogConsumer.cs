using AutoMapper;
using Logs.Business.Interfaces.Services.v1;
using Logs.Business.Interfaces.Services.v2;
using Logs.Data.DTOs;
using MassTransit;
using Shared.Messages;

namespace Logs.API.Consumers
{
    public class AddLogConsumer : IConsumer<AddLogMessage>
    {
        private readonly IMongoDbLogService _mongoDbLogService;
        private readonly IElasticLogService _elasticLogService;
        private readonly IMapper _mapper;

        public AddLogConsumer(IMongoDbLogService mongoDbLogService, IElasticLogService elasticLogService, IMapper mapper) =>
            (_mongoDbLogService, _elasticLogService, _mapper) = (mongoDbLogService, elasticLogService, mapper);

        public async Task Consume(ConsumeContext<AddLogMessage> context)
        {
            var dto = _mapper.Map<CreateLogDTO>(context.Message);

            await _mongoDbLogService.CreateAsync(dto);
            await _elasticLogService.CreateAsync(dto);
        }
    }
}
