using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using Authorization.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Shared.Core.Enums;
using Shared.Exceptions;
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
                throw new AccountNotCreatedException($"Email = {email} is already exist.");
            }

            result = await _userManager.AddToRoleAsync(user, nameof(AccountRoles.Patient));

            if (!result.Succeeded)
            {
                Log.Warning("Default role for user with {email} didn't set.", email);
            }
        }

        public async Task<TokenResponseDTO> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new InvalidCredentialsException("Invalid email or password");
            }

            if (user.Status is AccountStatuses.Inactive)
            {
                throw new AccountInactiveException($"Account with email = {email} has INACTIVE status.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true);

            return !signInResult.Succeeded
                ? throw new InvalidCredentialsException("Invalid email or password")
                : await _tokenService.GetToken(user.UserName, password);
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
                throw new NotFoundException($"Account with id = {id} doesn't exist.");
            }

            account.Status = dto.Status;

            var updaterId = dto.UpdaterClaimsPrincipal.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                ?.Value;

            if (updaterId is null)
            {
                Log.Warning($"Invalid updater account id recieved from access token.");
            }

            account.UpdatedBy = updaterId is null ? default : new Guid(updaterId);
            account.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(account);

            if (!result.Succeeded)
            {
                Log.Warning($"Account with id = {id} wasn't updated.");
            }
        }

        public async Task UpdateRolesAsync(Guid id, PatchRolesDTO dto)
        {
            var account = await _userManager.FindByIdAsync(id.ToString());

            if (account is null)
            {
                throw new NotFoundException($"Account with id = {id} doesn't exist.");
            }

            var role = await _roleManager.FindByNameAsync(dto.RoleName);

            if (role is null)
            {
                Log.Information($"Role with name = {dto.RoleName} doesn't exist.");
            }

            if (!await _userManager.IsInRoleAsync(account, dto.RoleName))
            {
                if (dto.IsAddRole)
                {
                    await _userManager.AddToRoleAsync(account, dto.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(account, dto.RoleName);
                }
            }
        }
    }
}