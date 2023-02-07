using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Serilog;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Application.Features.Doctor.Queries
{
    public class GetDoctorsInformationQuery : IRequest<GetDoctorsResponseModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string FullName { get; set; }
    }

    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsInformationQuery, GetDoctorsResponseModel>
    {
        private readonly IDoctorInformationRepository _doctorInformationRepository;
        private readonly IMapper _mapper;

        public GetDoctorsQueryHandler(IDoctorInformationRepository doctorInformationRepository, IMapper mapper) =>
            (_doctorInformationRepository, _mapper) = (doctorInformationRepository, mapper);

        public async Task<GetDoctorsResponseModel> Handle(GetDoctorsInformationQuery request, CancellationToken cancellationToken)
        {
            var repositoryResponse = await _doctorInformationRepository.GetDoctors(request);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no doctors in storage.");
            }

            var doctors = _mapper.Map<IEnumerable<DoctorInformationResponse>>(repositoryResponse.doctors);
            return new GetDoctorsResponseModel(doctors, request.PageNumber, request.PageSize, repositoryResponse.totalCount);
        }
    }
}
