using Microsoft.EntityFrameworkCore;
using Services.Data.Contexts;

namespace Services.API.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ServicesDbContext>().Database.Migrate();
            }
        }
    }
}
