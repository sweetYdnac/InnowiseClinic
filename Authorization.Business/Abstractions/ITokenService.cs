using Authorization.Data.DataTransferObjects;

namespace Authorization.Business.Abstractions
{
    public interface ITokenService
    {
        Task<TokenResponseDTO> GetToken(string userName, string password);
        Task<TokenResponseDTO> RefreshToken(string refreshToken);
    }
}
