using AutoMapper;
using Profiles.Data.DTOs.Patient;
using Shared.Messages;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<GetPatientsRequest, GetPatientsDTO>();
            CreateMap<CreatePatientRequest, CreatePatientDTO>();
            CreateMap<GetMatchedPatientRequest, GetMatchedPatientDTO>();
            CreateMap<UpdatePatientRequest, UpdatePatientDTO>();
            CreateMap<UpdatePatientDTO, UpdatePatientMessage>()
                .ForMember(
                message => message.FullName,
                opt => opt.MapFrom(dto => $"{dto.FirstName} {dto.LastName} {dto.MiddleName}"));
        }
    }
}
