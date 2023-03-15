using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.AppointmentsResults
{
    public class CreateAppointmentResultCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public Guid AppointmentId { get; set; }
    }

    public class CreateAppointmentResultCommandHandler : IRequestHandler<CreateAppointmentResultCommand, int>
    {
        private readonly IAppointmentsResultsRepository _appointmentsResultsRepository;
        private readonly IMapper _mapper;

        public CreateAppointmentResultCommandHandler(
            IAppointmentsResultsRepository appointmentsResultsRepository,
            IMapper mapper) =>
        (_appointmentsResultsRepository, _mapper) = (appointmentsResultsRepository, mapper);

        public async Task<int> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AppointmentResult>(request);

            return await _appointmentsResultsRepository.AddAsync(entity);
        }
    }
}
