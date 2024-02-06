using Authorization.Data.DataTransferObjects;
using Authorization.Data.Entities;
using AutoMapper;
using Shared.Messages;
using Shared.Models.Request.Authorization;
using Shared.Models.Response.Authorization;

namespace Authorization.API.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<PatchRolesRequest, PatchRolesDTO>();
            CreateMap<UpdateAccountStatusMessage, PatchAccountDTO>();
            CreateMap<Account, AccountResponse>();
        }
    }
}
