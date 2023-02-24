using Services.Data.Entities;

namespace Services.Data.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
    }
}
