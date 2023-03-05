using Appointments.Domain.Common;

namespace Appointments.Application.Interfaces.Repositories
{
    public interface IReadDbRepository<T> :  IQueriesRepository<T>, ICommandsRepository<T> where T : BaseEntity
    {
    }
}
