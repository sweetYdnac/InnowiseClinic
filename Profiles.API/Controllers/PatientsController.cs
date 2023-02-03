using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Features.Patient.Commands;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PatientsController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Create new Patient profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequestModel request)
        {
            var id = await _mediator.Send(_mapper.Map<CreatePatientCommand>(request));
            return StatusCode(201, new { id });
        }
    }
}
