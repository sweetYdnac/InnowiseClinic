using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using Authorization.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Shared.Core.Enums;
using Shared.Exceptions;
using Shared.Exceptions.Authorization;
using Shared.Models.Response.Authorization;

namespace Authorization.Business.ServicesImplementations
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(
            SignInManager<Account> signInManager,
            UserManager<Account> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITokenService tokenService,
            IMapper mapper) =>
        (_signInManager, _userManager, _roleManager, _tokenService, _mapper) =
        (signInManager, userManager, roleManager, tokenService, mapper);

        public async Task<Guid> SignUpAsync(string email, string password, string roleName)
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

            result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                Log.Warning("Role for user with {@Email} didn't set", email);
            }

            var account = await _userManager.FindByEmailAsync(email);

            return account is null
                ? throw new NotFoundException($"Account with email = {email} doesn't exist.")
                : account.Id;
        }

        public async Task<TokenResponseDTO> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new InvalidCredentialsException("Invalid email or password.");
            }

            if (user.Status is AccountStatuses.Inactive)
            {
                throw new AccountInactiveException($"Account with email = {email} has INACTIVE status.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true);

            return !signInResult.Succeeded
                ? throw new InvalidCredentialsException("Invalid email or password.")
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

            if (dto.UpdaterId is null)
            {
                Log.Warning($"Invalid updater account id received from access token");
            }

            account.UpdatedBy = dto.UpdaterId is null ? default : new Guid(dto.UpdaterId);
            account.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(account);

            if (!result.Succeeded)
            {
                Log.Warning("Account with {@Id} wasn't updated", id);
            }
        }

        public async Task UpdateRolesAsync(Guid id, PatchRolesDTO dto)
        {
            var account = await _userManager.FindByIdAsync(id.ToString()) ?? throw new NotFoundException($"Account with id = {id} doesn't exist.");
            var role = await _roleManager.FindByNameAsync(dto.RoleName);

            if (role is null)
            {
                Log.Information("Role with {@Dto.RoleName} doesn't exist", dto.RoleName);
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

        public async Task<AccountResponse> GetById(Guid id)
        {
            var account = await _userManager.FindByIdAsync(id.ToString());

            return account is null
                ? throw new NotFoundException($"Account with id = {id} doesn't exist")
                : _mapper.Map<AccountResponse>(account);

        }
    }
}