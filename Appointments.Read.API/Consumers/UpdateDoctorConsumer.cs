using Appointments.Read.Application.Features.Commands;
using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Appointments.Read.API.Consumers
{
    public class UpdateDoctorConsumer : IConsumer<UpdateDoctorMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateDoctorConsumer(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public async Task Consume(ConsumeContext<UpdateDoctorMessage> context)
        {
            await _mediator.Send(_mapper.Map<UpdateDoctorCommand>(context.Message));
        }
    }
}
