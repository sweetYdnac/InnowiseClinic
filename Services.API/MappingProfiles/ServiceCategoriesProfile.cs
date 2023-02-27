using AutoMapper;
using Services.Data.Entities;
using Shared.Models.Response.Services.ServiceCategories;

namespace Services.API.MappingProfiles
{
    internal class ServiceCategoriesProfile : Profile
    {
        public ServiceCategoriesProfile()
        {
            CreateMap<ServiceCategory, ServiceCategoryResponse>();
        }
    }
}
