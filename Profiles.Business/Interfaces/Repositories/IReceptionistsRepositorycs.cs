using Profiles.Data.DTOs.Receptionist;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IReceptionistsRepository
    {
        Task<ReceptionistResponse> GetByIdAsync(Guid id);
        Task<(IEnumerable<ReceptionistInformationResponse> receptionists, int totalCount)> GetPagedAsync(GetReceptionistsDTO dto);
    }
}
