using Appointments.Write.Domain.Entities;
using Appointments.Write.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Write.Persistence.Contexts
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
