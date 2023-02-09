using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IReceptionistsRepository
    {
        Task<ReceptionistResponse> GetByIdAsync(Guid id);
    }
}
