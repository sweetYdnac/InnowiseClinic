using Profiles.Data.DTOs.Receptionist;
using Shared.Models;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Data.Interfaces.Repositories
{
    public interface IReceptionistsRepository
    {
        Task<ReceptionistResponse> GetByIdAsync(Guid id);
        Task<PagedResult<ReceptionistInformationResponse>> GetPagedAsync(GetReceptionistsDTO dto);
        Task AddAsync(CreateReceptionistDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateReceptionistDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<Guid> GetAccountIdAsync(Guid id);
        Task<Guid> GetPhotoIdAsync(Guid id);
    }
}
