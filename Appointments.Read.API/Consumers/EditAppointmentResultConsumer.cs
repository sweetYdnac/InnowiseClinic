using Appointments.Read.Application.Features.Commands.AppointmentsResults;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class EditAppointmentResultConsumer : IConsumer<EditAppointmentResultMessage>
    {
        private readonly IMediator _meditor;
        private readonly IMapper _mapper;

        public EditAppointmentResultConsumer(IMediator meditor, IMapper mapper) =>
            (_meditor, _mapper) = (meditor, mapper);

        public async Task Consume(ConsumeContext<EditAppointmentResultMessage> context)
        {
            await _meditor.Send(_mapper.Map<EditAppointmentResultCommand>(context.Message));
        }
    }
}
