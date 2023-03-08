using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Domain.Entities;
using Appointments.Write.Persistence.Contexts;

namespace Appointments.Write.Persistence.Implementations.Repositories
{
    public class AppointmentsResultsRepository : Repository<AppointmentResult>, IAppointmentsResultsRepository
    {
        public AppointmentsResultsRepository(AppointmentsDbContext database)
            : base(database) { }
    }
}
