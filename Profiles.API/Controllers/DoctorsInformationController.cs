using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Features.Doctor.Queries;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Doctor;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Doctor;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.Controllers
{
    /// <summary>
    /// DoctorsInformationController is used to view demonstrative information about doctors.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class DoctorsInformationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DoctorsInformationController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Get doctors with paging info by filter
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="request">Contains paging parameters and properties for filtering doctors</param>
        /// <returns></returns>
        /// <example>
        /// </example>
        [HttpGet]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetDoctorsResponseModel))]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(GetDoctorsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctors([FromQuery] GetDoctorsRequestModel request)
        {
            var response = await _mediator.Send(_mapper.Map<GetDoctorsInformationQuery>(request));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DoctorInformationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetDoctorInformationQuery { Id = id });
            return Ok(response);
        }
    }
}
