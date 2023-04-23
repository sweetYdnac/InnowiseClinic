﻿using Appointments.Read.Application.Features.Queries.Appointments;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Request.Appointments.Appointment;
using Shared.Models.Request.Appointments.Appointment.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.Appointments.Appointment;
using Shared.Models.Response.Appointments.Appointment.SwaggerExamples;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Appointments.Read.API.Controllers
{
    /// <summary>
    /// This controller used for query appointments data
    /// </summary>
    [Route("api")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AppointmentsController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Get appointment by Id
        /// </summary>
        /// <param name="id">Appointment's unique identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}, {nameof(AccountRoles.Patient)}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}")]
        [ProducesResponseType(typeof(RescheduleAppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RescheduleAppointmentResponseExample))]
        public async Task<IActionResult> GetAppointment([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetAppointmentByIdQuery() { Id = id });

            return Ok(response);
        }

        /// <summary>
        /// Get appointment schedule by doctor
        /// </summary>
        /// <param name="id">Doctor's unique identifier</param>
        /// <param name="request">Paging parameters</param>
        [HttpGet("doctors/{id}/appointments")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}")]
        [ProducesResponseType(typeof(PagedResponse<DoctorScheduledAppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(GetDoctorScheduleRequest), typeof(GetDoctorScheduleRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetDoctorScheduleResponseExample))]
        public async Task<IActionResult> GetDoctorSchedule([FromRoute] Guid id, [FromQuery] GetDoctorScheduleRequest request)
        {
            var query = _mapper.Map<GetDoctorScheduleQuery>(request);
            query.DoctorId = id;

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Get ordered appointments by page and filters
        /// </summary>
        /// <param name="request">Contains paged and filtering parameters</param>
        [HttpGet("appointments")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(PagedResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(GetAppointmentsRequest), typeof(GetAppointmentsRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAppointmentsResponseExample))]
        public async Task<IActionResult> GetAppointments([FromQuery] GetAppointmentsRequest request)
        {
            var response = await _mediator.Send(_mapper.Map<GetAppointmentsQuery>(request));

            return Ok(response);
        }

        /// <summary>
        /// Get patient's history
        /// </summary>
        /// <param name="id">Patient's unique identifier</param>
        /// <param name="request">Contains paging parameters</param>
        [HttpGet("patients/{id}/appointments")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(PagedResponse<AppointmentHistoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(GetPatientHistoryRequest), typeof(GetPatientHistoryRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPatientHistoryResponseExample))]
        public async Task<IActionResult> GetPatientHistory([FromRoute] Guid id, [FromQuery] GetPatientHistoryRequest request)
        {
            var query = _mapper.Map<GetPatientHistoryQuery>(request);
            query.PatientId = id;

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Get available time slots for specific date
        /// </summary>
        /// <param name="request">Contains parameters for filtering appointments</param>
        [HttpGet("appointments/timeslots")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(TimeSlotsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(GetTimeSlotsRequest), typeof(GetTimeSlotsRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TimeSlotsResponseExample))]
        public async Task<IActionResult> GetTimeSlots([FromQuery] GetTimeSlotsRequest request)
        {
            var response = await _mediator.Send(_mapper.Map<GetTimeSlotsQuery>(request));

            return Ok(response);
        }
    }
}
