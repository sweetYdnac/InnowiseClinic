using Profiles.Data.DTOs.ReceptionistSummary;
using Shared.Core.Enums;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IReceptionistSummaryRepository
    {
        Task<int> AddAsync(CreateReceptionistSummaryDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateReceptionistSummaryDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<int> ChangeStatus(Guid id, AccountStatuses status);
    }
}
