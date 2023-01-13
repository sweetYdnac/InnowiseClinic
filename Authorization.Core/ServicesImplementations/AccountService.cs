using Authorization.Business.Abstractions;
using Authorization.Data.Entities;
using Authorization.Data.Enums;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions.Authorization;

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

        public async Task SignUp(string email, string password)
        {
            var id = Guid.NewGuid();

            var user = new Account
            {
                Id = id,
                Email = email,
                UserName = email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = id,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = id
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new AccountNotCreatedException();
            }

            result = await _userManager.AddToRoleAsync(user, AccountRoles.User.ToString());

            if (!result.Succeeded)
            {
                throw new NotAddedToRoleException();
            }
        }

        public async Task<TokenResponse> SignIn(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountNotFoundException();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true);

            return !signInResult.Succeeded
                ? throw new InvalidPasswordException()
                : await _tokenService.GetToken(user.UserName, password);
        }

        public async Task AddToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountNotFoundException();
            }

            var role = _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                throw new RoleIsNotExistException();
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new NotAddedToRoleException();
            }
        }

        public async Task RemoveFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountNotFoundException();
            }

            var role = _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                throw new RoleIsNotExistException();
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new NotRemovedFromRoleException();
            }
        }
    }
}
