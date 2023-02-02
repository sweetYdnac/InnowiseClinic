using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offices.Application.Features.Office.Queries;
using Shared.Core.Enums;
using Shared.Models.Request.Offices;
using Shared.Models.Response;
using Shared.Models.Response.Offices;

namespace Offices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OfficesController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Get paginated offices
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(GetOfficesResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficesRequestModel request)
        {
            var offices = await _mediator.Send(_mapper.Map<GetOfficesQuery>(request));
            return Ok(offices);
        }

        /// <summary>
        /// Get specific office by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(OfficeDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOffice([FromRoute] Guid id)
        {
            var office = await _mediator.Send(new GetOfficeByIdQuery { Id = id });
            return Ok(office);
        }

        /// <summary>
        /// Create office
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeRequestModel request)
        {
            var id = await _mediator.Send(_mapper.Map<CreateOfficeCommand>(request));
            return StatusCode(201, new { id });
        }


        [HttpPatch("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromQuery] bool isActive)
        {
            await _mediator.Send(
                new ChangeOfficeStatusCommand 
                { 
                    Id = id, 
                    IsActive = isActive 
                });

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOffice([FromRoute] Guid id, [FromBody] UpdateOfficeRequestModel request)
        {
            var command = _mapper.Map<UpdateOfficeCommand>(request);
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
