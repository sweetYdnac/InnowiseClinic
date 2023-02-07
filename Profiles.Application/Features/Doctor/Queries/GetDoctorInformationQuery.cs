using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Application.Features.Doctor.Queries
{
    public class GetDoctorInformationQuery : IRequest<DoctorInformationResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetDoctorInformationQueryHandler : IRequestHandler<GetDoctorInformationQuery, DoctorInformationResponse>
    {
        private readonly IDoctorInformationRepository _doctorInformationRepository;

        public GetDoctorInformationQueryHandler(IDoctorInformationRepository doctorInformationRepository) =>
            _doctorInformationRepository = doctorInformationRepository;

        public async Task<DoctorInformationResponse> Handle(GetDoctorInformationQuery request, CancellationToken cancellationToken)
        {
            var response = await _doctorInformationRepository.GetByIdAsync(request.Id);

            return response is null 
                ? throw new NotFoundException($"Doctor with id = {request.Id} doesn't exist.") 
                : response;
        }
    }
}
