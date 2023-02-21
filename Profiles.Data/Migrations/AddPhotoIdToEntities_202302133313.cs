using FluentMigrator;

namespace Profiles.Data.Migrations
{
    [Migration(202302133313)]
    public class AddPhotoIdToEntities_202302133313 : Migration
    {
        public override void Down()
        {
            Delete.Column("PhotoId")
                .FromTable("Patients");

            Delete.Column("PhotoId")
                .FromTable("Doctors");

            Delete.Column("PhotoId")
                .FromTable("Receptionists");
        }

        public override void Up()
        {
            Create.Column("PhotoId")
                .OnTable("Patients")
                .AsGuid()
                .Nullable()
                .WithDefaultValue(null);

            Create.Column("PhotoId")
                .OnTable("Doctors")
                .AsGuid()
                .Nullable()
                .WithDefaultValue(null);

            Create.Column("PhotoId")
                .OnTable("Receptionists")
                .AsGuid()
                .Nullable()
                .WithDefaultValue(null);
        }
    }
}
