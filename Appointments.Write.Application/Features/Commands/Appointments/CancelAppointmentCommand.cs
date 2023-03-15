using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using MediatR;

namespace Appointments.Write.Application.Features.Commands.Appointments
{
    public class CancelAppointmentCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Unit>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMessageService _messageService;

        public CancelAppointmentCommandHandler(
            IAppointmentsRepository appointmentRepository,
            IMessageService messageService) =>
        (_appointmentsRepository, _messageService) = (appointmentRepository, messageService);

        public async Task<Unit> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsRepository.DeleteByIdAsync(request.Id);

            if (result > 0)
            {
                await _messageService.SendDeleteAppointmentMessageAsync(request.Id);
            }

            return Unit.Value;
        }
    }
}
