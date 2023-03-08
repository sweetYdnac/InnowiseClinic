using Appointments.Read.Application.Features.Commands.Appointments;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class UpdatePatientConsumer : IConsumer<UpdatePatientMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdatePatientConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<UpdatePatientMessage> context)
        {
            await _mediator.Send(_mapper.Map<UpdatePatientCommand>(context.Message));
        }
    }
}
