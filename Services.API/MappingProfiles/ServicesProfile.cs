using AutoMapper;
using Services.Data.DTOs;
using Services.Data.Entities;
using Shared.Models.Request.Services.Service;
using Shared.Models.Response.Services.Service;

namespace Services.API.MappingProfiles
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<Service, ServiceResponse>()
                .ForMember(r => r.CategoryTitle, opt => opt.MapFrom(s => s.Category.Title));

            CreateMap<GetServicesRequest, GetServicesDTO>();
        }
    }
}
