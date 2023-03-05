using Appointments.Application.Interfaces.Contexts;
using Appointments.Domain.Entities.Read;
using Appointments.Persistence.Configurations.Read;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Persistence.Contexts
{
    public class ReadAppointmentsDbContext : DbContext, IReadAppointmentsDbContext
    {
        public ReadAppointmentsDbContext(DbContextOptions<ReadAppointmentsDbContext> options)
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
