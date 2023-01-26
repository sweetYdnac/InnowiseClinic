using Authorization.Data.DataTransferObjects;

namespace Authorization.Business.Abstractions
{
    public interface IAccountService
    {
        Task SignUpAsync(string email, string password);
        Task<TokenResponseDTO> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task UpdateAsync(Guid id, PatchAccountDTO dto);
        Task UpdateRolesAsync(Guid id, PatchRolesDTO dto);
        Task<Guid> GetIdByEmailAsync(string email);
    }
}
