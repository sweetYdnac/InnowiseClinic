using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using Appointments.Write.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Messages;

namespace Appointments.Write.Application.Features.Commands.AppointmentsResults
{
    public class CreateAppointmentResultCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public Guid AppointmentId { get; set; }
    }

    public class CreateAppointmentResultCommandHandler : IRequestHandler<CreateAppointmentResultCommand, Guid>
    {
        private readonly IAppointmentsResultsRepository _appointmentsResultsRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateAppointmentResultCommandHandler(
            IAppointmentsResultsRepository appointmentsResultsRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_appointmentsResultsRepository, _messageService, _mapper) = (appointmentsResultsRepository, messageService, mapper);

        public async Task<Guid> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsResultsRepository.AddAsync(_mapper.Map<AppointmentResult>(request));

            if (result > 0)
            {
                await _messageService.SendCreateAppointmentResultMessage(
                    _mapper.Map<CreateAppointmentResultMessage>(request));
            }

            return request.Id;
        }
    }
}
