﻿using Appointments.Write.Application.Features.Commands.Appointments;
using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Domain.Entities;
using Appointments.Write.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Write.Persistence.Implementations.Repositories
{
    public class AppointmentsRepository : Repository<Appointment>, IAppointmentsRepository
    {
        public AppointmentsRepository(AppointmentsDbContext database)
            : base(database) { }

        public async Task<int> RescheduleAppointment(RescheduleAppointmentCommand command)
        {
            return await DbSet
                .Where(a => a.Id.Equals(command.Id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.DoctorId, a => command.DoctorId)
                    .SetProperty(a => a.Date, a => command.Date)
                    .SetProperty(a => a.Time, a => command.Time));
        }
    }
}
