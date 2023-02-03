using AutoMapper;
using Profiles.Application.Features.Patient.Commands;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientRequestModel, CreatePatientCommand>();
        }
    }
}
