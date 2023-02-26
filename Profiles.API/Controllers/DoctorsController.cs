using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Doctor;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles;
using Shared.Models.Request.Profiles.Doctor;
using Shared.Models.Request.Profiles.Doctor.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Doctor;
using Shared.Models.Response.Profiles.Doctor.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

namespace Profiles.API.Controllers
{
    /// <summary>
    /// This controller used to work with doctors' profiles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorsService;
        private readonly IMapper _mapper;
        public DoctorsController(IDoctorsService doctorsService, IMapper mapper) =>
            (_doctorsService, _mapper) = (doctorsService, mapper);

        /// <summary>
        /// Get doctor's profile by Id
        /// </summary>
        /// <param name="id">Doctor's profile unique identifier</param>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DoctorResponseExample))]
        public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
        {
            var response = await _doctorsService.GetByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get paged doctors profiles by filter
        /// </summary>
        /// <param name="request">Contains properties for paging and filtering among doctors</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(PagedResponse<DoctorInformationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetDoctorsResponseExample))]
        public async Task<IActionResult> GetDoctors([FromQuery] GetDoctorsRequest request)
        {
            var response = await _doctorsService.GetPagedAndFilteredAsync(_mapper.Map<GetDoctorsDTO>(request));

            return Ok(response);
        }

        /// <summary>
        /// Create new doctor's profile
        /// </summary>
        /// <param name="request">Contains all data that need to create doctor's profile entity</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateDoctorRequest), typeof(CreateDoctorRequestExample))]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            var id = await _doctorsService.CreateAsync(_mapper.Map<CreateDoctorDTO>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Edit specific doctor's profile
        /// </summary>
        /// <param name="id">Doctor's profile unique identifier</param>
        /// <param name="request">Contains new values for all doctor's profile entity properties</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Doctor)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(UpdateDoctorRequest), typeof(UpdateDoctorRequestExample))]
        public async Task<IActionResult> UpdateDoctor([FromRoute] Guid id, [FromBody] UpdateDoctorRequest request)
        {
            var dto = _mapper.Map<UpdateDoctorDTO>(request);
            dto.UpdaterId = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                ?.Value;

            await _doctorsService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Remove specific doctor's profile from storage
        /// </summary>
        /// <param name="id">Doctor's profile unique identifier</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDoctor([FromRoute] Guid id)
        {
            await _doctorsService.RemoveAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Change doctor's status
        /// </summary>
        /// <param name="id">Doctor's profile unique identifier</param>
        /// <param name="request">Contains new doctor's status</param>
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] ChangeStatusRequestModel request)
        {
            var dto = _mapper.Map<ChangeStatusDTO>(request);
            dto.UpdaterId = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                ?.Value;

            await _doctorsService.ChangeStatusAsync(id, dto);

            return NoContent();
        }
    }
}
