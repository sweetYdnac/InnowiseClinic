using AutoMapper;
using Logs.Business.Interfaces.Services;
using Logs.Data.DTOs;
using MassTransit;
using Shared.Messages;

namespace Logs.API.Consumers
{
    public class AddLogConsumer : IConsumer<AddLogMessage>
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public AddLogConsumer(ILogService logService, IMapper mapper) => (_logService, _mapper) = (logService, mapper);

        public async Task Consume(ConsumeContext<AddLogMessage> context) =>
            await _logService.CreateAsync(_mapper.Map<CreateLogDTO>(context.Message));
    }
}
