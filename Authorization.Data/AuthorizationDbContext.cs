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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            var added = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in added)
            {
                if (entity is ITrackable trackable)
                {
                    trackable.CreatedAt = DateTime.Now;
                    trackable.CreatedBy = UserId;
                    trackable.UpdatedAt = DateTime.Now;
                    trackable.UpdatedBy = UserId;
                }
            }

            var modified = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in modified)
            {
                if (entity is ITrackable trackable)
                {
                    trackable.UpdatedAt = DateTime.Now;
                    trackable.UpdatedBy = UserId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        } 
    }
}
