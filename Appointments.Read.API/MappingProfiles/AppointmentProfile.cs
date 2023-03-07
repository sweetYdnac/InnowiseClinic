using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Features.Commands;
using Appointments.Read.Application.Features.Queries;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using Shared.Messages;
using Shared.Models.Request.Appointments.Appointment;
using Shared.Models.Response.Appointments.Appointment;

namespace Appointments.Read.API.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentMessage, CreateAppointmentCommand>();
            CreateMap<CreateAppointmentCommand, Appointment>()
                .ForMember(entity => entity.IsApproved, opt => opt.MapFrom(command => false));
            CreateMap<RescheduleAppointmentMessage, RescheduleAppointmentCommand>();
            CreateMap<CancelAppointmentMessage, CancelAppointmentCommand>();
            CreateMap<ApproveAppointmentMessage, ApproveAppointmentCommand>();

            CreateMap<UpdatePatientMessage, UpdatePatientCommand>();
            CreateMap<UpdateDoctorMessage, UpdateDoctorCommand>();
            CreateMap<UpdateServiceMessage, UpdateServiceCommand>();

            CreateMap<GetDoctorScheduleRequest, GetDoctorScheduleQuery>();
            CreateMap<DoctorScheduledAppointmentDTO, DoctorScheduledAppointmentResponse>();
        }
    }
}
