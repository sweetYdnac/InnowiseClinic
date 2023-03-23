using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.AppointmentsResults
{
    public class EditAppointmentResultCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }
    }

    public class EditAppointmentResultCommandHandler : IRequestHandler<EditAppointmentResultCommand, int>
    {
        private readonly IAppointmentsResultsRepository _appointmentsResultsRepository;
        private readonly IMapper _mapper;

        public EditAppointmentResultCommandHandler(
            IAppointmentsResultsRepository appointmentsResultsRepository,
            IMapper mapper) =>
        (_appointmentsResultsRepository, _mapper) = (appointmentsResultsRepository, mapper);

        public async Task<int> Handle(EditAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsResultsRepository.UpdateAsync(
                _mapper.Map<EditAppointmentResultDTO>(request));
        }
    }
}
