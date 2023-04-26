using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Read.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class remove_date_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Appointment_Date",
                table: "AppointmentsResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHK_Appointment_Date",
                table: "AppointmentsResults",
                sql: "\"Date\" < CURRENT_TIMESTAMP");
        }
    }
}
