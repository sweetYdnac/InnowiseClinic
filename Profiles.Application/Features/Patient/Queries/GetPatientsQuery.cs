using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Serilog;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Application.Features.Patient.Queries
{
    public class GetPatientsQuery : IRequest<GetPatientsResponseModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FullName { get; set; }
    }

    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, GetPatientsResponseModel>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public GetPatientsQueryHandler(IPatientRepository patientRepository, IMapper mapper) =>
            (_patientRepository, _mapper) = (patientRepository, mapper);
        public async Task<GetPatientsResponseModel> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var repositoryResponse = await _patientRepository.GetPatients(request);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no patients in storage.");
            }

            var patients = _mapper.Map<IEnumerable<PatientNameResponse>>(repositoryResponse.patients);
            return new GetPatientsResponseModel(patients, request.PageNumber, request.PageSize, repositoryResponse.totalCount);
        }
    }
}
