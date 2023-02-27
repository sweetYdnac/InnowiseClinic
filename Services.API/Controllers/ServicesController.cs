using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Interfaces;
using Services.Data.DTOs;
using Shared.Core.Enums;
using Shared.Models.Request.Services.Service;
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
        public async Task<IActionResult> GetService([FromRoute] Guid id)
        {
            var response = await _servicesService.GetByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get paged services
        /// </summary>
        /// <param name="request">Contains properties for paging among services</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(PagedResponse<ServiceResponse>), StatusCodes.Status200OK)]
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
    }
}
