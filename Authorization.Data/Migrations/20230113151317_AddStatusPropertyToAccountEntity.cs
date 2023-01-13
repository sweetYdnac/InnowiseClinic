using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Migrations
{
    public partial class AddStatusPropertyToAccountEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0422c2d9-60d0-46c9-865d-faa62b87800c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("556297c7-7e1e-4915-b88a-1352a25327b9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("67f709f8-3407-42d8-a3cb-6a6b37fad2c1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aff5b280-9ab5-4e99-a6e0-ca4114630ac5"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("18b1ca4c-698f-4dc4-8277-87e39ec25876"), "adeaf5fa-e89d-4a6c-881a-52605469dd46", "Doctor", "DOCTOR" },
                    { new Guid("4e64fcad-c355-4f7d-a7d0-1adf587380c8"), "21ceb85a-da86-4c42-8e5f-f1db697a7a55", "Patient", "PATIENT" },
                    { new Guid("566d1d5d-69e0-4641-86c9-fd6de27322fe"), "84d48cf2-7835-493b-803a-3a819e683611", "Receptionist", "RECEPTIONIST" },
                    { new Guid("6817b4de-ca73-49ab-a0d1-11a0b6a225d4"), "41095aff-a518-4d82-829d-7fcf3daedf66", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18b1ca4c-698f-4dc4-8277-87e39ec25876"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e64fcad-c355-4f7d-a7d0-1adf587380c8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("566d1d5d-69e0-4641-86c9-fd6de27322fe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6817b4de-ca73-49ab-a0d1-11a0b6a225d4"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0422c2d9-60d0-46c9-865d-faa62b87800c"), "ea967295-294b-4292-b388-00cb0e1f20d5", "Doctor", "DOCTOR" },
                    { new Guid("556297c7-7e1e-4915-b88a-1352a25327b9"), "39a9178d-3fdc-4540-a0a9-f327bb667c5e", "Admin", "ADMIN" },
                    { new Guid("67f709f8-3407-42d8-a3cb-6a6b37fad2c1"), "15bc6f28-4822-469c-ac08-82d5a49d375d", "Reseptionist", "RESEPTIONIST" },
                    { new Guid("aff5b280-9ab5-4e99-a6e0-ca4114630ac5"), "eaf97cc0-c009-4b48-b52c-291c3c154e77", "User", "USER" }
                });
        }
    }
}
