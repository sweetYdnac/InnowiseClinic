using Profiles.Data.DTOs.Receptionist;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Data.Interfaces.Repositories
{
    public interface IReceptionistsRepository
    {
        Task<ReceptionistResponse> GetByIdAsync(Guid id);
        Task<(IEnumerable<ReceptionistInformationResponse> receptionists, int totalCount)> GetPagedAsync(GetReceptionistsDTO dto);
        Task<int> AddAsync(CreateReceptionistDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateReceptionistDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<Guid> GetAccountIdAsync(Guid id);
        Task<Guid> GetPhotoIdAsync(Guid id);
    }
}
