using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Application.Features.Patient.Queries
{
    public class GetPatientDetailsQuery : IRequest<PatientResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetPatientDetailsQueryHandler : IRequestHandler<GetPatientDetailsQuery, PatientResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientDetailsQueryHandler(IPatientRepository patientRepository, IMapper mapper) =>
           (_patientRepository, _mapper) = (patientRepository, mapper);

        public async Task<PatientResponse> Handle(GetPatientDetailsQuery request, CancellationToken cancellationToken)
        {
            var patientEntity = await _patientRepository.GetByIdAsync(request.Id);
            return _mapper.Map<PatientResponse>(patientEntity);
        }
    }
}
