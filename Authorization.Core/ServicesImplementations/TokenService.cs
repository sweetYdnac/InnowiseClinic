using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
using IdentityModel.Client;
using IdentityServer4;

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
                ClientId = "machineClient",
                Scope = $"Full { IdentityServerConstants.StandardScopes.OfflineAccess }",
                UserName = userName,
                Password = password
            };

            var response = await client.RequestPasswordTokenAsync(request);
            return _mapper.Map<TokenResponseDTO>(response);
        }
    }
}
