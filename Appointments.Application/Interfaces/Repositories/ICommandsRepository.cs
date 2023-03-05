using Appointments.Domain.Common;

namespace Appointments.Application.Interfaces.Repositories
{
    public interface ICommandsRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
    }
}
