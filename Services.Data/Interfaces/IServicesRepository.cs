using Services.Data.Entities;

namespace Services.Data.Interfaces
{
    public interface IServicesRepository : IRepository<Service>
    {
        Task DisableAsync(Guid specializationId);
    }
}
