using Authorization.Business.Abstractions;
using IdentityModel.Client;

namespace Authorization.Business.ServicesImplementations
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }

        public async Task<TokenResponse> GetToken(string userName, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new PasswordTokenRequest
            {
                Address = "https://localhost:44306/connect/token",
                ClientId = "machineClient",
                Scope = "Full",
                UserName = userName,
                Password = password
            };

            return await client.RequestPasswordTokenAsync(request);
        }
    }
}
