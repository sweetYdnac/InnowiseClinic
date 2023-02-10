using Profiles.Data.DTOs.ReceptionistSummary;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IReceptionistSummaryRepository
    {
        Task<int> CreateAsync(CreateReceptionistSummaryDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateReceptionistSummaryDTO dto);
        Task<int> RemoveAsync(Guid id);
    }
}
