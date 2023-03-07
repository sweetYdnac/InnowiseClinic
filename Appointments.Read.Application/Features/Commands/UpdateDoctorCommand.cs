using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
{
    public class UpdateDoctorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
    }

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Unit>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IAppointmentsResultsRepository _appointmentsResultRepository;

        public UpdateDoctorCommandHandler(
            IAppointmentsRepository appointmentsRepository,
            IAppointmentsResultsRepository appointmentsResultRepository) =>
        (_appointmentsRepository, _appointmentsResultRepository) =
        (appointmentsRepository, appointmentsResultRepository);

        public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            await _appointmentsRepository.UpdateDoctorAsync(request.Id, request.FullName);
            await _appointmentsResultRepository.UpdateDoctorAsync(request.Id, request.SpecializationName);

            return Unit.Value;
        }
    }
}
