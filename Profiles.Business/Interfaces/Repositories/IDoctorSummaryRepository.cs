using Profiles.Data.DTOs.DoctorSummary;
using Shared.Core.Enums;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IDoctorSummaryRepository
    {
        Task<int> AddAsync(CreateDoctorSummaryDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateDoctorSummaryDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<int> ChangeStatus(Guid id, AccountStatuses status);
    }
}
