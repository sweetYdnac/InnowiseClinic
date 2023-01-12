using Authorization.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data
{
    public class AuthorizationDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public Guid UserId { get; set; }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
            : base(options) { }
    }
}
