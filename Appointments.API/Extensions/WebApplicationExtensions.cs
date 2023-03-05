using Appointments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Services.API.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeContext = scope.ServiceProvider.GetRequiredService<WriteAppointmentsDbContext>();

                if (writeContext.Database.GetPendingMigrations().Any())
                {
                    writeContext.Database.Migrate();
                }

                var readContext = scope.ServiceProvider.GetRequiredService<ReadAppointmentsDbContext>();

                if (readContext.Database.GetPendingMigrations().Any())
                {
                    readContext.Database.Migrate();
                }
            }
        }
    }
}
