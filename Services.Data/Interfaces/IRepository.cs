using Services.Data.Entities;
using Shared.Models;

namespace Services.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Func<T, bool>[] filters);
    }
}
