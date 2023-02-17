using Profiles.Data.DTOs.DoctorSummary;
using Shared.Core.Enums;

namespace Profiles.Data.Interfaces.Repositories
{
    public interface IDoctorSummaryRepository
    {
        Task AddAsync(CreateDoctorSummaryDTO dto);
        Task UpdateAsync(Guid id, UpdateDoctorSummaryDTO dto);
        Task RemoveAsync(Guid id);
        Task<int> ChangeStatus(Guid id, AccountStatuses status);
    }
}
