using Appointments.Read.Application.Features.Commands;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class CreateAppointmentConsumer : IConsumer<CreateAppointmentMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateAppointmentConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<CreateAppointmentMessage> context)
        {
            await _mediator.Send(_mapper.Map<CreateAppointmentCommand>(context.Message));
        }
    }
}
