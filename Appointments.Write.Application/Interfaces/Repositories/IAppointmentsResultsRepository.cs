using Appointments.Write.Domain.Entities;

namespace Appointments.Write.Application.Interfaces.Repositories
{
    public interface IAppointmentsResultsRepository : IRepository<AppointmentResult>
    {
        Task<int> UpdateAsync(Guid id, string complaints, string conclusion, string recomendations);
    }
}
