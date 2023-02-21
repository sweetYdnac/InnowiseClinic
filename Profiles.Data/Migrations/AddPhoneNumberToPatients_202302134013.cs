using FluentMigrator;

namespace Profiles.Data.Migrations
{
    [Migration(202302134013)]
    public class AddPhoneNumberToPatients_202302134013 : Migration
    {
        public override void Down()
        {
            Delete.Column("PhoneNumber")
                .FromTable("Patients");
        }

        public override void Up()
        {
            Create.Column("PhoneNumber")
                .OnTable("Patients")
                .AsString()
                .Nullable()
                .WithDefaultValue(null);
        }
    }
}
