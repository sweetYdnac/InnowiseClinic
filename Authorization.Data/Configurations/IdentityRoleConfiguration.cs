using Authorization.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityRole<Guid>
                {
                    Id= Guid.NewGuid(),
                    Name = AccountRoles.Admin.ToString(),
                    NormalizedName = AccountRoles.Admin.ToString().ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = AccountRoles.User.ToString(),
                    NormalizedName = AccountRoles.User.ToString(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = AccountRoles.Doctor.ToString(),
                    NormalizedName = AccountRoles.Doctor.ToString().ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = AccountRoles.Receptionist.ToString(),
                    NormalizedName = AccountRoles.Receptionist.ToString().ToUpper(),
                }
            );
        }
    }
}
