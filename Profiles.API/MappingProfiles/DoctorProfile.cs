using AutoMapper;
using Profiles.Data.DTOs.Doctor;
using Profiles.Data.DTOs.DoctorSummary;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.API.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<GetDoctorsRequest, GetDoctorsDTO>();
            CreateMap<CreateDoctorRequest, CreateDoctorDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()))
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(model => model.DateOfBirth.ToDateTime(TimeOnly.MinValue)));
            CreateMap<UpdateDoctorRequest, UpdateDoctorDTO>()
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(model => model.DateOfBirth.ToDateTime(TimeOnly.MinValue)));

            CreateMap<CreateDoctorDTO, CreateDoctorSummaryDTO>();
            CreateMap<UpdateDoctorDTO, UpdateDoctorSummaryDTO>();
        }
    }
}
