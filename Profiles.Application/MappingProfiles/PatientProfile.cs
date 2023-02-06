using AutoMapper;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Domain.Entities;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Application.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientCommand, PatientEntity>()
                .ForMember(entity => entity.Id, opt => opt.MapFrom(command => Guid.NewGuid()));


            CreateMap<PatientEntity, PatientDetailsResponse>();
        }
    }
}
