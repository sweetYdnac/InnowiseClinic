using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Read.Application.Interfaces.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now();
        DateTime UtcNow();
        DateTimeOffset DateTimeOffsetNow();
        DateTimeOffset DateTimeOffsetUtcNow();
    }
}
