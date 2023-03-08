using Appointments.Read.Domain.Entities;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsResultsRepository : IRepository<AppointmentResult>
    {
        Task<int> UpdateAsync(Guid id, string complaints, string conclusion, string recomendations);
    }
}
