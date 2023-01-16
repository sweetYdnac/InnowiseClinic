using Authorization.Data.Entities;
using Authorization.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data
{
    public class AuthorizationDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.SeedData();
        }
    }
}
