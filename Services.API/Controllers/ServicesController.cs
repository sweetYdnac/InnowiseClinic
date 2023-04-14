using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Interfaces;
using Services.Data.DTOs.Service;
using Shared.Core.Enums;
using Shared.Models.Request.Services.Service;
using Shared.Models.Request.Services.Service.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.Services.Service;
using Shared.Models.Response.Services.Service.SwaggerExamples;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Services.API.Controllers
{
    /// <summary>
    /// This controller used to work with services
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;
        private readonly IMapper _mapper;

        public ServicesController(IServicesService servicesService, IMapper mapper) =>
            (_servicesService, _mapper) = (servicesService, mapper);

        /// <summary>
        /// Get service by id
        /// </summary>
        /// <param name="id">Service's unique identifier</param>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponseExample))]
        public async Task<IActionResult> GetServiceById([FromRoute] Guid id)
        {
            var response = await _servicesService.GetByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get paged services
        /// </summary>
        /// <param name="request">Contains properties for paging among services</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(PagedResponse<ServiceInformationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetServicesResponseExample))]
        public async Task<IActionResult> GetServices([FromQuery] GetServicesRequest request)
        {
            var response = await _servicesService.GetPagedAsync(_mapper.Map<GetServicesDTO>(request));

            return Ok(response);
        }

        /// <summary>
        /// Create new service
        /// </summary>
        /// <param name="request">Contains all data that need to create service entity</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateServiceRequest), typeof(CreateServiceRequestExample))]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceRequest request)
        {
            var id = await _servicesService.CreateAsync(_mapper.Map<CreateServiceDTO>(request));

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Change status of specific service
        /// </summary>
        /// <param name="id">Service's unique identifier</param>
        /// <param name="isActive">New services's status</param>
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromQuery] bool isActive)
        {
            await _servicesService.ChangeStatusAsync(id, isActive);

            return NoContent();
        }

        /// <summary>
        /// Edit specific service
        /// </summary>
        /// <param name="id">Service's unique identifier</param>
        /// <param name="request">Contains all data that need to update service entity</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(UpdateServiceRequest), typeof(UpdateServiceRequestExample))]
        public async Task<IActionResult> UpdateService([FromRoute] Guid id, UpdateServiceRequest request)
        {
            await _servicesService.UpdateAsync(id, _mapper.Map<UpdateServiceDTO>(request));

            return NoContent();
        }
    }
}
