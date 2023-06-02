using Authorization.Data.DataTransferObjects;
using Authorization.Data.Entities;

namespace Authorization.Business.Abstractions
{
    public interface IAccountService
    {
        Task<Account> GetById(Guid id);
        Task<Guid> SignUpAsync(string email, string password);
        Task<TokenResponseDTO> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task UpdateAsync(Guid id, PatchAccountDTO dto);
        Task UpdateRolesAsync(Guid id, PatchRolesDTO dto);
    }
}
