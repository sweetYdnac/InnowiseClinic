using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Features.Doctor.Commands;
using Profiles.Application.Features.Doctor.Queries;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Doctor;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Doctor;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Doctor;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.Controllers
{
    /// <summary>
    /// This controller used to work with doctors's profiles
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
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(DoctorInformationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetDoctorsResponseModel))]
        [Authorize(Roles = $"{nameof(AccountRoles.Patient)}, {nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(GetDoctorsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctors([FromQuery] GetDoctorsRequestModel request)
        {
            var response = await _doctorsService.GetPagedAndFilteredAsync(_mapper.Map<GetDoctorsDTO>(request));

            return Ok(response);
        }

        /// <summary>
        /// Create new doctor's profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequestModel request)
        {
            var id = await _mediator.Send(_mapper.Map<CreateDoctorCommand>(request));
            return StatusCode(201, new { id });
        }
    }
}
