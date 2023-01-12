using Authorization.Business.Abstractions;
using Authorization.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Business.ServicesImplementations
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AccountService(
            SignInManager<Account> signInManager, 
            UserManager<Account> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task<IdentityResult> SignIn(string email, string password)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true);

            return signInResult.Succeeded
                ? await _userManager.AddLoginAsync(user, new UserLoginInfo("local", "local", "local"))
                : throw new Exception("Invalid credentionals");
        }
    }
}
