using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using Authorization.Data.Entities;
using Authorization.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions.Authorization;
using System.Security.Claims;

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
            ITokenService tokenService) =>
            (_signInManager, _userManager, _roleManager, _tokenService) =
            (signInManager, userManager, roleManager, tokenService);

        public async Task SignUpAsync(string email, string password)
        {
            var id = Guid.NewGuid();

            var user = new Account
            {
                Id = id,
                Email = email,
                UserName = email,
                Status = AccountStatuses.None,
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

            result = await _userManager.AddToRoleAsync(user, AccountRoles.Patient.ToString());

            if (!result.Succeeded)
            {
                throw new NotAddedToRoleException();
            }
        }

        public async Task<TokenResponseDTO> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountNotFoundException();
            }

            if (user.Status is AccountStatuses.Inactive)
            {
                throw new AccountInactiveException();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true);

            return !signInResult.Succeeded
                ? throw new InvalidPasswordException()
                : await _tokenService.GetToken(user.UserName, password);
        }

        public async Task AddToRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountNotFoundException();
            }

            var role = await _roleManager.FindByNameAsync(roleName);

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

        public async Task RemoveFromRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountNotFoundException();
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                throw new RoleIsNotExistException();
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task UpdateAsync(Guid id, PatchAccountDTO dto)
        {
            var account = await _userManager.FindByIdAsync(id.ToString());

            if (account is null)
            {
                throw new AccountNotFoundException();
            }

            account.Status = dto.Status;

            var updaterId = dto.UpdaterClaimsPrincipal.Claims
                .Where(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                .Select(c => c.Value)
                .FirstOrDefault();

            if (updaterId is null)
            {
                throw new AccountNotFoundException();
            }

            account.UpdatedBy = new Guid(updaterId);
            account.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(account);

            if (!result.Succeeded)
            {
                throw new NotUpdatedAccountException();
            }
        }
    }
}