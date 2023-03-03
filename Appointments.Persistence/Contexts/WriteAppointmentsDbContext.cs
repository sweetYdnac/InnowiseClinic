using Appointments.Application.Interfaces.Contexts;
using Appointments.Domain.Entities.Write;
using Appointments.Persistence.Configurations.Write;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Persistence.Contexts
{
    public class WriteAppointmentsDbContext : DbContext, IWriteAppointmentsDbContext
    {
        public WriteAppointmentsDbContext(DbContextOptions<WriteAppointmentsDbContext> options)
            : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentResult> AppointmentsResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentResultConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
