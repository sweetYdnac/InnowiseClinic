using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Application.Features.Patient.Queries
{
    public class GetPatientDetailsQuery : IRequest<PatientDetailsResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetPatientDetailsQueryHandler : IRequestHandler<GetPatientDetailsQuery, PatientDetailsResponse>
    {
        private readonly IGenericRepository<PatientEntity> _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientDetailsQueryHandler(IGenericRepository<PatientEntity> patientRepository, IMapper mapper) =>
           (_patientRepository, _mapper) = (patientRepository, mapper);

        public async Task<PatientDetailsResponse> Handle(GetPatientDetailsQuery request, CancellationToken cancellationToken)
        {
            var patientEntity = await _patientRepository.GetByIdAsync(request.Id);
            return _mapper.Map<PatientDetailsResponse>(patientEntity);
        }
    }
}
