using Appointments.Write.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Write.Persistence.Configurations
{
    internal class AppointmentResultConfiguration : IEntityTypeConfiguration<AppointmentResult>
    {
        public void Configure(EntityTypeBuilder<AppointmentResult> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Complaints).IsRequired();
            builder.Property(a => a.Conclusion).IsRequired();
            builder.Property(a => a.Recomendations).IsRequired();

            builder.HasOne(r => r.Appointment)
                .WithOne(a => a.AppointmentResult)
                .HasForeignKey<AppointmentResult>(r => r.AppointmentId)
                .IsRequired();

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<AppointmentResult> builder)
        {
            builder.HasData(
                new AppointmentResult
                {
                    Id = new Guid("176999C3-035E-43E1-B68A-F9071DC7A016"),
                    Complaints = "nothing new",
                    Conclusion = "healthy",
                    Recomendations = "drink water",
                    AppointmentId = new Guid("9CD00FB6-0A51-4AEF-99BD-A1F6C00203C1"),
                },
                new AppointmentResult
                {
                    Id = new Guid("16FC93AD-CB73-4A78-9538-F808F3E812CD"),
                    Complaints = "here we go",
                    Conclusion = "have a disease",
                    Recomendations = "go for a walk",
                    AppointmentId = new Guid("51E15AF0-A487-48AA-80BC-2C45ABAE4096"),
                });
        }
    }
}
