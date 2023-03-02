using Services.Data.Entities;

namespace Services.Data.Interfaces
{
    public interface IServiceCategoriesRepository
    {
        Task<ServiceCategory> GetByIdAsync(Guid id);
        Task<IEnumerable<ServiceCategory>> GetAllAsync();
    }
}
