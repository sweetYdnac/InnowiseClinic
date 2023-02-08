using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Features.Patient.Queries;
using Profiles.Business.Interfaces.Services;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Patient;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        private readonly IMapper _mapper;

        public PatientsController(IPatientsService patientsService, IMapper mapper) =>
            (_patientsService, _mapper) = (patientsService, mapper);

        /// <summary>
        /// Try find a match among the existed profiles.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Given a match with profile has been found returns this profile. Else - returns null</returns>
        [HttpGet("match")]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMatch([FromQuery] GetMatchedPatientRequestModel request)
        {
            var response = await _mediator.Send(_mapper.Map<GetMatchedPatientQuery>(request));
            return Ok(response);
        }

        /// <summary>
        /// Create new patient's profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequestModel request)
        {
            var id = await _mediator.Send(_mapper.Map<CreatePatientCommand>(request));
            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Get patient's profile by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientById([FromRoute] Guid id)
        {
            var patient = await _patientsService.GetByIdAsync(id);
            return Ok(patient);
        }

        /// <summary>
        /// Get patients profiles by filter and by specific page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(GetPatientsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatients([FromQuery] GetPatientsRequestModel request)
        {
            var patients = await _mediator.Send(_mapper.Map<GetPatientsQuery>(request));
            return Ok(patients);
        }

        /// <summary>
        /// Delete specific patient's profile from storage
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePatient([FromRoute] Guid Id)
        {
            await _mediator.Send(new DeletePatientCommand { Id = Id });
            return NoContent();
        }

        /// <summary>
        /// Edit specific patient's profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditPatient([FromRoute] Guid id, [FromBody] EditPatientRequestModel request)
        {
            var command = _mapper.Map<UpdatePatientCommand>(request);
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Link specific patient's profile to account when user want to accept matched profile with this input.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = nameof(AccountRoles.Patient))]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LinkToAccount([FromRoute] Guid id, [FromBody] LinkToAccountRequestModel request)
        {
            var command = _mapper.Map<LinkToAccountCommand>(request);
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
