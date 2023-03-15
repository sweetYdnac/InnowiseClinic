using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Appointments.Write.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Complaints = table.Column<string>(type: "text", nullable: false),
                    Conclusion = table.Column<string>(type: "text", nullable: false),
                    Recomendations = table.Column<string>(type: "text", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsResults", x => x.Id);
                    table.CheckConstraint("CHK_Appointment_Date", "\"Date\" < CURRENT_TIMESTAMP");
                    table.ForeignKey(
                        name: "FK_AppointmentsResults_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorId", "IsApproved", "PatientId", "ServiceId", "Time" },
                values: new object[,]
                {
                    { new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"), new DateOnly(2023, 2, 22), new Guid("835cd971-9f41-4a81-a477-b88171671639"), true, new Guid("b2957690-4d76-468c-a449-fb9529283857"), new Guid("ebbc7a6c-21c7-4049-b68a-544056861d45"), new TimeOnly(11, 0, 0) },
                    { new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"), new DateOnly(2023, 2, 15), new Guid("96c91bee-3b1d-48b0-abae-116bebba3efb"), true, new Guid("ea1afb83-5da9-4b81-ad94-b6a62eb25d43"), new Guid("cd0d073b-acc8-4ccf-8119-9ec909ed70ed"), new TimeOnly(15, 30, 0) },
                    { new Guid("fff7bcd2-5a83-47f2-a69b-b85399ca96d5"), new DateOnly(2023, 3, 28), new Guid("835cd971-9f41-4a81-a477-b88171671639"), false, new Guid("ea1afb83-5da9-4b81-ad94-b6a62eb25d43"), new Guid("cd0d073b-acc8-4ccf-8119-9ec909ed70ed"), new TimeOnly(9, 40, 0) }
                });

            migrationBuilder.InsertData(
                table: "AppointmentsResults",
                columns: new[] { "Id", "AppointmentId", "Complaints", "Conclusion", "Date", "Recomendations" },
                values: new object[,]
                {
                    { new Guid("16fc93ad-cb73-4a78-9538-f808f3e812cd"), new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"), "here we go", "have a disease", new DateTime(2023, 3, 4, 15, 50, 30, 0, DateTimeKind.Unspecified), "go for a walk" },
                    { new Guid("176999c3-035e-43e1-b68a-f9071dc7a016"), new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"), "nothing new", "healthy", new DateTime(2023, 3, 1, 13, 20, 10, 0, DateTimeKind.Unspecified), "drink water" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsResults_AppointmentId",
                table: "AppointmentsResults",
                column: "AppointmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentsResults");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
