using Authorization.Data.DataTransferObjects;

namespace Authorization.Business.Abstractions
{
    public interface IAccountService
    {
        Task SignUpAsync(string email, string password);
        Task<TokenResponseDTO> SignInAsync(string email, string password);
        Task AddToRoleAsync(string email, string roleName);
        Task RemoveFromRoleAsync(string email, string roleName);
        Task SignOutAsync();
        Task UpdateAsync(Guid id, PatchAccountDTO dto);
    }
}
