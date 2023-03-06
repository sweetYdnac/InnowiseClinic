using Appointments.Read.Domain.Common;
using Shared.Models;
using System.Linq.Expressions;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Expression<Func<T, bool>>[] filters);
        Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<T, object>>> includes, params Expression<Func<T, bool>>[] filters);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
