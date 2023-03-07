using Appointments.Write.Domain.Common;

namespace Appointments.Write.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);
        Task<int> DeleteByIdAsync(Guid id);
    }
}
