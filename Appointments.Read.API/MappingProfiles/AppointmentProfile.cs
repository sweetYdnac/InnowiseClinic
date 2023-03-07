using Appointments.Read.Application.Features.Commands;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using Shared.Messages;

namespace Appointments.Read.API.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentMessage, CreateAppointmentCommand>();
            CreateMap<CreateAppointmentCommand, Appointment>()
                .ForMember(entity => entity.IsApproved, opt => opt.MapFrom(command => false));

            CreateMap<UpdatePatientMessage, UpdatePatientCommand>();
            CreateMap<UpdateDoctorMessage, UpdateDoctorCommand>();
            CreateMap<UpdateServiceMessage, UpdateServiceCommand>();


            CreateMap<RescheduleAppointmentMessage, RescheduleAppointmentCommand>();
        }
    }
}
