using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Receptionist;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Interfaces.Services
{
    public interface IReceptionistsService
    {
        Task<ReceptionistResponse> GetByIdAsync(Guid id);
        Task<PagedResponse<ReceptionistInformationResponse>> GetPagedAsync(GetReceptionistsDTO dto);
        Task<Guid> CreateAsync(CreateReceptionistDTO dto);
        Task UpdateAsync(Guid id, UpdateReceptionistDTO dto);
        Task RemoveAsync(Guid id);
        Task ChangeStatus(Guid id, ChangeStatusDTO dto);
    }
}
