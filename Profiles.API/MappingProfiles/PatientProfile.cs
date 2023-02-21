using AutoMapper;
using Profiles.Data.DTOs.Patient;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<GetPatientsRequest, GetPatientsDTO>();
            CreateMap<CreatePatientRequest, CreatePatientDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()));
            CreateMap<GetMatchedPatientRequest, GetMatchedPatientDTO>();
            CreateMap<UpdatePatientRequest, UpdatePatientDTO>();
        }
    }
}
