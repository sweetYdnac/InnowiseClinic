using Profiles.Application.Parameters;
using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<(IEnumerable<T> data, int totalCount)> GetByPageAsync(PagedParameters parameters);
        Task<Guid?> AddAsync(T entity);
        //Task<int> UpdateAsync(T entity);
        //Task<int> DeleteAsync(Guid id);
    }
}
