using Authorization.Data.DataTransferObjects;
using AutoMapper;
using Shared.Models.Messages;
using Shared.Models.Request.Authorization;

namespace Authorization.API.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<PatchAccountRequestModel, PatchAccountDTO>();
            CreateMap<PatchRolesRequestModel, PatchRolesDTO>();

            CreateMap<AccountStatusUpdated, PatchAccountDTO>();
        }
    }
}
