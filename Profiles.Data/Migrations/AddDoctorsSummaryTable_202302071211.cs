using FluentMigrator;
using Profiles.Data.Entities;

namespace Profiles.Data.Migrations
{
    [Migration(202302071211)]
    public class AddDoctorsSummaryTable_202302071211 : Migration
    {
        public override void Down()
        {
            Delete.Table("DoctorsSummary");
        }

        public override void Up()
        {
            Create.Table("DoctorsSummary")
                .WithColumn(nameof(User.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn("SpecializationName").AsString().NotNullable()
                .WithColumn("OfficeAddress").AsString().NotNullable();

            Insert.IntoTable("DoctorsSummary")
                .Row(new
                {
                    Id = new Guid("A2361776-CC5A-45C2-BDCA-390C820AB7C7"),
                    SpecializationName = "Therapist",
                    OfficeAddress = "Homel Belickogo 9 1"
                })
                .Row(new
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    SpecializationName = "Dentist",
                    OfficeAddress = "Minsk Test 22 10"
                });
        }
    }
}
