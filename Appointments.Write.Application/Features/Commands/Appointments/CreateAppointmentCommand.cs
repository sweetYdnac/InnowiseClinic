using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using Appointments.Write.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Messages;

namespace Appointments.Write.Application.Features.Commands.Appointments
{
    public class CreateAppointmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid OfficeId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; }

        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
    }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(
            IAppointmentsRepository appointmentRepository,
            IMapper mapper,
            IMessageService messageService) =>
        (_appointmentsRepository,_messageService, _mapper) = (appointmentRepository, messageService, mapper);

        public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var writeEntity = _mapper.Map<Appointment>(request);
            var result = await _appointmentsRepository.AddAsync(writeEntity);

            if (result > 0)
            {
                var message = _mapper.Map<CreateAppointmentMessage>(request);
                await _messageService.SendCreateAppointmentMessageAsync(message);
            }

            return request.Id;
        }
    }
}
