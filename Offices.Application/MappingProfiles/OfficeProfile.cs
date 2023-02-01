using AutoMapper;
using Offices.Domain.Entities;
using Shared.Models.Response.Offices;

namespace Offices.Application.MappingProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<OfficeEntity, OfficePreviewResponse>();
        }
    }
}
