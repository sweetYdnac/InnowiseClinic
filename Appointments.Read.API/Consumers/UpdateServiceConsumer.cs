using Appointments.Read.Application.Features.Commands;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class UpdateServiceConsumer : IConsumer<UpdateServiceMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateServiceConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<UpdateServiceMessage> context)
        {
            await _mediator.Send(_mapper.Map<UpdateServiceCommand>(context.Message));
        }
    }
}
