using Appointments.Write.Application.Features.Commands.Appointments;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Request.Appointments.Appointment;
using Shared.Models.Request.Appointments.Appointment.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Appointments.Write.API.Controllers
{
    /// <summary>
    /// This controller used to work with appointments
    /// </summary>
    [Route("api/[controller]")]
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
        /// Create new appointment
        /// </summary>
        /// <param name="request">Contains all data that need to create service entity</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateAppointmentRequest), typeof(CreateAppointmentRequestExample))]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            var id = await _mediator.Send(_mapper.Map<CreateAppointmentCommand>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Reschedule specific appointment
        /// </summary>
        /// <param name="id">Appointment's unique identifier</param>
        /// <param name="request">Contains data that need for edit existed appointment</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(RescheduleAppointmentRequest), typeof(RescheduleAppointmentRequestExample))]
        public async Task<IActionResult> RescheduleAppointment([FromRoute] Guid id, [FromBody] RescheduleAppointmentRequest request)
        {
            var message = _mapper.Map<RescheduleAppointmentCommand>(request);
            message.Id = id;

            await _mediator.Send(message);

            return NoContent();
        }

        /// <summary>
        /// Cancel appointment
        /// </summary>
        /// <param name="id">Appointment's unique identifier</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelAppointment([FromRoute] Guid id)
        {
            await _mediator.Send(new CancelAppointmentCommand() { Id = id });

            return NoContent();
        }
    }
}
