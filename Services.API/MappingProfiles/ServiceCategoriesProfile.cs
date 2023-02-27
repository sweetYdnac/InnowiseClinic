using AutoMapper;
using Services.Data.Entities;
using Shared.Models.Response.Services.ServiceCategories;

namespace Services.API.MappingProfiles
{
    public class ServiceCategoriesProfile : Profile
    {
        public ServiceCategoriesProfile()
        {
            CreateMap<ServiceCategory, ServiceCategoryResponse>();
        }
    }
}
