using AutoMapper;
using Profiles.Application.Features.Doctor.Queries;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.Application.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<GetDoctorsRequestModel, GetDoctorsQuery>();
        }
    }
}
