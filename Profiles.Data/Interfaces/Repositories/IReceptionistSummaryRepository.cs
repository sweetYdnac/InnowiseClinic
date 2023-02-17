using Profiles.Data.DTOs.ReceptionistSummary;
using Shared.Core.Enums;

namespace Profiles.Data.Interfaces.Repositories
{
    public interface IReceptionistSummaryRepository
    {
        Task AddAsync(CreateReceptionistSummaryDTO dto);
        Task UpdateAsync(Guid id, UpdateReceptionistSummaryDTO dto);
        Task RemoveAsync(Guid id);
        Task<int> ChangeStatus(Guid id, AccountStatuses status);
    }
}
