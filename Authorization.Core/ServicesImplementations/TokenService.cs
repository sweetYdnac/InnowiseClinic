using Authorization.Business.Abstractions;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Authorization.Business.ServicesImplementations
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly HttpClient _httpClient;

        public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
        {
            _identityServerSettings = identityServerSettings;
            _httpClient = new HttpClient();
            _discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(_identityServerSettings.Value.DiscoveryUrl).Result;

            if (_discoveryDocument.IsError)
            {
                throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponce = await _httpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = _identityServerSettings.Value.ClientName,
                    ClientSecret = _identityServerSettings.Value.ClientPassword,
                    Scope = scope
                });

            return tokenResponce.IsError 
                ? throw new Exception("Unable to get token", tokenResponce.Exception) 
                : tokenResponce;
        }
    }
}
