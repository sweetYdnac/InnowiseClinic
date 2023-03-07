using Services.Data.Entities;
using Shared.Models;
using System.Linq.Expressions;

namespace Services.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Expression<Func<T, bool>>[] filters);
        Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<T, object>>> includes, params Expression<Func<T, bool>>[] filters);
        Task AddAsync(T entity);
        Task ChangeStatusAsync(Guid id, bool isActive);
        Task<int> UpdateAsync(T entity);
    }
}
