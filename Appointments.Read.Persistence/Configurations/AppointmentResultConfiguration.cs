using Appointments.Read.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Read.Persistence.Configurations
{
    internal class AppointmentResultConfiguration : IEntityTypeConfiguration<AppointmentResult>
    {
        public void Configure(EntityTypeBuilder<AppointmentResult> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Complaints).IsRequired();
            builder.Property(a => a.Conclusion).IsRequired();
            builder.Property(a => a.Recommendations).IsRequired();

            builder.Property(a => a.Date)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.HasOne(r => r.Appointment)
                .WithOne(a => a.AppointmentResult)
                .HasForeignKey<AppointmentResult>(r => r.Id)
                .IsRequired();

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<AppointmentResult> builder)
        {
            builder.HasData(
                new AppointmentResult
                {
                    Id = new Guid("9CD00FB6-0A51-4AEF-99BD-A1F6C00203C1"),
                    Date = new DateTime(2023, 3, 1, 13, 20, 10),
                    Complaints = "nothing new",
                    Conclusion = "healthy",
                    Recommendations = "drink water",
                },
                new AppointmentResult
                {
                    Id = new Guid("51E15AF0-A487-48AA-80BC-2C45ABAE4096"),
                    Date = new DateTime(2023, 3, 4, 15, 50, 30),
                    Complaints = "here we go",
                    Conclusion = "have a disease",
                    Recommendations = "go for a walk",
                }); ;
        }
    }
}
