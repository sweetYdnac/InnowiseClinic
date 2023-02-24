using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Data.Entities;

namespace Services.Data.Configurations
{
    internal class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Title)
                .IsRequired();

            builder.HasIndex(s => s.Title)
                .IsUnique();

            builder.Property(c => c.TimeSlotSize)
                .IsRequired();

            builder.ToTable(c => c.HasCheckConstraint(
                "CHK_ServiceCategories_TimeSlotSize",
                "TimeSlotSize > 0"));
        }
    }
}
