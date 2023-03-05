using Appointments.Domain.Entities.Read;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Application.Interfaces.Contexts
{
    public interface IReadAppointmentsDbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentResult> AppointmentsResults { get; set; }
    }
}
