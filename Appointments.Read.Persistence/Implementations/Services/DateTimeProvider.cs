using Appointments.Read.Application.Interfaces.Services;

namespace Appointments.Read.Persistence.Implementations.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
        public DateTime UtcNow() => DateTime.UtcNow;
        public DateTimeOffset DateTimeOffsetNow() => DateTimeOffset.Now;
        public DateTimeOffset DateTimeOffsetUtcNow() => DateTimeOffset.UtcNow;
    }
}
