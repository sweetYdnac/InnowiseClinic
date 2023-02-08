using FluentMigrator;

namespace Offices.Data.Migrations
{
    [Migration(202302010714)]
    public class InitialTables_202302010714 : Migration
    {
        public override void Down()
        {
            Delete.Table("Offices");
        }
        public override void Up()
        {
            Create.Table("Offices")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("RegistryPhoneNumber").AsString().NotNullable().Unique()
                .WithColumn("PhotoId").AsGuid().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable();
        }
    }
}
