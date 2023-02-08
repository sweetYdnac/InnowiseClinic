using AutoMapper;
using Profiles.Data.DTOs.Patient;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<GetPatientsRequestModel, GetPatientsDTO>();
            CreateMap<CreatePatientRequestModel, CreatePatientDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()));
            CreateMap<GetMatchedPatientRequestModel, GetMatchedPatientDTO>();
            CreateMap<UpdatePatientRequestModel, UpdatePatientDTO>();
        }
    }
}
