using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Data.Entities;

namespace Services.Data.Configurations
{
    internal class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(s => s.Title)
                .IsRequired();

            builder.HasIndex(s => s.Title)
                .IsUnique();

            builder.Property(s => s.IsActive)
                .IsRequired();
        }
    }
}
