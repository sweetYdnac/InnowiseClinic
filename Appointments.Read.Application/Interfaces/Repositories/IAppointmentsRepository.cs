using Appointments.Read.Application.Features.Commands;
using Appointments.Read.Domain.Entities;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        Task UpdatePatientAsync(Guid id, string fullName, string phoneNumber);
        Task UpdateDoctorAsync(Guid id, string fullName);
        Task<int> UpdateServiceAsync(UpdateServiceCommand command);
    }
}
