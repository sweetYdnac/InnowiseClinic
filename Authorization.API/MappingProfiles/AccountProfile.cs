using Authorization.API.Models.Request;
using Authorization.Data.DataTransferObjects;
using AutoMapper;

namespace Authorization.API.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<PatchAccountRequestModel, PatchAccountDTO>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(model => model.Status));
        }
    }
}
