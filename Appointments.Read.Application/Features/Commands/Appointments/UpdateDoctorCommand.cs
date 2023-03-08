using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
{
    public class UpdateDoctorCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
        public Guid OfficeId { get; set; }
    }

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public UpdateDoctorCommandHandler(IAppointmentsRepository appointmentsRepository) =>
            _appointmentsRepository = appointmentsRepository;

        public async Task<int> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsRepository.UpdateDoctorAsync(
                request.Id,
                request.FullName,
                request.OfficeId,
                request.SpecializationName);

            return result;
        }
    }
}
