using AutoMapper;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Features.Patient.Queries;
using Profiles.Domain.Entities;
using Shared.Models.Request.Profiles.Patient;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.API.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientRequestModel, CreatePatientCommand>();
            CreateMap<GetPatientsRequestModel, GetPatientsQuery>();

            CreateMap<PatientEntity, PatientNameResponse>();
        }
    }
}
