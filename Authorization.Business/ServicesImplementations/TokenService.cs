using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
using IdentityModel.Client;
using IdentityServer4;
using Shared.Exceptions;

namespace Authorization.Business.ServicesImplementations
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public TokenService(IHttpClientFactory httpClientFactory, IMapper mapper) =>
            (_httpClientFactory, _mapper) = (httpClientFactory, mapper);

        public async Task<TokenResponseDTO> GetToken(string userName, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new PasswordTokenRequest
            {
                Address = "http://host.docker.internal:8020/connect/token",
                ClientId = "userClient",
                Scope = $"Full { IdentityServerConstants.StandardScopes.OfflineAccess }",
                UserName = userName,
                Password = password
            };

            var response = await client.RequestPasswordTokenAsync(request);
            return _mapper.Map<TokenResponseDTO>(response);
        }

        public async Task<TokenResponseDTO> RefreshToken(string refreshToken)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new RefreshTokenRequest
            {
                Address = "http://host.docker.internal:8020/connect/token",
                ClientId = "userClient",
                Scope = IdentityServerConstants.StandardScopes.OfflineAccess,
                RefreshToken = refreshToken,
            };

            var response = await client.RequestRefreshTokenAsync(request);

            return response.AccessToken is null || response.RefreshToken is null
                ? throw new UnauthorizedException("Refresh token does not valid.")
                : _mapper.Map<TokenResponseDTO>(response);
        }
    }
}
