using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Features.Commands.AppointmentsResults;
using Appointments.Read.Application.Features.Queries.AppointmentsResults;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using Shared.Messages;
using Shared.Models.Request.Appointments.AppointmentResult;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.API.MappingProfiles
{
    public class AppointmentResultProfile : Profile
    {
        public AppointmentResultProfile()
        {
            CreateMap<CreateAppointmentResultMessage, CreateAppointmentResultCommand>();
            CreateMap<CreateAppointmentResultCommand, AppointmentResult>();

            CreateMap<EditAppointmentResultMessage, EditAppointmentResultCommand>();
            CreateMap<EditAppointmentResultCommand, EditAppointmentResultDTO>();

            CreateMap<AppointmentResultDTO, AppointmentResultResponse>();

            CreateMap<GetPdfResultRequest, GetPdfResultQuery>();
            CreateMap<GetPdfResultQuery, PdfResultDTO>();
        }
    }
}
