using FluentMigrator;

namespace Profiles.Data.Migrations
{
    [Migration(202302035515)]
    public class SeedData_202302035515 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Patients")
                .Row(new
                {
                    Id = new Guid("B2957690-4D76-468C-A449-FB9529283857"),
                    FirstName = "Evgeny",
                    LastName = "Koreba",
                    MiddleName = "Sweety",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                })
                .Row(new
                {
                    Id = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    FirstName = "Alex",
                    LastName = "Lorem",
                    MiddleName = "",
                    AccountId = new Guid("{2730FDE4-FD40-405C-9AB7-799BF189FA98}"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                });

            Delete.FromTable("Doctors")
                .Row(new
                {
                    Id = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                })
                .Row(new
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("1EB7437B-65AB-4F9D-8227-7EAB168DE4FC"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                });

            Delete.FromTable("Receptionists")
                .Row(new
                {
                    Id = new Guid("A315F9D1-C385-4665-920C-D03896D626CA"),
                    FirstName = "123",
                    LastName = "qwe",
                    MiddleName = "asd",
                    AccountId = new Guid("D872356D-EF84-4D80-83A8-F625969F1E0D"),
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629")
                })
                .Row(new
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    FirstName = "321",
                    LastName = "eqw",
                    MiddleName = "dsa",
                    AccountId = new Guid("54E8E973-D257-439C-B952-03BDE03A7E1A"),
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c")
                });
        }

        public override void Up()
        {
            Insert.IntoTable("Patients")
                .Row(new
                {
                    Id = new Guid("B2957690-4D76-468C-A449-FB9529283857"),
                    FirstName = "Evgeny",
                    LastName = "Koreba",
                    MiddleName = "Sweety",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                })
                .Row(new
                {
                    Id = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    FirstName = "Alex",
                    LastName = "Lorem",
                    MiddleName = "",
                    AccountId = new Guid("{2730FDE4-FD40-405C-9AB7-799BF189FA98}"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    IsLinkedToAccount = false
                });

            Insert.IntoTable("Doctors")
                .Row(new
                {
                    Id = new Guid("835CD971-9F41-4A81-A477-B88171671639"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("F2B88C6B-FDF7-4CE4-A799-9DBCBB82D8B0"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                })
                .Row(new
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    AccountId = new Guid("1EB7437B-65AB-4F9D-8227-7EAB168DE4FC"),
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c"),
                    CareerStartYear = new DateTime(1999, 10, 15),
                });

            Insert.IntoTable("Receptionists")
                .Row(new
                {
                    Id = new Guid("A315F9D1-C385-4665-920C-D03896D626CA"),
                    FirstName = "123",
                    LastName = "qwe",
                    MiddleName = "asd",
                    AccountId = new Guid("D872356D-EF84-4D80-83A8-F625969F1E0D"),
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629")
                })
                .Row(new
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    FirstName = "321",
                    LastName = "eqw",
                    MiddleName = "dsa",
                    AccountId = new Guid("54E8E973-D257-439C-B952-03BDE03A7E1A"),
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c")
                });
        }
    }
}
