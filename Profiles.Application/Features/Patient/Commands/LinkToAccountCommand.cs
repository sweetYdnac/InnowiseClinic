using MediatR;
using Profiles.Application.Interfaces.Repositories;

namespace Profiles.Application.Features.Patient.Commands
{
    public class LinkToAccountCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
    }

    public class LinkToAccountCommandHandler : IRequestHandler<LinkToAccountCommand, Unit>
    {
        private readonly IPatientRepository _patientRepository;

        public LinkToAccountCommandHandler(IPatientRepository patientRepository) => _patientRepository = patientRepository;

        public async Task<Unit> Handle(LinkToAccountCommand request, CancellationToken cancellationToken)
        {
            await _patientRepository.LinkToAccount(request);
            return Unit.Value;
        }
    }
}
