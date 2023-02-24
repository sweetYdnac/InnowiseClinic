using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Interfaces;
using Services.Data.DTOs;
using Shared.Core.Enums;
using Shared.Models.Request.Services.Specialization;
using Shared.Models.Request.Services.Specialization.SwaggerExamples;
using Shared.Models.Response;
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
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public SpecializationsController(ISpecializationService specializationService, IMapper mapper) =>
            (_specializationService, _mapper) = (specializationService, mapper);

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
    }
}
