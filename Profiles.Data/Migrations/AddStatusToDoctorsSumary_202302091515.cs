using FluentMigrator;
using Shared.Core.Enums;

namespace Profiles.Data.Migrations
{
    [Migration(202302091515)]
    public class AddStatusToDoctorsSumary_202302091515 : Migration
    {
        public override void Down()
        {
            Delete.Column("Status")
                .FromTable("DoctorsSummary");
        }

        public override void Up()
        {
            Create.Column("Status")
                .OnTable("DoctorsSummary")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue((int)AccountStatuses.AtWork);
        }
    }
}
