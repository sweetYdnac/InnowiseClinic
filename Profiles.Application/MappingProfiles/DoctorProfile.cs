using AutoMapper;
using Profiles.Application.Features.Doctor.Commands;
using Profiles.Application.Features.Doctor.Queries;
using Profiles.Domain.Entities;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.Application.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<GetDoctorsRequestModel, GetDoctorsQuery>();
            CreateMap<CreateDoctorRequestModel, CreateDoctorCommand>();
            CreateMap<CreateDoctorCommand, DoctorEntity>()
                .ForMember(ent => ent.Id, opt => opt.MapFrom(command => Guid.NewGuid()));

            CreateMap<CreateDoctorCommand, DoctorSummary>();
        }
    }
}
