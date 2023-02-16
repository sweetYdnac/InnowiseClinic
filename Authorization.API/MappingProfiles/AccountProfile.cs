﻿using Authorization.Data.DataTransferObjects;
using AutoMapper;
using Shared.Messages;
using Shared.Models.Request.Authorization;

namespace Authorization.API.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<PatchRolesRequestModel, PatchRolesDTO>();
            CreateMap<AccountStatusUpdatedMessage, PatchAccountDTO>();
        }
    }
}
