using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Features.Doctor.Queries;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Doctor;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsInformationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DoctorsInformationController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Get doctors with paging info by filter
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(GetDoctorsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctors([FromQuery] GetDoctorsRequestModel request)
        {
            var response = await _mediator.Send(_mapper.Map<GetDoctorsInformationQuery>(request));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(DoctorInformationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetDoctorInformationQuery { Id = id });
            return Ok(response);
        }
    }
}
