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
            var adminId = new Guid("777BF98B-4EB8-4DA0-B09F-C9FED3419288");
            var adminRoleId = Guid.NewGuid();

            var patientId = new Guid("62DC0BBF-6423-41A5-8A35-EBDA51DF2EB2");
            var patientRoleId = Guid.NewGuid();

            var doctorId = new Guid("A2361776-CC5A-45C2-BDCA-390C820AB7C7");
            var doctorRoleId = Guid.NewGuid();

            var receptionistId = new Guid("1B6A0D76-F6CC-48C4-AC24-5845C5D8EBDE");
            var receptionistRoleId = Guid.NewGuid();

            SeedAccounts(builder, adminId, patientId, doctorId, receptionistId);
            SeedRoles(builder, adminRoleId, patientRoleId, doctorRoleId, receptionistRoleId);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminId,
                    RoleId = adminRoleId,
                },
                new IdentityUserRole<Guid>
                {
                    UserId = patientId,
                    RoleId = patientRoleId,
                },
                new IdentityUserRole<Guid>
                {
                    UserId = doctorId,
                    RoleId = doctorRoleId,
                },
                new IdentityUserRole<Guid>
                {
                    UserId = receptionistId,
                    RoleId = receptionistRoleId,
                });
        }

        private static void SeedRoles(ModelBuilder builder, Guid adminRoleId, Guid patientRoleId, Guid doctorRoleId, Guid receptionistRoleId)
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
                    Id = patientRoleId,
                    Name = nameof(AccountRoles.Patient),
                    NormalizedName = nameof(AccountRoles.Patient).ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = doctorRoleId,
                    Name = nameof(AccountRoles.Doctor),
                    NormalizedName = nameof(AccountRoles.Doctor).ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = receptionistRoleId,
                    Name = nameof(AccountRoles.Receptionist),
                    NormalizedName = nameof(AccountRoles.Receptionist).ToUpper(),
                });
        }
        private static void SeedAccounts(ModelBuilder builder, Guid adminId, Guid patientId, Guid doctorId, Guid receptionistId)
        {
            var admin = CreateAccount(adminId, AccountRoles.Admin, "123123");
            var patient = CreateAccount(patientId, AccountRoles.Patient, "123123");
            var doctor = CreateAccount(doctorId, AccountRoles.Doctor, "123123", AccountStatuses.AtWork);
            var receptionist = CreateAccount(receptionistId, AccountRoles.Receptionist, "123123", AccountStatuses.AtWork);

            builder.Entity<Account>().HasData(admin, patient, doctor, receptionist);
        }

        private static Account CreateAccount(Guid accountId, AccountRoles role, string password, AccountStatuses status = AccountStatuses.None)
        {
            var name = Enum.GetName(typeof(AccountRoles), role).ToLower();

            var account = new Account
            {
                Id = accountId,
                Email = $"{name}@{name}",
                NormalizedEmail = $"{name.ToUpper()}@{name.ToUpper()}",
                UserName = $"{name}@{name}",
                NormalizedUserName = $"{name.ToUpper()}@{name.ToUpper()}",
                SecurityStamp = Guid.NewGuid().ToString(),
                Status = status,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = accountId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = accountId,
            };

            var ph = new PasswordHasher<Account>();
            account.PasswordHash = ph.HashPassword(account, password);

            return account;
        }
    }
}
