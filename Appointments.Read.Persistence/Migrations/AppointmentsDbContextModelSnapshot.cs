﻿// <auto-generated />
using System;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Appointments.Read.Persistence.Migrations
{
    [DbContext(typeof(AppointmentsDbContext))]
    partial class AppointmentsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Appointments.Read.Domain.Entities.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("DoctorFullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<string>("PatientFullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("PatientPhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Appointments", t =>
                        {
                            t.HasCheckConstraint("CHK_Appointment_Duration", "\"Duration\" > 0");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"),
                            Date = new DateOnly(2023, 2, 15),
                            DoctorFullName = "Test Test ",
                            DoctorId = new Guid("96c91bee-3b1d-48b0-abae-116bebba3efb"),
                            Duration = 20,
                            IsApproved = true,
                            PatientFullName = "Alex Lorem ",
                            PatientId = new Guid("ea1afb83-5da9-4b81-ad94-b6a62eb25d43"),
                            ServiceId = new Guid("cd0d073b-acc8-4ccf-8119-9ec909ed70ed"),
                            ServiceName = "Examination",
                            Time = new TimeOnly(15, 30, 0)
                        },
                        new
                        {
                            Id = new Guid("fff7bcd2-5a83-47f2-a69b-b85399ca96d5"),
                            Date = new DateOnly(2023, 3, 28),
                            DoctorFullName = "Test Test ",
                            DoctorId = new Guid("835cd971-9f41-4a81-a477-b88171671639"),
                            Duration = 20,
                            IsApproved = false,
                            PatientFullName = "Alex Lorem ",
                            PatientId = new Guid("ea1afb83-5da9-4b81-ad94-b6a62eb25d43"),
                            ServiceId = new Guid("cd0d073b-acc8-4ccf-8119-9ec909ed70ed"),
                            ServiceName = "Examination",
                            Time = new TimeOnly(9, 40, 0)
                        },
                        new
                        {
                            Id = new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"),
                            Date = new DateOnly(2023, 2, 22),
                            DoctorFullName = "Test Test ",
                            DoctorId = new Guid("835cd971-9f41-4a81-a477-b88171671639"),
                            Duration = 30,
                            IsApproved = true,
                            PatientFullName = "Evgeny Koreba Sweety",
                            PatientId = new Guid("b2957690-4d76-468c-a449-fb9529283857"),
                            ServiceId = new Guid("ebbc7a6c-21c7-4049-b68a-544056861d45"),
                            ServiceName = "Filling",
                            Time = new TimeOnly(11, 0, 0)
                        });
                });

            modelBuilder.Entity("Appointments.Read.Domain.Entities.AppointmentResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Complaints")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Conclusion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp");

                    b.Property<string>("DoctorSpecializationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("PatientDateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Recomendations")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("AppointmentsResults", t =>
                        {
                            t.HasCheckConstraint("CHK_Appointment_Date", "\"Date\" < CURRENT_TIMESTAMP");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("176999c3-035e-43e1-b68a-f9071dc7a016"),
                            AppointmentId = new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"),
                            Complaints = "nothing new",
                            Conclusion = "healthy",
                            Date = new DateTime(2023, 3, 1, 13, 20, 10, 0, DateTimeKind.Unspecified),
                            DoctorSpecializationName = "Therapist",
                            PatientDateOfBirth = new DateOnly(1980, 11, 28),
                            Recomendations = "drink water"
                        },
                        new
                        {
                            Id = new Guid("16fc93ad-cb73-4a78-9538-f808f3e812cd"),
                            AppointmentId = new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"),
                            Complaints = "here we go",
                            Conclusion = "have a disease",
                            Date = new DateTime(2023, 3, 4, 15, 50, 30, 0, DateTimeKind.Unspecified),
                            DoctorSpecializationName = "Dentist",
                            PatientDateOfBirth = new DateOnly(2000, 6, 15),
                            Recomendations = "go for a walk"
                        });
                });

            modelBuilder.Entity("Appointments.Read.Domain.Entities.AppointmentResult", b =>
                {
                    b.HasOne("Appointments.Read.Domain.Entities.Appointment", "Appointment")
                        .WithOne("AppointmentResult")
                        .HasForeignKey("Appointments.Read.Domain.Entities.AppointmentResult", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Appointments.Read.Domain.Entities.Appointment", b =>
                {
                    b.Navigation("AppointmentResult");
                });
#pragma warning restore 612, 618
        }
    }
}
