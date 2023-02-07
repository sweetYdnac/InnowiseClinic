using AutoMapper;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Features.Patient.Queries;
using Profiles.Domain.Entities;
using Shared.Models.Request.Profiles.Patient;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Application.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientRequestModel, CreatePatientCommand>();
            CreateMap<CreatePatientCommand, PatientEntity>()
                .ForMember(entity => entity.Id, opt => opt.MapFrom(command => Guid.NewGuid()));

            CreateMap<GetMatchedPatientRequestModel, GetMatchedPatientQuery>(); 
            CreateMap<GetPatientsRequestModel, GetPatientsQuery>();

            CreateMap<EditPatientRequestModel, UpdatePatientCommand>();
            CreateMap<UpdatePatientCommand, PatientEntity>();

            CreateMap<LinkToAccountRequestModel, LinkToAccountCommand>();

            CreateMap<PatientEntity, PatientDetailsResponse>();
            CreateMap<PatientEntity, PatientNameResponse>();
        }
    }
}
