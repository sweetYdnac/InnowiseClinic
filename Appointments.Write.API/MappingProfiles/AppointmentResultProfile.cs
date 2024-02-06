using Appointments.Write.Application.DTOs.AppointmentResult;
using Appointments.Write.Application.Features.Commands.AppointmentsResults;
using Appointments.Write.Domain.Entities;
using AutoMapper;
using Shared.Messages;
using Shared.Models.Request.Appointments.AppointmentResult;

namespace Appointments.Write.API.MappingProfiles
{
    public class AppointmentResultProfile : Profile
    {
        public AppointmentResultProfile()
        {
            CreateMap<CreateAppointmentResultRequest, CreateAppointmentResultCommand>()
                .ForMember(entity => entity.Date, opt => opt.MapFrom(command => DateTime.Now));

            CreateMap<CreateAppointmentResultCommand, AppointmentResult>();
            CreateMap<CreateAppointmentResultCommand, CreateAppointmentResultMessage>();
            CreateMap<CreateAppointmentResultCommand, GeneratePdfMessage>();

            CreateMap<EditAppointmentResultRequest, EditAppointmentResultCommand>();
            CreateMap<EditAppointmentResultCommand, EditAppointmentResultMessage>();
            CreateMap<EditAppointmentResultCommand, EditAppointmentResultDTO>();
            CreateMap<EditAppointmentResultCommand, GeneratePdfMessage>();
        }
    }
}
