using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Appointments.Write.Application.Features.Commands.Appointments
{
    public class CreateAppointmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
    }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IRepository<Appointment> _appointmentsRepository;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(IRepository<Appointment> writeDbAppointmentRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (writeDbAppointmentRepository, mapper);

        public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var writeEntity = _mapper.Map<Appointment>(request);
            var result = await _appointmentsRepository.AddAsync(writeEntity);

            return request.Id;
        }
    }
}
