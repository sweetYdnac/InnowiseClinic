using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Interfaces;
using Services.Data.DTOs.Specialization;
using Shared.Core.Enums;
using Shared.Models.Request.Services.Specialization;
using Shared.Models.Request.Services.Specialization.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.Services.Specialization;
using Shared.Models.Response.Services.Specialization.SwaggerExamples;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Services.API.Controllers
{
    /// <summary>
    /// This controller used to work with specializations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public SpecializationsController(ISpecializationService specializationService, IMapper mapper) =>
            (_specializationService, _mapper) = (specializationService, mapper);

        /// <summary>
        /// Get specialization by Id
        /// </summary>
        /// <param name="id">Specialization's unique identifier</param>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(SpecializationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SpecializationResponseExample))]
        public async Task<IActionResult> GetSpecialization([FromRoute] Guid id)
        {
            var response = await _specializationService.GetByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Create new specialization
        /// </summary>
        /// <param name="request">Contains all data that need to create specialization entity</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateSpecializationRequest), typeof(CreateSpecializationRequestExample))]
        public async Task<IActionResult> CreateSpecialization([FromBody] CreateSpecializationRequest request)
        {
            var id = await _specializationService.CreateAsync(_mapper.Map<CreateSpecializationDTO>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Get paged specializations
        /// </summary>
        /// <param name="request">Contains properties for paging among specializations</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(PagedResponse<SpecializationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetSpecializationsResponseExample))]
        public async Task<IActionResult> GetSpecializations([FromQuery] GetSpecializationsRequest request)
        {
            var response = await _specializationService.GetPagedAsync(_mapper.Map<GetSpecializationsDTO>(request));

            return Ok(response);
        }

        /// <summary>
        /// Change status of specific specialization
        /// </summary>
        /// <param name="id">Specialization's unique identifier</param>
        /// <param name="isActive">New specialization's status</param>
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromQuery] bool isActive)
        {
            await _specializationService.ChangeStatusAsync(id, isActive);

            return NoContent();
        }

        /// <summary>
        /// Edit specific specialization
        /// </summary>
        /// <param name="id">Specialization's unique identifier</param>
        /// <param name="request">Contains all data that need to update specialization entity</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(UpdateSpecializationRequest), typeof(UpdateSpecializationRequestExample))]
        public async Task<IActionResult> UpdateSpecialization([FromRoute] Guid id, [FromBody] UpdateSpecializationRequest request)
        {
            await _specializationService.UpdateAsync(id, _mapper.Map<UpdateSpecializationDTO>(request));

            return NoContent();
        }
    }
}
