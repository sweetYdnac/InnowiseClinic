using Appointments.Write.Application.Features.Commands.Appointments;
using Appointments.Write.Domain.Entities;
using AutoMapper;
using Shared.Models.Request.Appointments.Appointment.SwaggerExamples;

namespace Appointments.Write.API.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentRequestExample, CreateAppointmentCommand>()
                .ForMember(command => command.Id, opt => opt.MapFrom(request => Guid.NewGuid()));

            CreateMap<CreateAppointmentCommand, Appointment>()
                .ForMember(a => a.IsApproved, opt => opt.MapFrom(command => false));
        }
    }
}
