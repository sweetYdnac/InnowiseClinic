using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using AutoMapper;
using MediatR;
using Shared.Messages;

namespace Appointments.Write.Application.Features.Commands.Appointments
{
    public class RescheduleAppointmentCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public string DoctorFullName { get; set; }
    }

    public class RescheduleAppointmenCommandHandler : IRequestHandler<RescheduleAppointmentCommand, Unit>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public RescheduleAppointmenCommandHandler(
            IAppointmentsRepository appointmentsRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_appointmentsRepository, _messageService, _mapper) = (appointmentsRepository, messageService, mapper);

        public async Task<Unit> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsRepository.RescheduleAppointment(request);

            if (result > 0)
            {
                await _messageService.SendRescheduleAppointmentMessageAsync(
                    _mapper.Map<RescheduleAppointmentMessage>(request));
            }

            return Unit.Value;
        }
    }
}
