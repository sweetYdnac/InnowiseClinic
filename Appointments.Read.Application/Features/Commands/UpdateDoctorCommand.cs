using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
{
    public class UpdateDoctorCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
    }

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IAppointmentsResultsRepository _appointmentsResultRepository;

        public UpdateDoctorCommandHandler(
            IAppointmentsRepository appointmentsRepository,
            IAppointmentsResultsRepository appointmentsResultRepository) =>
        (_appointmentsRepository, _appointmentsResultRepository) =
        (appointmentsRepository, appointmentsResultRepository);

        public async Task<int> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsRepository.UpdateDoctorAsync(request.Id, request.FullName);
            await _appointmentsResultRepository.UpdateDoctorAsync(request.Id, request.SpecializationName);

            return result;
        }
    }
}
