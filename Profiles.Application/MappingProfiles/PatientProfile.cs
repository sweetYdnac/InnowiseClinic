using AutoMapper;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Domain.Entities;

namespace Profiles.Application.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientCommand, PatientEntity>()
                .ForMember(entity => entity.Id, opt => opt.MapFrom(command => Guid.NewGuid()));
        }
    }
}
