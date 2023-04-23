using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
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
        private readonly IMapper _mapper;

        public RescheduleAppointmentCommandHandler(IAppointmentsRepository appointmentRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentRepository, mapper);

        public async Task<int> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.RescheduleAsync(
                _mapper.Map<RescheduleAppointmentDTO>(request));
        }
    }
}
