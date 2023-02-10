using Profiles.Data.DTOs.ReceptionistSummary;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IReceptionistSummaryRepository
    {
        Task<int> CreateAsync(CreateReceptionistSummaryDTO dto);
    }
}
