using Microsoft.EntityFrameworkCore;
using Services.Data.Entities;

namespace Services.Data.Contexts
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServicesDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
