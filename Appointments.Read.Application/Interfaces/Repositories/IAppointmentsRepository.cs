using Appointments.Read.Domain.Entities;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        Task<int> UpdatePatientAsync(Guid id, string fullName, string phoneNumber);
        Task<int> UpdateDoctorAsync(Guid id, string fullName);
        Task<int> UpdateServiceAsync(Guid id, string name, int timeSlotSize);
        Task<int> RescheduleAppointmentAsync(Guid id, Guid doctorId, DateOnly date, TimeOnly time, string doctorFullName);
    }
}
