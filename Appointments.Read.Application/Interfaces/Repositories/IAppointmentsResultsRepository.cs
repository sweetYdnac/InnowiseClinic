using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Domain.Entities;

namespace Appointments.Read.Application.Interfaces.Repositories
{
    public interface IAppointmentsResultsRepository : IRepository<AppointmentResult>
    {
        Task<int> UpdateAsync(EditAppointmentResultDTO dto);
        Task<AppointmentResultDTO> GetByIdAsync(Guid id);
    }
}
