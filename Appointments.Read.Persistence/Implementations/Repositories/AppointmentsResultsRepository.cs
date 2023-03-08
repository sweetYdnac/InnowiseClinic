using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Read.Persistence.Implementations.Repositories
{
    public class AppointmentsResultsRepository : Repository<AppointmentResult>, IAppointmentsResultsRepository
    {
        public AppointmentsResultsRepository(AppointmentsDbContext database)
            : base(database) { }

    }
}
