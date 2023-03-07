using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
{
    public class UpdatePatientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IAppointmentsResultsRepository _appointmentsResultRepository;

        public UpdatePatientCommandHandler(
            IAppointmentsRepository appointmentsRepository,
            IAppointmentsResultsRepository appointmentsResultRepository) =>
        (_appointmentsRepository, _appointmentsResultRepository) =
        (appointmentsRepository, appointmentsResultRepository);

        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            await _appointmentsRepository.UpdatePatientAsync(request.Id, request.FullName, request.PhoneNumber);
            await _appointmentsResultRepository.UpdatePatientAsync(request.Id, request.DateOfBirth);

            return Unit.Value;
        }
    }
}
