using Microsoft.AspNetCore.Identity;

namespace Authorization.Business.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUp(string email, string password);
        Task<IdentityResult> SignIn(string email, string password);
    }
}
