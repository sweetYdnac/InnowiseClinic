using AzureFunctions.Configurations;
using AzureFunctions.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctions.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IdentityServerConfiguration _identityServerConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenService(IOptions<IdentityServerConfiguration> identityServerOptions, IHttpClientFactory httpClientFactory) =>
            (_identityServerConfig, _httpClientFactory) = (identityServerOptions.Value, httpClientFactory);

        public async Task<TokenResponse> GetTokenAsync()
        {
            var identityServerClient = _httpClientFactory.CreateClient();
            var request = new ClientCredentialsTokenRequest
            {
                Address = _identityServerConfig.Address,
                ClientId = _identityServerConfig.ClientId,
                Scope = _identityServerConfig.Scope,
            };

            return await identityServerClient.RequestClientCredentialsTokenAsync(request);
        }
    }
}
