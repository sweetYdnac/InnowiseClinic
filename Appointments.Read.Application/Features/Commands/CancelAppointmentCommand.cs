using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
{
    public class CancelAppointmentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public DeleteAppointmentCommandHandler(IAppointmentsRepository appointmentRepository) =>
            _appointmentsRepository = appointmentRepository;

        public async Task<int> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.DeleteByIdAsync(request.Id);
        }
    }
}
