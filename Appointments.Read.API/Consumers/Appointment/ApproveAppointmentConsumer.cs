using Appointments.Read.Application.Features.Commands.Appointments;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers.Appointment
{
    public class ApproveAppointmentConsumer : IConsumer<ApproveAppointmentMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ApproveAppointmentConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<ApproveAppointmentMessage> context)
        {
            await _mediator.Send(_mapper.Map<ApproveAppointmentCommand>(context.Message));
        }
    }
}
