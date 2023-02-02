using FluentMigrator;
using Offices.Domain.Entities;

namespace Offices.Persistence.Migrations
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
                    PhotoId = new Guid("842AE760-69CB-4240-B9C5-D5C601407D42"),
                    IsActive = true,
                })
                .Row(new OfficeEntity
                {
                    Id = new Guid("864FF8C2-56C6-49CD-A8FF-BA827FF5B91C"),
                    Address = "Minsk Test 22 10",
                    RegistryPhoneNumber = "2281337",
                    PhotoId = new Guid("29845BD3-7423-4E34-9F46-0B76E18122E5"),
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
                    PhotoId = new Guid("842AE760-69CB-4240-B9C5-D5C601407D42"),
                    IsActive = true,
                })
                .Row(new OfficeEntity
                {
                    Id = new Guid("864FF8C2-56C6-49CD-A8FF-BA827FF5B91C"),
                    Address = "Minsk Test 22 10",
                    RegistryPhoneNumber = "2281337",
                    PhotoId = new Guid("29845BD3-7423-4E34-9F46-0B76E18122E5"),
                    IsActive = false,
                });
        }
    }
}
