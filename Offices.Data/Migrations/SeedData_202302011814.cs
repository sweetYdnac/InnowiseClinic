using FluentMigrator;
using Offices.Data.Entities;

namespace Offices.Data.Migrations
{
    [Migration(202302011814)]
    public class SeedData_202302011814 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Offices")
                .Row(new OfficeEntity
                {
                    Id = new Guid("09F72BA6-FB72-4B76-BE2E-549D45296629"),
                    Address = "Homel Belickogo 9 1",
                    RegistryPhoneNumber = "88005553535",
                    PhotoId = null,
                    IsActive = true,
                })
                .Row(new OfficeEntity
                {
                    Id = new Guid("864FF8C2-56C6-49CD-A8FF-BA827FF5B91C"),
                    Address = "Minsk Test 22 10",
                    RegistryPhoneNumber = "2281337",
                    PhotoId = null,
                    IsActive = false,
                });
        }
        public override void Up()
        {
            Insert.IntoTable("Offices")
                .Row(new OfficeEntity
                {
                    Id = new Guid("09F72BA6-FB72-4B76-BE2E-549D45296629"),
                    Address = "Homel Belickogo 9 1",
                    RegistryPhoneNumber = "88005553535",
                    PhotoId = null,
                    IsActive = true,
                })
                .Row(new OfficeEntity
                {
                    Id = new Guid("864FF8C2-56C6-49CD-A8FF-BA827FF5B91C"),
                    Address = "Minsk Test 22 10",
                    RegistryPhoneNumber = "2281337",
                    PhotoId = null,
                    IsActive = false,
                });
        }
    }
}
