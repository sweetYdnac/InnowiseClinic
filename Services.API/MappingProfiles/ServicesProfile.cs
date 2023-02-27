using AutoMapper;
using Services.Data.Entities;
using Shared.Models.Response.Services.Service;

namespace Services.API.MappingProfiles
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<Service, ServiceResponse>()
                .ForMember(r => r.CategoryTitle, opt => opt.MapFrom(s => s.Category.Title));
        }
    }
}
