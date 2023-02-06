using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Application.Features.Patient.Commands
{
    public class CreatePatientCommand : IRequest<Guid?>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? AccountId { get; set; }
    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid?>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public CreatePatientCommandHandler(
            IPatientRepository patientRepository, 
            IMapper mapper) => 
            (_patientRepository, _mapper) = (patientRepository, mapper);

        public async Task<Guid?> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            return await _patientRepository.AddAsync(_mapper.Map<PatientEntity>(request));
        }
    }
}
