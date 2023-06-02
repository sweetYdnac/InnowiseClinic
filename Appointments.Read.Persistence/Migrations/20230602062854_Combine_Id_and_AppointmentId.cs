using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Appointments.Read.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Combine_Id_and_AppointmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsResults_Appointments_AppointmentId",
                table: "AppointmentsResults");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsResults_AppointmentId",
                table: "AppointmentsResults");

            migrationBuilder.DeleteData(
                table: "AppointmentsResults",
                keyColumn: "Id",
                keyValue: new Guid("16fc93ad-cb73-4a78-9538-f808f3e812cd"));

            migrationBuilder.DeleteData(
                table: "AppointmentsResults",
                keyColumn: "Id",
                keyValue: new Guid("176999c3-035e-43e1-b68a-f9071dc7a016"));

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "AppointmentsResults");

            migrationBuilder.InsertData(
                table: "AppointmentsResults",
                columns: new[] { "Id", "Complaints", "Conclusion", "Date", "Recommendations" },
                values: new object[,]
                {
                    { new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"), "here we go", "have a disease", new DateTime(2023, 3, 4, 15, 50, 30, 0, DateTimeKind.Unspecified), "go for a walk" },
                    { new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"), "nothing new", "healthy", new DateTime(2023, 3, 1, 13, 20, 10, 0, DateTimeKind.Unspecified), "drink water" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsResults_Appointments_Id",
                table: "AppointmentsResults",
                column: "Id",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsResults_Appointments_Id",
                table: "AppointmentsResults");

            migrationBuilder.DeleteData(
                table: "AppointmentsResults",
                keyColumn: "Id",
                keyValue: new Guid("51e15af0-a487-48aa-80bc-2c45abae4096"));

            migrationBuilder.DeleteData(
                table: "AppointmentsResults",
                keyColumn: "Id",
                keyValue: new Guid("9cd00fb6-0a51-4aef-99bd-a1f6c00203c1"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "AppointmentsResults",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AppointmentsResults",
                columns: new[] { "Id", "AppointmentId", "Complaints", "Conclusion", "Date", "Recommendations" },
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

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsResults_Appointments_AppointmentId",
                table: "AppointmentsResults",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
