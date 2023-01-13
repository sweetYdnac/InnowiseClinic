using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Business.Abstractions
{
    public interface IAccountService
    {
        Task SignUp(string email, string password);
        Task<TokenResponse> SignIn(string email, string password);
        Task AddToRole(string email, string roleName);
        Task RemoveFromRole(string email, string roleName);
    }
}
