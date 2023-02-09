using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Validators.Receptionist;
using Profiles.Business.Interfaces.Services;
using Shared.Core.Enums;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;

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
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
    }
}
