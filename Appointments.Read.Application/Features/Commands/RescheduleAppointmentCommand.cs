using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
{
    public class RescheduleAppointmentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public string DoctorFullName { get; set; }
    }

    public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public RescheduleAppointmentCommandHandler(IAppointmentsRepository appointmentRepository) =>
            _appointmentsRepository = appointmentRepository;

        public async Task<int> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.RescheduleAsync(
                request.Id,
                request.DoctorId,
                request.Date,
                request.Time,
                request.DoctorFullName);
        }
    }
}
