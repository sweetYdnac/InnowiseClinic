﻿// <auto-generated />
using System;
using Appointments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Appointments.Persistence.Migrations
{
    [DbContext(typeof(WriteAppointmentsDbContext))]
    partial class WriteAppointmentsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Appointments.Domain.Entities.Write.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"),
                            Date = new DateOnly(2023, 2, 15),
                            DoctorId = new Guid("96c91bee-3b1d-48b0-abae-116bebba3efb"),
                            IsApproved = true,
                            PatientId = new Guid("ea1afb83-5da9-4b81-ad94-b6a62eb25d43"),
                            ServiceId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Time = new TimeOnly(15, 30, 0)
                        },
                        new
                        {
                            Id = new Guid("fff7bcd2-5a83-47f2-a69b-b85399ca96d5"),
                            Date = new DateOnly(2023, 3, 28),
                            DoctorId = new Guid("835cd971-9f41-4a81-a477-b88171671639"),
                            IsApproved = false,
                            PatientId = new Guid("ea1afb83-5da9-4b81-ad94-b6a62eb25d43"),
                            ServiceId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Time = new TimeOnly(9, 40, 0)
                        },
                        new
                        {
                            Id = new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"),
                            Date = new DateOnly(2023, 2, 22),
                            DoctorId = new Guid("835cd971-9f41-4a81-a477-b88171671639"),
                            IsApproved = true,
                            PatientId = new Guid("b2957690-4d76-468c-a449-fb9529283857"),
                            ServiceId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Time = new TimeOnly(11, 0, 0)
                        });
                });

            modelBuilder.Entity("Appointments.Domain.Entities.Write.AppointmentResult", b =>
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
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Recomendations")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("AppointmentsResults");

                    b.HasData(
                        new
                        {
                            Id = new Guid("176999c3-035e-43e1-b68a-f9071dc7a016"),
                            AppointmentId = new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"),
                            Complaints = "nothing new",
                            Conclusion = "healthy",
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Recomendations = "drink water"
                        },
                        new
                        {
                            Id = new Guid("16fc93ad-cb73-4a78-9538-f808f3e812cd"),
                            AppointmentId = new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"),
                            Complaints = "here we go",
                            Conclusion = "have a disease",
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Recomendations = "go for a walk"
                        });
                });

            modelBuilder.Entity("Appointments.Domain.Entities.Write.AppointmentResult", b =>
                {
                    b.HasOne("Appointments.Domain.Entities.Write.Appointment", "Appointment")
                        .WithOne("AppointmentResult")
                        .HasForeignKey("Appointments.Domain.Entities.Write.AppointmentResult", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Appointments.Domain.Entities.Write.Appointment", b =>
                {
                    b.Navigation("AppointmentResult");
                });
#pragma warning restore 612, 618
        }
    }
}
