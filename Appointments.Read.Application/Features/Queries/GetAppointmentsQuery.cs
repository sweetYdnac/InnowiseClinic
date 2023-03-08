using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Models.Response;
using Shared.Models.Response.Appointments.Appointment;
using System.Linq.Expressions;

namespace Appointments.Read.Application.Features.Queries
{
    public class GetAppointmentsQuery : IRequest<PagedResponse<AppointmentResponse>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public DateOnly Date { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public Guid? OfficeId { get; set; }
        public bool IsApproved { get; set; }
    }

    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, PagedResponse<AppointmentResponse>>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<PagedResponse<AppointmentResponse>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var response = await _appointmentsRepository.GetAppointments(
                request.CurrentPage,
                request.PageSize,
                new Dictionary<Expression<Func<Appointment, object>>, bool>()
                {
                    { appointment => appointment.Time, true },
                    { appointment => appointment.DoctorFullName, true },
                    { appointment => appointment.ServiceName, true },
                },
                new Expression<Func<Appointment, bool>>[]
                {
                    appointment => appointment.Date.Equals(request.Date),
                    appointment => appointment.OfficeId.Equals(request.OfficeId),
                    appointment => appointment.IsApproved.Equals(request.IsApproved),
                    appointment => appointment.DoctorFullName.Contains(request.DoctorFullName, StringComparison.OrdinalIgnoreCase),
                    appointment => appointment.ServiceName.Contains(request.ServiceName, StringComparison.OrdinalIgnoreCase),
                });

            return new PagedResponse<AppointmentResponse>(
                _mapper.Map<IEnumerable<AppointmentResponse>>(response.Items),
                request.CurrentPage,
                request.PageSize,
                response.TotalCount);
        }
    }
}
