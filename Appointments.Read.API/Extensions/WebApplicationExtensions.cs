using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Read.API.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeContext = scope.ServiceProvider.GetRequiredService<AppointmentsDbContext>();

                if (writeContext.Database.GetPendingMigrations().Any())
                {
                    writeContext.Database.Migrate();
                }
            }
        }
    }
}
