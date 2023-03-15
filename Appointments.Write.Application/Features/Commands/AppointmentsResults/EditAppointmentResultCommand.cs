using Appointments.Write.Application.DTOs.AppointmentResult;
using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using AutoMapper;
using MediatR;
using Shared.Messages;

namespace Appointments.Write.Application.Features.Commands.AppointmentsResults
{
    public class EditAppointmentResultCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
    }

    public class EditAppointmentResultCommandHandler : IRequestHandler<EditAppointmentResultCommand, Unit>
    {
        private readonly IAppointmentsResultsRepository _appointmentsResultsRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public EditAppointmentResultCommandHandler(
            IAppointmentsResultsRepository appointmentsResultsRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_appointmentsResultsRepository, _messageService, _mapper) = (appointmentsResultsRepository, messageService, mapper);

        public async Task<Unit> Handle(EditAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsResultsRepository.UpdateAsync(
                _mapper.Map<EditAppointmentResultDTO>(request));

            if (result > 0)
            {
                await _messageService.SendEditAppointmentResultMessageAsync(
                    _mapper.Map<EditAppointmentResultMessage>(request));
            }

            return Unit.Value;
        }
    }
}
