using Appointments.Read.Application.Features.Queries.AppointmentsResults;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Response;
using Shared.Models.Response.Appointments.AppointmentResult;
using Shared.Models.Response.Appointments.AppointmentResult.SwaggerExamples;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Appointments.Read.API.Controllers
{
    /// <summary>
    /// This controller used for query appointments results data
    /// </summary>
    [Route("api")]
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
        /// Get appointment result by Id
        /// </summary>
        /// <param name="id">Appointment result's unique identifier</param>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Doctor)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(AppointmentResultResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AppointmentResultResponseExample))]
        public async Task<IActionResult> GetAppointmentResult([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetAppointmentResultQuery { Id = id });

            return Ok(response);
        }
    }
}
