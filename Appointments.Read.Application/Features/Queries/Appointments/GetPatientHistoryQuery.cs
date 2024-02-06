using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Models.Response;
using Shared.Models.Response.Appointments.Appointment;
using System.Linq.Expressions;

namespace Appointments.Read.Application.Features.Queries.Appointments
{
    public class GetPatientHistoryQuery : IRequest<PagedResponse<AppointmentHistoryResponse>>
    {
        public Guid PatientId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool IsFinished { get; set; }
    }

    public class GetPatientHistoryQueryHandler : IRequestHandler<GetPatientHistoryQuery, PagedResponse<AppointmentHistoryResponse>>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public GetPatientHistoryQueryHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<PagedResponse<AppointmentHistoryResponse>> Handle(GetPatientHistoryQuery request, CancellationToken cancellationToken)
        {
            var response = await _appointmentsRepository.GetAppointmentHistoryAsync(
                request.CurrentPage,
                request.PageSize,
                new Expression<Func<Appointment, object>>[]
                {
                    appointment => appointment.AppointmentResult,
                },
                new Dictionary<Expression<Func<Appointment, object>>, bool>()
                {
                    { appointment => appointment.Date, false },
                    { appointment => appointment.Time, true },
                },
                new Expression<Func<Appointment, bool>>[]
                {
                    appointment => appointment.PatientId.Equals(request.PatientId),
                    appointment => !request.IsFinished || !appointment.AppointmentResult.Equals(null)
                });

            return new PagedResponse<AppointmentHistoryResponse>(
                _mapper.Map<IEnumerable<AppointmentHistoryResponse>>(response.Items),
                request.CurrentPage,
                request.PageSize,
                response.TotalCount);
        }
    }
}
