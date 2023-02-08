using AutoMapper;
using Profiles.Data.DTOs.Doctor;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.API.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<GetDoctorsRequestModel, GetDoctorsDTO>();
        }
    }
}
