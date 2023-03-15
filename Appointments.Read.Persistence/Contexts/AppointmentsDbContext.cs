using Appointments.Read.Domain.Entities;
using Appointments.Read.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Read.Persistence.Contexts
{
    public class AppointmentsDbContext : DbContext
    {
        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options)
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
