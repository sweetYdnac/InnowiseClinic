using AutoMapper;
using Services.Data.DTOs;
using Services.Data.Entities;
using Shared.Models.Request.Services.Specialization;

namespace Services.API.MappingProfiles
{
    public class SpecializationsProfile : Profile
    {
        public SpecializationsProfile()
        {
            CreateMap<CreateSpecializationRequest, CreateSpecializationDTO>();

            CreateMap<CreateSpecializationDTO, Specialization>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()));
        }
    }
}
