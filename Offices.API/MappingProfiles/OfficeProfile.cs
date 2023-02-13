using AutoMapper;
using Offices.Data.DTOs;
using Shared.Models.Request.Offices;

namespace Offices.API.MappingProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<GetOfficesRequestModel, GetPagedOfficesDTO>();
            CreateMap<CreateOfficeRequestModel, CreateOfficeDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()));
            CreateMap<UpdateOfficeRequestModel, UpdateOfficeDTO>();
        }
    }
}
