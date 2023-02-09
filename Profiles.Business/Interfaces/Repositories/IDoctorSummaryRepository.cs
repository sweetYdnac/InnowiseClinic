using Profiles.Data.DTOs.DoctorSummary;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IDoctorSummaryRepository
    {
        Task<int> AddAsync(CreateDoctorSummaryDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateDoctorSummaryDTO dto);
    }
}
