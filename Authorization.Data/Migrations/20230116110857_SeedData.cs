using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("175d3061-c995-4216-93e0-29e3555a1386"), "6e1bcfda-3cf7-48d8-b91d-49a8f9d08bb2", "Admin", "ADMIN" },
                    { new Guid("712a10c4-2c72-4f78-9601-af3d617acfd2"), "0a992d35-4501-4493-a8f0-6c57599a3530", "Doctor", "DOCTOR" },
                    { new Guid("8d94b0d5-ac37-4f25-8363-1583f8f5544b"), "f7fd077d-dd57-4de1-b68f-c2450ff3fb7b", "Receptionist", "RECEPTIONIST" },
                    { new Guid("da111f9f-21f7-4c9b-95f7-82b9a4e79969"), "82f625e3-aaa0-4f0e-b16c-8d553f3aac0c", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoId", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("d19cfb12-79ab-45da-875b-9c977c0a9f8d"), 0, "d352c37c-dc6b-4777-9428-f17f659932da", new DateTime(2023, 1, 16, 11, 8, 57, 171, DateTimeKind.Utc).AddTicks(3530), new Guid("d19cfb12-79ab-45da-875b-9c977c0a9f8d"), "admin@admin", false, false, null, "ADMIN@ADMIN", "ADMIN", "AQAAAAEAACcQAAAAEMrbti06Qfg2gyUBDBsWGe6MnX0rwNtnNc7hoNyBhdpRdSFjs0nBaBIPCksL8tuOhQ==", null, false, new Guid("00000000-0000-0000-0000-000000000000"), "f8afd8ec-7aca-49c8-af4c-5558935c46b0", 0, false, new DateTime(2023, 1, 16, 11, 8, 57, 171, DateTimeKind.Utc).AddTicks(3531), new Guid("d19cfb12-79ab-45da-875b-9c977c0a9f8d"), "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("175d3061-c995-4216-93e0-29e3555a1386"), new Guid("d19cfb12-79ab-45da-875b-9c977c0a9f8d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("712a10c4-2c72-4f78-9601-af3d617acfd2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d94b0d5-ac37-4f25-8363-1583f8f5544b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("da111f9f-21f7-4c9b-95f7-82b9a4e79969"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("175d3061-c995-4216-93e0-29e3555a1386"), new Guid("d19cfb12-79ab-45da-875b-9c977c0a9f8d") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("175d3061-c995-4216-93e0-29e3555a1386"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d19cfb12-79ab-45da-875b-9c977c0a9f8d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("18b1ca4c-698f-4dc4-8277-87e39ec25876"), "adeaf5fa-e89d-4a6c-881a-52605469dd46", "Doctor", "DOCTOR" },
                    { new Guid("4e64fcad-c355-4f7d-a7d0-1adf587380c8"), "21ceb85a-da86-4c42-8e5f-f1db697a7a55", "Patient", "Patient" },
                    { new Guid("566d1d5d-69e0-4641-86c9-fd6de27322fe"), "84d48cf2-7835-493b-803a-3a819e683611", "Receptionist", "RECEPTIONIST" },
                    { new Guid("6817b4de-ca73-49ab-a0d1-11a0b6a225d4"), "41095aff-a518-4d82-829d-7fcf3daedf66", "Admin", "ADMIN" }
                });
        }
    }
}
