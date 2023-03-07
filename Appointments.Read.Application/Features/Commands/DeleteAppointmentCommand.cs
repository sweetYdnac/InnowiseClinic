using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
{
    public class DeleteAppointmentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public DeleteAppointmentCommandHandler(IAppointmentsRepository appointmentRepository) =>
            _appointmentsRepository = appointmentRepository;

        public async Task<int> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.DeleteByIdAsync(request.Id);
        }
    }
}
