using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using MediatR;

namespace Appointments.Write.Application.Features.Commands.Appointments
{
    public class ApproveAppointmentCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand, Unit>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMessageService _messageService;

        public ApproveAppointmentCommandHandler(
            IAppointmentsRepository appointmentRepository,
            IMessageService messageService) =>
        (_appointmentsRepository, _messageService) = (appointmentRepository, messageService);

        public async Task<Unit> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            await _appointmentsRepository.ApproveAsync(request.Id);
            await _messageService.SendApproveAppointmentMessageAsync(request.Id);

            return Unit.Value;
        }
    }
}
