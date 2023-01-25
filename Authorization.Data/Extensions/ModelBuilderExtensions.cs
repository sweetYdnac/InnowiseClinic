using Authorization.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Enums;

namespace Authorization.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder builder)
        {
            var adminId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();

            SeedRoles(builder, adminRoleId);
            SeedAccounts(builder, adminId);           

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminId,
                    RoleId = adminRoleId,
                });
        }

        private static void SeedRoles(ModelBuilder builder, Guid adminRoleId)
        {
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = adminRoleId,
                    Name = nameof(AccountRoles.Admin),
                    NormalizedName = nameof(AccountRoles.Admin).ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(AccountRoles.Patient),
                    NormalizedName = nameof(AccountRoles.Patient).ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(AccountRoles.Doctor),
                    NormalizedName = nameof(AccountRoles.Doctor).ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(AccountRoles.Receptionist),
                    NormalizedName = nameof(AccountRoles.Receptionist).ToUpper(),
                });
        }
        private static void SeedAccounts(ModelBuilder builder, Guid adminId)
        {
            var admin = new Account
            {
                Id = adminId,
                Email = "admin@admin",
                NormalizedEmail = "ADMIN@ADMIN",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString(),
                Status = AccountStatuses.None,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = adminId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = adminId,
            };

            var ph = new PasswordHasher<Account>();
            admin.PasswordHash = ph.HashPassword(admin, "123123");

            builder.Entity<Account>().HasData(admin);
        }
    }
}
