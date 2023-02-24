using Services.Data.Entities;

namespace Services.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
    }
}
