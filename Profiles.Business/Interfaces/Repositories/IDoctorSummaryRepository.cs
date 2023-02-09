using Profiles.Data.DTOs.DoctorSummary;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IDoctorSummaryRepository
    {
        Task<int> AddAsync(DoctorSummaryDTO dto);
    }
}
