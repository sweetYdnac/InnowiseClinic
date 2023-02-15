using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.SwaggerExamples.Requests.Receptionist;
using Profiles.API.SwaggerExamples.Responses.Receptionist;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Receptionist;
using Shared.Core.Enums;
using Shared.Models.Request.Profiles;
using Shared.Models.Request.Profiles.Receptionist;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

namespace Profiles.API.Controllers
{
    /// <summary>
    /// This controller used to work with receptionists' profiles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ReceptionistsController : ControllerBase
    {
        private readonly IReceptionistsService _receptionistsService;
        private readonly IMapper _mapper;
        public ReceptionistsController(IReceptionistsService receptionistsService, IMapper mapper) =>
            (_receptionistsService, _mapper) = (receptionistsService, mapper);

        /// <summary>
        /// Get receptionist's profile by Id
        /// </summary>
        /// <param name="id">Receptionist's profile unique identifier</param>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(ReceptionistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ReceptionistResponseExample))]
        public async Task<IActionResult> GetReceptionistById([FromRoute] Guid id)
        {
            var response = await _receptionistsService.GetByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get paged receptionists profiles by filter
        /// </summary>
        /// <param name="request">Contains properties for paging among receptionists</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]   
        [ProducesResponseType(typeof(GetReceptionistsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetReceptionistsResponseExample))]
        public async Task<IActionResult> GetReceptionists([FromQuery] GetReceptionistsRequestModel request)
        {
            var response = await _receptionistsService.GetPagedAsync(_mapper.Map<GetReceptionistsDTO>(request));

            return Ok(response);
        }

        /// <summary>
        /// Create new receptionist's profile
        /// </summary>
        /// <param name="request">Contains all data that need to create receptionist's profile entity</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateReceptionistRequestModel), typeof(CreateReceptionistRequestExample))]
        public async Task<IActionResult> CreateReceptionist([FromBody] CreateReceptionistRequestModel request)
        {
            var id = await _receptionistsService.CreateAsync(_mapper.Map<CreateReceptionistDTO>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Edit specific receptionist's profile
        /// </summary>
        /// <param name="id">Receptionist's profile unique identifier</param>
        /// <param name="request">Contains new values for all receptionist's profile entity properties</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(UpdateReceptionistRequestModel), typeof(UpdateReceptionistRequestExample))]
        public async Task<IActionResult> UpdateReceptionist([FromRoute] Guid id, [FromBody] UpdateReceptionistRequestModel request)
        {
            var dto = _mapper.Map<UpdateReceptionistDTO>(request);
            dto.UpdaterId = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                ?.Value;

            await _receptionistsService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Remove specific receptionist's profile from storage
        /// </summary>
        /// <param name="id">Receptionist's profile unique identifier</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReceptionist([FromRoute] Guid id)
        {
            await _receptionistsService.RemoveAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Change receptionist's status
        /// </summary>
        /// <param name="id">Receptionist's profile unique identifier</param>
        /// <param name="request">Contains new receptionist's status</param>
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] ChangeStatusRequestModel request)
        {
            var dto = _mapper.Map<ChangeStatusDTO>(request);
            dto.UpdaterId = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                ?.Value;

            await _receptionistsService.ChangeStatus(id, dto);

            return NoContent();
        }
    }
}
