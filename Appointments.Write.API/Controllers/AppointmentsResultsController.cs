using Appointments.Write.Application.Features.Commands.AppointmentsResults;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Request.Appointments.AppointmentResult;
using Shared.Models.Request.Appointments.AppointmentResult.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Appointments.Write.API.Controllers
{
    /// <summary>
    /// This controller used to work with appointments results
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class AppointmentsResultsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AppointmentsResultsController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Create result for specific appointment
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateAppointmentResultRequest), typeof(CreateAppointmentResultRequestExample))]
        public async Task<IActionResult> CreateAppointmentResult([FromBody] CreateAppointmentResultRequest request)
        {
            var id = await _mediator.Send(_mapper.Map<CreateAppointmentResultCommand>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Edit specific appointment's result
        /// </summary>
        /// <param name="id">Appointment result's unique identifier</param>
        /// <param name="request">Contains new appointment result's properties values</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(EditAppointmentResultRequest), typeof(EditAppointmentResultRequestExample))]
        public async Task<IActionResult> EditAppointmentResult([FromRoute] Guid id, [FromBody] EditAppointmentResultRequest request)
        {
            var command = _mapper.Map<EditAppointmentResultCommand>(request);
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
