using Appointments.Read.Application.Features.Commands.AppointmentsResults;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using Shared.Messages;

namespace Appointments.Read.API.MappingProfiles
{
    public class AppointmentResultProfile : Profile
    {
        public AppointmentResultProfile()
        {
            CreateMap<CreateAppointmentResultMessage, CreateAppointmentResultCommand>();
            CreateMap<CreateAppointmentResultCommand, AppointmentResult>();
        }
    }
}
