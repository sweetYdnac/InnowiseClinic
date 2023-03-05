using Appointments.Domain.Entities.Write;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Application.Interfaces.Contexts
{
    public interface IWriteAppointmentsDbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentResult> AppointmentsResults { get; set; }
    }
}
