using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Models.Response;
using Shared.Models.Response.Appointments.Appointment;
using System.Linq.Expressions;

namespace Appointments.Read.Application.Features.Queries
{
    public class GetDoctorScheduleQuery : IRequest<PagedResponse<DoctorScheduledAppointmentResponse>>
    {
        public Guid DoctorId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public DateOnly Date { get; set; }
    }

    public class GetDoctorScheduleQueryHandler :
        IRequestHandler<GetDoctorScheduleQuery, PagedResponse<DoctorScheduledAppointmentResponse>>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public GetDoctorScheduleQueryHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<PagedResponse<DoctorScheduledAppointmentResponse>> Handle(
            GetDoctorScheduleQuery request, CancellationToken cancellationToken)
        {
            var response = await _appointmentsRepository.GetDoctorScheduleAsync(
                request.CurrentPage,
                request.PageSize,
                new Expression<Func<Appointment, object>>[]
                {
                    appointment => appointment.AppointmentResult,
                },
                new(Expression<Func<Appointment, object>> keySelector, bool isAscending)[]
                {
                    new(appointment => appointment.Time, true)
                },
                new Expression<Func<Appointment, bool>>[]
                {
                    appointment => appointment.DoctorId.Equals(request.DoctorId),
                    appointment => appointment.Date.Equals(request.Date),
                });

            return new PagedResponse<DoctorScheduledAppointmentResponse>(
                _mapper.Map<IEnumerable<DoctorScheduledAppointmentResponse>>(response.Items),
                request.CurrentPage,
                request.PageSize,
                response.TotalCount);
        }
    }
}
