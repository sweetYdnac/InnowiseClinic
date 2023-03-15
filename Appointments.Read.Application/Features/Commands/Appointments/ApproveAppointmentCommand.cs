using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
{
    public class ApproveAppointmentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public ApproveAppointmentCommandHandler(IAppointmentsRepository appointmentRepository) =>
            _appointmentsRepository = appointmentRepository;

        public async Task<int> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.ApproveAsync(request.Id);
        }
    }
}
