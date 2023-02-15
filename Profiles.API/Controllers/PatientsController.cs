using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.SwaggerExamples.Requests.Patient;
using Profiles.API.SwaggerExamples.Responses.Patient;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Patient;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Patient;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.Controllers
{
    /// <summary>
    /// This controller used to work with Patient's profiles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        private readonly IMapper _mapper;

        public PatientsController(IPatientsService patientsService, IMapper mapper) =>
            (_patientsService, _mapper) = (patientsService, mapper);

        /// <summary>
        /// Get patient's profile by Id
        /// </summary>
        /// <param name="id">Patient's profile unique identifier</param>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PatientResponseExample))]
        public async Task<IActionResult> GetPatientById([FromRoute] Guid id)
        {
            var patient = await _patientsService.GetByIdAsync(id);

            return Ok(patient);
        }

        /// <summary>
        /// Get paged patients profiles by filter
        /// </summary>
        /// <param name="request">Contains properties for paging and filtering among patients</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(GetPatientsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPatientsResponseExample))]
        public async Task<IActionResult> GetPatients([FromQuery] GetPatientsRequestModel request)
        {
            var patients = await _patientsService.GetPagedAndFilteredAsync(_mapper.Map<GetPatientsDTO>(request));

            return Ok(patients);
        }

        /// <summary>
        /// Create new patient's profile
        /// </summary>
        /// <param name="request">Contains all data that need to create patient's profile entity</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreatePatientRequestModel), typeof(CreatePatientRequestExample))]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequestModel request)
        {
            var id = await _patientsService.CreateAsync(_mapper.Map<CreatePatientDTO>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Try find a match among the existed profiles.
        /// </summary>
        /// <param name="request">Contains properties for filtering among profiles and find a match</param>
        /// <returns>Given a match with profile has been found returns founded profile. Otherwise - returns null</returns>
        [HttpGet("match")]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PatientResponseExample))]
        public async Task<IActionResult> GetMatch([FromQuery] GetMatchedPatientRequestModel request)
        {
            var response = await _patientsService.GetMatchedPatientAsync(_mapper.Map<GetMatchedPatientDTO>(request));

            return Ok(response);
        }

        /// <summary>
        /// Remove specific patient's profile from storage
        /// </summary>
        /// <param name="id">Patient's profile unique identifier</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePatient([FromRoute] Guid id)
        {
            await _patientsService.DeleteAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Edit specific patient's profile
        /// </summary>
        /// <param name="id">Patient's profile unique identifier</param>
        /// <param name="request">Contains new values for all patient's profile entity properties</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(UpdatePatientRequestModel), typeof(UpdatePatientRequestExample))]
        public async Task<IActionResult> UpdatePatient([FromRoute] Guid id, [FromBody] UpdatePatientRequestModel request)
        {
            await _patientsService.UpdateAsync(id, _mapper.Map<UpdatePatientDTO>(request));

            return NoContent();
        }

        /// <summary>
        /// Link specific patient's profile to account when user want to accept matched profile with this input.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountId"></param>
        [HttpPatch("{id}")]
        [Authorize(Roles = nameof(AccountRoles.Patient))]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LinkToAccount([FromRoute] Guid id, [FromBody] Guid accountId)
        {
            await _patientsService.LinkToAccount(id, accountId);

            return NoContent();
        }
    }
}
