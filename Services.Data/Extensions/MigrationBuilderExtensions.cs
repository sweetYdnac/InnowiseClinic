using Microsoft.EntityFrameworkCore;
using Services.Data.Entities;

namespace Services.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<Specialization>()
                .HasData(
                new Specialization
                {
                    Id = new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                    Title = "Dentist",
                    IsActive = true,
                },
                new Specialization
                {
                    Id = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                    Title = "Therapist",
                    IsActive = false,
                });

            builder.Entity<ServiceCategory>()
                .HasData(
                new ServiceCategory
                {
                    Id = new Guid("99540760-4527-4511-8851-5B882D921E0A"),
                    Title = "Analyses",
                    TimeSlotSize = 10,
                },
                new ServiceCategory
                {
                    Id = new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25"),
                    Title = "Consultation",
                    TimeSlotSize = 20,
                },
                new ServiceCategory
                {
                    Id = new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4"),
                    Title = "Diagnostics",
                    TimeSlotSize = 30,
                });

            builder.Entity<Service>()
                .HasData(
                new Service
                {
                    Id = Guid.NewGuid(),
                    Title = "Filling",
                    Price = 50,
                    IsActive = true,
                    SpecializationId = new Guid("F92BB223-1A2B-420C-B19D-EEB4191DB06B"),
                    CategoryId = new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4")
                },
                new Service
                {
                    Id = Guid.NewGuid(),
                    Title = "Examination",
                    Price = 20,
                    IsActive = false,
                    SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                    CategoryId = new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25")
                });
        }
    }
}
