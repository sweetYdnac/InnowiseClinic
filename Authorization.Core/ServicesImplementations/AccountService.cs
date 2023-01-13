using Authorization.Business.Abstractions;
using Authorization.Data.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Business.ServicesImplementations
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ITokenService _tokenService;

        public AccountService(
            SignInManager<Account> signInManager,
            UserManager<Account> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> SignUp(string email, string password)
        {
            var user = new Account
            {
                Email = email,
                UserName = email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<TokenResponse> SignIn(string email, string password)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true);

            return !signInResult.Succeeded
                ? throw new Exception("Invalid credentionals")
                : await _tokenService.GetToken(user.UserName, password);
        }
    }
}
