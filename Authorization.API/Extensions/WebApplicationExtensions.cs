using Authorization.Data;
using Microsoft.EntityFrameworkCore;

namespace Authorization.API.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>().Database.Migrate();
            }
        }
    }
}
