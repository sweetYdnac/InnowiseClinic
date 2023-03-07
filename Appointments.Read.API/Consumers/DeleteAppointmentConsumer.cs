using Appointments.Read.Application.Features.Commands;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class DeleteAppointmentConsumer : IConsumer<DeleteAppointmentMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteAppointmentConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<DeleteAppointmentMessage> context)
        {
            await _mediator.Send(_mapper.Map<DeleteAppointmentCommand>(context.Message));
        }
    }
}
