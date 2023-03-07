using Appointments.Read.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Read.Persistence.Configurations
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.PatientId).IsRequired();
            builder.Property(a => a.DoctorId).IsRequired();
            builder.Property(a => a.ServiceId).IsRequired();
            builder.Property(a => a.IsApproved).IsRequired();
            builder.Property(a => a.PatientFullName).IsRequired();
            builder.Property(a => a.PatientPhoneNumber).IsRequired(false);
            builder.Property(a => a.DoctorFullName).IsRequired();
            builder.Property(a => a.ServiceName).IsRequired();
            builder.Property(a => a.Duration).IsRequired();

            builder.Property(a => a.Date)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(a => a.Time)
                .HasColumnType("time")
                .IsRequired();

            builder.ToTable(a => a.HasCheckConstraint("CHK_Appointment_Duration", "\"Duration\" > 0"))
;
            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasData(
                new Appointment
                {
                    Id = new Guid("9CD00FB6-0A51-4AEF-99BD-A1F6C00203C1"),
                    PatientId = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    DoctorId = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    ServiceId = new Guid("CD0D073B-ACC8-4CCF-8119-9EC909ED70ED"),
                    Date = new DateOnly(2023, 2, 15),
                    Time = new TimeOnly(15, 30),
                    Duration = 20,
                    IsApproved = true,
                    PatientFullName = "Alex Lorem ",
                    PatientPhoneNumber = null,
                    DoctorFullName = "Test Test ",
                    ServiceName = "Examination",
                },
                new Appointment
                {
                    Id = new Guid("FFF7BCD2-5A83-47F2-A69B-B85399CA96D5"),
                    PatientId = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    DoctorId = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
                    ServiceId = new Guid("CD0D073B-ACC8-4CCF-8119-9EC909ED70ED"),
                    Date = new DateOnly(2023, 3, 28),
                    Time = new TimeOnly(09, 40),
                    Duration = 20,
                    IsApproved = false,
                    PatientFullName = "Alex Lorem ",
                    PatientPhoneNumber = null,
                    DoctorFullName = "Test Test ",
                    ServiceName = "Examination",
                },
                new Appointment
                {
                    Id = new Guid("51E15AF0-A487-48AA-80BC-2C45ABAE4096"),
                    PatientId = new Guid("B2957690-4D76-468C-A449-FB9529283857"),
                    DoctorId = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
                    ServiceId = new Guid("EBBC7A6C-21C7-4049-B68A-544056861D45"),
                    Date = new DateOnly(2023, 2, 22),
                    Time = new TimeOnly(11, 00),
                    Duration = 30,
                    IsApproved = true,
                    PatientFullName = "Evgeny Koreba Sweety",
                    PatientPhoneNumber = null,
                    DoctorFullName = "Test Test ",
                    ServiceName = "Filling",
                });
        }
    }
}
