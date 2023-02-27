using Services.Data.Entities;
using Shared.Models;
using System.Linq.Expressions;

namespace Services.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync<T1>(Guid id, Expression<Func<T,T1>> include1);
        Task<T> GetByIdAsync<T1, T2>(Guid id, Expression<Func<T,T1>> include1, Expression<Func<T, T2>> include2);
        Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Expression<Func<T, bool>>[] filters);
        Task<PagedResult<T>> GetPagedAndFilteredAsync<T1>(int currentPage, int pageSize, Expression<Func<T, T1>> include1, params Expression<Func<T, bool>>[] filters);
        Task<PagedResult<T>> GetPagedAndFilteredAsync<T1,T2>(int currentPage, int pageSize, Expression<Func<T, T1>> include1, Expression<Func<T, T2>> include2, params Expression<Func<T, bool>>[] filters);
        Task AddAsync(T entity);
        Task ChangeStatusAsync(Guid id, bool isActive);
        Task UpdateAsync(T entity);
    }
}
