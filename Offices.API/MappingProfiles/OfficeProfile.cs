using AutoMapper;
using Offices.Data.DTOs;
using Shared.Messages;
using Shared.Models.Request.Offices;

namespace Offices.API.MappingProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<GetOfficesRequest, GetPagedOfficesDTO>();
            CreateMap<CreateOfficeRequest, CreateOfficeDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()));
            CreateMap<UpdateOfficeRequest, UpdateOfficeDTO>();

            CreateMap<UpdateOfficeDTO, OfficeUpdatedMessage>()
                .ForMember(message => message.OfficeAddress,
                           opt => opt.MapFrom(dto => $"{dto.City} {dto.Street} {dto.HouseNumber} {dto.OfficeNumber}"));
        }
    }
}
