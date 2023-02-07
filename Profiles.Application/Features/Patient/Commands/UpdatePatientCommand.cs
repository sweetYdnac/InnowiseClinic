using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Application.Features.Patient.Commands
{
    public class UpdatePatientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdatePatientCommandHndler : IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public UpdatePatientCommandHndler(IPatientRepository patientRepository, IMapper mapper) =>
            (_patientRepository, _mapper) = (patientRepository, mapper);
        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<PatientEntity>(request);
            await _patientRepository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}