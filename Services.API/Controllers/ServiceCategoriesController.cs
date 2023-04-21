using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Business.Interfaces;
using Shared.Core.Enums;
using Shared.Models.Response;
using Shared.Models.Response.Services.ServiceCategories;
using Shared.Models.Response.Services.ServiceCategories.SwaggerExamples;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Services.API.Controllers
{
    /// <summary>
    /// This controller used to work with service categories
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]

    public class ServiceCategoriesController : ControllerBase
    {
        private readonly IServiceCategoriesService _serviceCategoriesService;

        public ServiceCategoriesController(IServiceCategoriesService serviceCategoriesService) =>
            _serviceCategoriesService = serviceCategoriesService;

        /// <summary>
        /// Get service category by id
        /// </summary>
        /// <param name="id">Service category's unique identifier</param>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}")]
        [ProducesResponseType(typeof(ServiceCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceCategoryResponseExample))]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var response = await _serviceCategoriesService.GetByIdAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Get all service categories
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(GetCategoriesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCategoriesResponseExample))]
        public async Task<IActionResult> GetCatigories()
        {
            var response = await _serviceCategoriesService.GetAllAsync();

            return Ok(response);
        }
    }
}
