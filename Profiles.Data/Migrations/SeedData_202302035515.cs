using FluentMigrator;
using Profiles.Data.Entities;

namespace Profiles.Data.Migrations
{
    [Migration(202302035515)]
    public class SeedData_202302035515 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Patients")
                .Row(new PatientEntity
                {
                    Id = new Guid("B2957690-4D76-468C-A449-FB9529283857"),
                    FirstName = "Evgeny",
                    LastName = "Koreba",
                    MiddleName = "Sweety",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                })
                .Row(new PatientEntity
                {
                    Id = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    FirstName = "Alex",
                    LastName = "Lorem",
                    MiddleName = null,
                    AccountId = new Guid("{2730FDE4-FD40-405C-9AB7-799BF189FA98}"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                });

            Delete.FromTable("Doctors")
                .Row(new DoctorEntity
                {
                    Id = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("5F089A2C-C700-49DD-A1DD-65D6FC75A278"),
                    OfficeId = new Guid("FC489D02-19A0-4D3C-B790-297FA9B4A143"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                })
                .Row(new DoctorEntity
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("1EB7437B-65AB-4F9D-8227-7EAB168DE4FC"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("A5AFC31F-392D-4D41-A9CF-80FD7FE9F911"),
                    OfficeId = new Guid("A8EA6EF0-0724-4580-AED1-093F1F7B8F54"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                });

            Delete.FromTable("Receptionists")
                .Row(new ReceptionistEntity
                {
                    Id = new Guid("A315F9D1-C385-4665-920C-D03896D626CA"),
                    FirstName = "123",
                    LastName = "qwe",
                    MiddleName = "asd",
                    AccountId = new Guid("D872356D-EF84-4D80-83A8-F625969F1E0D"),
                    OfficeId = new Guid("187311D1-86D3-4DDC-BFA0-4AE85FAD6C05")
                })
                .Row(new ReceptionistEntity
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    FirstName = "321",
                    LastName = "eqw",
                    MiddleName = "dsa",
                    AccountId = new Guid("54E8E973-D257-439C-B952-03BDE03A7E1A"),
                    OfficeId = new Guid("090643DE-7B12-4FCC-80C7-BCC20BA3A57C")
                });
        }

        public override void Up()
        {
            Insert.IntoTable("Patients")
                .Row(new PatientEntity
                {
                    Id = new Guid("B2957690-4D76-468C-A449-FB9529283857"),
                    FirstName = "Evgeny",
                    LastName = "Koreba",
                    MiddleName = "Sweety",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                })
                .Row(new PatientEntity
                {
                    Id = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    FirstName = "Alex",
                    LastName = "Lorem",
                    MiddleName = null,
                    AccountId = new Guid("{2730FDE4-FD40-405C-9AB7-799BF189FA98}"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                });

            Insert.IntoTable("Doctors")
                .Row(new DoctorEntity
                {
                    Id = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("5F089A2C-C700-49DD-A1DD-65D6FC75A278"),
                    OfficeId = new Guid("FC489D02-19A0-4D3C-B790-297FA9B4A143"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                })
                .Row(new DoctorEntity
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("1EB7437B-65AB-4F9D-8227-7EAB168DE4FC"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("A5AFC31F-392D-4D41-A9CF-80FD7FE9F911"),
                    OfficeId = new Guid("A8EA6EF0-0724-4580-AED1-093F1F7B8F54"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                });

            Insert.IntoTable("Receptionists")
                .Row(new ReceptionistEntity
                {
                    Id = new Guid("A315F9D1-C385-4665-920C-D03896D626CA"),
                    FirstName = "123",
                    LastName = "qwe",
                    MiddleName = "asd",
                    AccountId = new Guid("D872356D-EF84-4D80-83A8-F625969F1E0D"),
                    OfficeId = new Guid("187311D1-86D3-4DDC-BFA0-4AE85FAD6C05")
                })
                .Row(new ReceptionistEntity
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    FirstName = "321",
                    LastName = "eqw",
                    MiddleName = "dsa",
                    AccountId = new Guid("54E8E973-D257-439C-B952-03BDE03A7E1A"),
                    OfficeId = new Guid("090643DE-7B12-4FCC-80C7-BCC20BA3A57C")
                });
        }
    }
}
