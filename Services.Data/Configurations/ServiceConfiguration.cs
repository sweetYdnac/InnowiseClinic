using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Data.Entities;

namespace Services.Data.Configurations
{
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(s => s.Title)
                .IsRequired();

            builder.HasIndex(s => s.Title)
                .IsUnique();

            builder.Property(s => s.Price)
                .HasColumnType("money")
                .IsRequired();

            builder.ToTable(c => c.HasCheckConstraint(
                "CHK_Services_Price",
                "Price >= 0"));

            builder.Property(s => s.IsActive)
                .IsRequired();

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Specialization)
                .WithMany(s => s.Services)
                .HasForeignKey(s => s.SpecializationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
