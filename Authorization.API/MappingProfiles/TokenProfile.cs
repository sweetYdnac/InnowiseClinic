using Authorization.API.Models.Responce;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
using IdentityModel.Client;

namespace Authorization.API.MappingProfiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenResponse, TokenResponseDTO>()
                .ForMember(dto => dto.AccessToken, opt => opt.MapFrom(model => model.AccessToken))
                .ForMember(dto => dto.RefreshToken, opt => opt.MapFrom(model => model.RefreshToken));

            CreateMap<TokenResponseDTO, TokenResponseModel>()
                .ForMember(model => model.AccessToken, opt => opt.MapFrom(dto => dto.AccessToken))
                .ForMember(model => model.RefreshToken, opt => opt.MapFrom(dto => dto.RefreshToken));
        }
    }
}
