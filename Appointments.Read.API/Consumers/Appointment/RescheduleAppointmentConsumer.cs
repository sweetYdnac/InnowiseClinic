using Appointments.Read.Application.Features.Commands.Appointments;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers.Appointment
{
    public class RescheduleAppointmentConsumer : IConsumer<RescheduleAppointmentMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RescheduleAppointmentConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<RescheduleAppointmentMessage> context)
        {
            await _mediator.Send(_mapper.Map<RescheduleAppointmentCommand>(context.Message));
        }
    }
}
