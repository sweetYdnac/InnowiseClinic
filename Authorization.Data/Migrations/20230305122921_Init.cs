using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0d8adf50-1a0e-4789-b72e-ee7b5b2daed8"), null, "Admin", "ADMIN" },
                    { new Guid("27346c79-7fb3-4aa1-b844-5acbb7b73ef2"), null, "Patient", "PATIENT" },
                    { new Guid("3fe3ac0b-3e5c-47b1-afc7-e931e3fefe05"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("5513b382-4cdb-4790-ab24-1d3888b45eac"), null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[,]
                {
                    { new Guid("1b6a0d76-f6cc-48c4-ac24-5845c5d8ebde"), 0, "a585563e-4fc9-47d0-912d-ddbbac5fcc3b", new DateTime(2023, 3, 5, 12, 29, 20, 787, DateTimeKind.Utc).AddTicks(9346), new Guid("1b6a0d76-f6cc-48c4-ac24-5845c5d8ebde"), "receptionist@receptionist", false, false, null, "RECEPTIONIST@RECEPTIONIST", "RECEPTIONIST@RECEPTIONIST", "AQAAAAIAAYagAAAAEEmmzqoecKpxNKY/6dqLXDupYIunNmuL3d9RZRLQX7xRGqes4E7u6/sOo7U/G3CS0w==", null, false, "69022992-1e1a-4d83-8e9c-12070797ad95", 1, false, new DateTime(2023, 3, 5, 12, 29, 20, 787, DateTimeKind.Utc).AddTicks(9349), new Guid("1b6a0d76-f6cc-48c4-ac24-5845c5d8ebde"), "receptionist@receptionist" },
                    { new Guid("62dc0bbf-6423-41a5-8a35-ebda51df2eb2"), 0, "f1d9ca8f-220d-4ae6-b69e-c2c348d91c63", new DateTime(2023, 3, 5, 12, 29, 20, 651, DateTimeKind.Utc).AddTicks(9058), new Guid("62dc0bbf-6423-41a5-8a35-ebda51df2eb2"), "patient@patient", false, false, null, "PATIENT@PATIENT", "PATIENT@PATIENT", "AQAAAAIAAYagAAAAEKZCKFAkiXAZrR5Fz+7Y9E6pqF0TwA4JxpJ9OIYLIDG3YKcn21sRsRaxsMPiGsVOgg==", null, false, "8d53958c-75a1-4ac6-b55f-0a155522ce4f", 0, false, new DateTime(2023, 3, 5, 12, 29, 20, 651, DateTimeKind.Utc).AddTicks(9060), new Guid("62dc0bbf-6423-41a5-8a35-ebda51df2eb2"), "patient@patient" },
                    { new Guid("777bf98b-4eb8-4da0-b09f-c9fed3419288"), 0, "2ee77f4e-96ec-433a-b9c9-15d9ce0fb643", new DateTime(2023, 3, 5, 12, 29, 20, 583, DateTimeKind.Utc).AddTicks(9520), new Guid("777bf98b-4eb8-4da0-b09f-c9fed3419288"), "admin@admin", false, false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "AQAAAAIAAYagAAAAEC8pMr0Es5Q5RYvABzahj8QRxW9xNlO+Onb6Srptwup8C2Zcuo/Y6KiWG4uycnz+mA==", null, false, "f14a4646-4b7f-42f9-a0fe-0bd60a944a2d", 0, false, new DateTime(2023, 3, 5, 12, 29, 20, 583, DateTimeKind.Utc).AddTicks(9522), new Guid("777bf98b-4eb8-4da0-b09f-c9fed3419288"), "admin@admin" },
                    { new Guid("a2361776-cc5a-45c2-bdca-390c820ab7c7"), 0, "1b729fe4-ff5c-4bc1-92a2-fbd0727d1331", new DateTime(2023, 3, 5, 12, 29, 20, 719, DateTimeKind.Utc).AddTicks(6706), new Guid("a2361776-cc5a-45c2-bdca-390c820ab7c7"), "doctor@doctor", false, false, null, "DOCTOR@DOCTOR", "DOCTOR@DOCTOR", "AQAAAAIAAYagAAAAECECxob66wSt6E4sEhS49kUScbN8XpHgbo92sLUc9WMZcUXfTk5RPvYEd3I2NrNmIQ==", null, false, "5f4102cc-86b6-4596-a26c-9c9aab4515ab", 1, false, new DateTime(2023, 3, 5, 12, 29, 20, 719, DateTimeKind.Utc).AddTicks(6708), new Guid("a2361776-cc5a-45c2-bdca-390c820ab7c7"), "doctor@doctor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3fe3ac0b-3e5c-47b1-afc7-e931e3fefe05"), new Guid("1b6a0d76-f6cc-48c4-ac24-5845c5d8ebde") },
                    { new Guid("27346c79-7fb3-4aa1-b844-5acbb7b73ef2"), new Guid("62dc0bbf-6423-41a5-8a35-ebda51df2eb2") },
                    { new Guid("0d8adf50-1a0e-4789-b72e-ee7b5b2daed8"), new Guid("777bf98b-4eb8-4da0-b09f-c9fed3419288") },
                    { new Guid("5513b382-4cdb-4790-ab24-1d3888b45eac"), new Guid("a2361776-cc5a-45c2-bdca-390c820ab7c7") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
