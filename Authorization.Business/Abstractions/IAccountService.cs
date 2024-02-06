using Authorization.Data.DataTransferObjects;
using Shared.Core.Enums;
using Shared.Models.Response.Authorization;

namespace Authorization.Business.Abstractions
{
    public interface IAccountService
    {
        Task<AccountResponse> GetById(Guid id);
        Task<Guid> SignUpAsync(string email, string password, string roleName);
        Task<TokenResponseDTO> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task UpdateAsync(Guid id, PatchAccountDTO dto);
        Task UpdateRolesAsync(Guid id, PatchRolesDTO dto);
    }
}
