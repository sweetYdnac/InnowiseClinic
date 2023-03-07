using Appointments.Read.Domain.Entities;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsResultsRepository : IRepository<AppointmentResult>
    {
        Task UpdatePatientAsync(Guid id, DateOnly dateOfBirth);
        Task UpdateDoctorAsync(Guid id, string specializationName);
    }
}
