using FluentMigrator;
using Profiles.Data.Entities;

namespace Profiles.Data.Migrations
{
    [Migration(202302091017)]
    public class AddReceptionistsSymmaryTable_202302091017 : Migration
    {
        public override void Down()
        {
            Delete.Table("ReceptionistsSummary");
        }

        public override void Up()
        {
            Create.Table("ReceptionistsSummary")
                .WithColumn(nameof(User.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn("OfficeAddress").AsString().NotNullable();

            Insert.IntoTable("ReceptionistsSummary")
                .Row(new
                {
                    Id = new Guid("1B6A0D76-F6CC-48C4-AC24-5845C5D8EBDE"),
                    OfficeAddress = "Homel Belickogo 9 1"
                })
                .Row(new
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    OfficeAddress = "Minsk Test 22 10"
                });
        }
    }
}
