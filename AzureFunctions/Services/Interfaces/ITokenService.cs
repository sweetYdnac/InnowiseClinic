using IdentityModel.Client;
using System.Threading.Tasks;

namespace AzureFunctions.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync();
    }
}
