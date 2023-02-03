using FluentMigrator;
using Profiles.Domain.Entities;
using Profiles.Persistence.Helpers;

namespace Profiles.Persistence.Migrations
{
    [Migration(202302034110)]
    public class InitialTables_202302034110 : Migration
    {
        public override void Down()
        {
            Delete.Table("Patients");
            Delete.Table("Doctors");
            Delete.Table("Receptionists");
        }

        public override void Up()
        {
            Create.Table("Patients")
                .WithUserColumns()
                .WithColumn(nameof(PatientEntity.DateOfBirth)).AsDate().NotNullable()
                .WithColumn(nameof(PatientEntity.IsLinkedToAccount)).AsBoolean().NotNullable().WithDefaultValue(false);

            Create.Table("Doctors")
                .WithUserColumns()
                .WithColumn(nameof(DoctorEntity.DateOfBirth)).AsDate().NotNullable()
                .WithColumn(nameof(DoctorEntity.SpecializationId)).AsGuid().NotNullable()
                .WithColumn(nameof(DoctorEntity.OfficeId)).AsGuid().NotNullable()
                .WithColumn(nameof(DoctorEntity.CareerStartYear)).AsDate().NotNullable();

            Create.Table("Receptionists")
                .WithUserColumns()
                .WithColumn(nameof(DoctorEntity.OfficeId)).AsGuid().NotNullable();
        }
    }
}
