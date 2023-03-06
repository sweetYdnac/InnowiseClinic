using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
{
    public class CreateAppointmentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; }

        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
    }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(IRepository<Appointment> appointmentRepository, IMapper mapper) =>
            (_appointmentRepository, _mapper) = (appointmentRepository, mapper);

        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.AddAsync(_mapper.Map<Appointment>(request));
        }
    }
}
