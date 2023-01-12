using IdentityModel.Client;

namespace Authorization.Business.Abstractions
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
