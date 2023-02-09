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
                    Id = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
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
