using Appointments.Read.Domain.Common;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);
    }
}
