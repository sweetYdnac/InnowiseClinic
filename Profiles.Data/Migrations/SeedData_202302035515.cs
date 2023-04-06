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
                    Id = new Guid("62DC0BBF-6423-41A5-8A35-EBDA51DF2EB2"),
                    FirstName = "Evgeny",
                    LastName = "Koreba",
                    MiddleName = "Sweety",
                    DateOfBirth = new DateOnly(1999, 10, 15),
                })
                .Row(new
                {
                    Id = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    FirstName = "Alex",
                    LastName = "Lorem",
                    MiddleName = "",
                    DateOfBirth = new DateOnly(1999, 10, 15),
                });

            Delete.FromTable("Doctors")
                .Row(new
                {
                    Id = new Guid("A2361776-CC5A-45C2-BDCA-390C820AB7C7"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629"),
                    CareerStartYear = 2020,
                })
                .Row(new
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c"),
                    CareerStartYear = 2020,
                });

            Delete.FromTable("Receptionists")
                .Row(new
                {
                    Id = new Guid("1B6A0D76-F6CC-48C4-AC24-5845C5D8EBDE"),
                    FirstName = "123",
                    LastName = "qwe",
                    MiddleName = "asd",
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629")
                })
                .Row(new
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    FirstName = "321",
                    LastName = "eqw",
                    MiddleName = "dsa",
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c")
                });
        }

        public override void Up()
        {
            Insert.IntoTable("Patients")
                .Row(new
                {
                    Id = new Guid("62DC0BBF-6423-41A5-8A35-EBDA51DF2EB2"),
                    FirstName = "Evgeny",
                    LastName = "Koreba",
                    MiddleName = "Sweety",
                    DateOfBirth = new DateTime(1999, 10, 15),
                })
                .Row(new
                {
                    Id = new Guid("EA1AFB83-5DA9-4B81-AD94-B6A62EB25D43"),
                    FirstName = "Alex",
                    LastName = "Lorem",
                    MiddleName = "",
                    DateOfBirth = new DateTime(1999, 10, 15),
                });

            Insert.IntoTable("Doctors")
                .Row(new
                {
                    Id = new Guid("A2361776-CC5A-45C2-BDCA-390C820AB7C7"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629"),
                    CareerStartYear = 2020,
                })
                .Row(new
                {
                    Id = new Guid("96C91BEE-3B1D-48B0-ABAE-116BEBBA3EFB"),
                    FirstName = "Test",
                    LastName = "Test",
                    MiddleName = "",
                    DateOfBirth = new DateTime(1999, 10, 15),
                    SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c"),
                    CareerStartYear = 2020,
                });

            Insert.IntoTable("Receptionists")
                .Row(new
                {
                    Id = new Guid("1B6A0D76-F6CC-48C4-AC24-5845C5D8EBDE"),
                    FirstName = "123",
                    LastName = "qwe",
                    MiddleName = "asd",
                    OfficeId = new Guid("09f72ba6-fb72-4b76-be2e-549d45296629")
                })
                .Row(new
                {
                    Id = new Guid("15363968-9924-4FEC-8A6C-B843CC4524FC"),
                    FirstName = "321",
                    LastName = "eqw",
                    MiddleName = "dsa",
                    OfficeId = new Guid("864ff8c2-56c6-49cd-a8ff-ba827ff5b91c")
                });
        }
    }
}
