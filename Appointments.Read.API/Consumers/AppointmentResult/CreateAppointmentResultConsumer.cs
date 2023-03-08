using Appointments.Read.Application.Features.Commands.AppointmentsResults;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers.AppointmentResult
{
    public class CreateAppointmentResultConsumer : IConsumer<CreateAppointmentResultMessage>
    {
        private readonly IMediator _meditor;
        private readonly IMapper _mapper;

        public CreateAppointmentResultConsumer(IMediator meditor, IMapper mapper) =>
            (_meditor, _mapper) = (meditor, mapper);

        public async Task Consume(ConsumeContext<CreateAppointmentResultMessage> context)
        {
            await _meditor.Send(_mapper.Map<CreateAppointmentResultCommand>(context.Message));
        }
    }
}
