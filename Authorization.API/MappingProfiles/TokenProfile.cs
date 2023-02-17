using Authorization.Data.DataTransferObjects;
using AutoMapper;
using IdentityModel.Client;
using Shared.Models.Response.Authorization;

namespace Authorization.API.MappingProfiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<IdentityModel.Client.TokenResponse, TokenResponseDTO>();
            CreateMap<TokenResponseDTO, Shared.Models.Response.Authorization.TokenResponse>();
        }
    }
}
