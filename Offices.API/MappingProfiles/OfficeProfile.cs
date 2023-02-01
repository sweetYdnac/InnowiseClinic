using AutoMapper;
using Offices.Application.Features.Office.Queries;
using Shared.Models.Request.Offices;

namespace Offices.API.MappingProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<GetOfficesRequestModel, GetOfficesQuery>();
        }
    }
}
