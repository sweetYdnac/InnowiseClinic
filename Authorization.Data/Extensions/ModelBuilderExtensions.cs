using Authorization.Data.Entities;
using Authorization.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                    Name = AccountRoles.Admin.ToString(),
                    NormalizedName = AccountRoles.Admin.ToString().ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = AccountRoles.Patient.ToString(),
                    NormalizedName = AccountRoles.Patient.ToString().ToUpper(),
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
