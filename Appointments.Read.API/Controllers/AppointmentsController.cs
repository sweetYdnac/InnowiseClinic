﻿using Appointments.Read.Application.Features.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Request.Appointments.Appointment;
using Shared.Models.Request.Appointments.Appointment.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.Appointments.Appointment;
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetDoctorScheduleRequestExample))]
        public async Task<IActionResult> GetDoctorSchedule([FromRoute] Guid id, [FromQuery] GetDoctorScheduleRequest request)
        {
            var query = _mapper.Map<GetDoctorScheduleQuery>(request);
            query.DoctorId = id;

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
