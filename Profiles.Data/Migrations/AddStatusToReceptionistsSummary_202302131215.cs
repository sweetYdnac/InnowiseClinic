using FluentMigrator;
using Shared.Core.Enums;

namespace Profiles.Data.Migrations
{
    [Migration(202302131215)]
    public class AddStatusToReceptionistsSummary_202302131215 : Migration
    {
        public override void Down()
        {
            Delete.Column("Status")
                .FromTable("ReceptionistsSummary");
        }

        public override void Up()
        {
            Create.Column("Status")
                .OnTable("ReceptionistsSummary")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue((int)AccountStatuses.Inactive);
        }
    }
}
