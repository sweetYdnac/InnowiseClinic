using Appointments.Domain.Common;

namespace Appointments.Application.Interfaces.Repositories
{
    public interface IQueriesRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
    }
}
