using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Serilog;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Application.Features.Doctor.Queries
{
    public class GetDoctorsQuery : IRequest<GetDoctorsResponseModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string FullName { get; set; }
    }

    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, GetDoctorsResponseModel>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetDoctorsQueryHandler(IDoctorRepository doctorRepository, IMapper mapper) =>
            (_doctorRepository, _mapper) = (doctorRepository, mapper);

        public async Task<GetDoctorsResponseModel> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var repositoryResponse = await _doctorRepository.GetDoctors(request);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no doctors in storage.");
            }

            var doctors = _mapper.Map<IEnumerable<DoctorPreviewResponse>>(repositoryResponse.doctors);
            return new GetDoctorsResponseModel(doctors, request.PageNumber, request.PageSize, repositoryResponse.totalCount);
        }
    }
}
