using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Application.Features.Patient.Queries
{
    public class GetMatchedPatientQuery : IRequest<PatientDetailsResponse>
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class GetMatchedPatientQueryHandler : IRequestHandler<GetMatchedPatientQuery, PatientDetailsResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetMatchedPatientQueryHandler(IPatientRepository patientRepository, IMapper mapper) =>
           (_patientRepository, _mapper) = (patientRepository, mapper);

        public async Task<PatientDetailsResponse> Handle(GetMatchedPatientQuery request, CancellationToken cancellationToken)
        {
            var patientEntity = await _patientRepository.GetMatchAsync(request);
            return _mapper.Map<PatientDetailsResponse>(patientEntity);
        }
    }
}
