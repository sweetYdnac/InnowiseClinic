using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Domain.Entities;
using Shared.Models;
using System.Linq.Expressions;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        Task<int> UpdatePatientAsync(UpdatePatientDTO dto);
        Task<int> UpdateDoctorAsync(UpdateDoctorDTO dto);
        Task<int> UpdateServiceAsync(UpdateServiceDTO dto);
        Task<int> RescheduleAsync(RescheduleAppointmentDTO dto);
        Task<int> DeleteByIdAsync(Guid id);
        Task<int> ApproveAsync(Guid id);
        Task<IEnumerable<TimeSlotAppointmentDTO>> GetAppointmentsAsync(DateOnly date, IEnumerable<Guid> doctors);
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
