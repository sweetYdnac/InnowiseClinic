﻿using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Write.Application.Features.Commands.Appointments;
using Appointments.Write.Domain.Entities;
using AutoMapper;
using Shared.Messages;
using Shared.Models.Request.Appointments.Appointment;
using Shared.Models.Response.Appointments.Appointment;

namespace Appointments.Write.API.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentRequest, CreateAppointmentCommand>()
                .ForMember(command => command.Id, opt => opt.MapFrom(request => Guid.NewGuid()));
            CreateMap<CreateAppointmentCommand, Appointment>()
                .ForMember(a => a.IsApproved, opt => opt.MapFrom(command => false));
            CreateMap<CreateAppointmentCommand, CreateAppointmentMessage>();

            CreateMap<RescheduleAppointmentRequest, RescheduleAppointmentCommand>();
            CreateMap<RescheduleAppointmentCommand, RescheduleAppointmentMessage>();

            CreateMap<AppointmentHistoryDTO, AppointmentHistoryResponse>();

        }
    }
}
