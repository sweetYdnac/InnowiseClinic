using Appointments.Read.Application.Features.Commands.Appointments;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class CancelAppointmentConsumer : IConsumer<CancelAppointmentMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CancelAppointmentConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<CancelAppointmentMessage> context)
        {
            await _mediator.Send(_mapper.Map<CancelAppointmentCommand>(context.Message));
        }
    }
}
