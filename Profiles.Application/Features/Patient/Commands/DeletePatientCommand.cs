using MediatR;
using Profiles.Application.Interfaces.Repositories;

namespace Profiles.Application.Features.Patient.Commands
{
    public class DeletePatientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
    {
        private readonly IPatientRepository _patientRepository;

        public DeletePatientCommandHandler(IPatientRepository patientRepository) => _patientRepository = patientRepository;

        public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            await _patientRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
