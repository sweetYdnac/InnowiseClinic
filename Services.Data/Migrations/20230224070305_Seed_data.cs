using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Title", "IsActive" },
                values: new object[,]
                {
                    { new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B") , "Dentist" , true },
                    { new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"), "Therapist", false }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Title", "TimeSlotSize" },
                values: new object[,]
                {
                    { new Guid("99540760-4527-4511-8851-5B882D921E0A"), "Analyses", 10 },
                    { new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25"), "Consultation", 20 },
                    { new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4"), "Diagnostics", 30 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Title", "Price", "IsActive", "SpecializationId", "CategoryId" },
                values: new object[,]
                {
                    {
                        new Guid("EBBC7A6C-21C7-4049-B68A-544056861D45"),
                        "Filling",
                        50,
                        true,
                        new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                        new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4")
                    },
                    {
                        new Guid("CD0D073B-ACC8-4CCF-8119-9EC909ED70ED"),
                        "Examination",
                        20,
                        false,
                        new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                        new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25")
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            #region Specializations
            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"));

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"));
            #endregion

            #region ServiceCategories
            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("99540760-4527-4511-8851-5B882D921E0A"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4"));
            #endregion

            #region Services
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("EBBC7A6C-21C7-4049-B68A-544056861D45"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("CD0D073B-ACC8-4CCF-8119-9EC909ED70ED"));
            #endregion
        }
    }
}
