using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Domain.Entities;
using Shared.Models;
using System.Linq.Expressions;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        Task<int> UpdatePatientAsync(Guid id, string fullName, string phoneNumber, DateOnly dateOfBirth);
        Task<int> UpdateDoctorAsync(Guid id, string fullName, Guid OfficeId, string specializationName);
        Task<int> UpdateServiceAsync(Guid id, string name, int timeSlotSize);
        Task<int> RescheduleAsync(Guid id, Guid doctorId, Guid officeId, DateOnly date, TimeOnly time, string doctorFullName);
        Task<int> DeleteByIdAsync(Guid id);
        Task<int> ApproveAsync(Guid id);
        Task<PagedResult<DoctorScheduledAppointmentDTO>> GetDoctorScheduleAsync(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<DoctorScheduledAppointmentDTO>> GetDoctorScheduleAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<DoctorScheduledAppointmentDTO>> GetDoctorScheduleAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, IDictionary<Expression<Func<Appointment, object>>, bool> sorts = null, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<AppointmentDTO>> GetAppointmentsAsync(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<AppointmentDTO>> GetAppointmentsAsync(int currentPage, int pageSize, IDictionary<Expression<Func<Appointment, object>>, bool> sorts = null, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<AppointmentHistoryDTO>> GetAppointmentHistoryAsync(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<AppointmentHistoryDTO>> GetAppointmentHistoryAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, params Expression<Func<Appointment, bool>>[] filters);
        Task<PagedResult<AppointmentHistoryDTO>> GetAppointmentHistoryAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, IDictionary<Expression<Func<Appointment, object>>, bool> sorts = null, params Expression<Func<Appointment, bool>>[] filters);
    }
}
