using Appointments.Domain.Entities.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Persistence.Configurations.Read
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.PatientId).IsRequired();
            builder.Property(a => a.PatientFullName).IsRequired();
            builder.Property(a => a.PatientPhoneNumber).IsRequired(false);
            builder.Property(a => a.DoctorFullName).IsRequired();
            builder.Property(a => a.ServiceName).IsRequired();
            builder.Property(a => a.IsApproved).IsRequired();

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasData(
                new Appointment
                {
                    Id = new Guid("9CD00FB6-0A51-4AEF-99BD-A1F6C00203C1"),
                    PatientId = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    PatientFullName = "Alex Lorem ",
                    PatientPhoneNumber = null,
                    DoctorFullName = "Test Test ",
                    ServiceName = "Examination",
                    IsApproved = true
                },
                new Appointment
                {
                    Id = new Guid("51E15AF0-A487-48AA-80BC-2C45ABAE4096"),
                    PatientId = new Guid("B2957690-4D76-468C-A449-FB9529283857"),
                    PatientFullName = "Evgeny Koreba Sweety",
                    PatientPhoneNumber = null,
                    DoctorFullName = "Test Test ",
                    ServiceName = "Filling",
                    IsApproved = true
                });
        }
    }
}
