using Authorization.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<T_Account> Accounts { get; set; }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
            : base(options)
        {
        }
    }
}