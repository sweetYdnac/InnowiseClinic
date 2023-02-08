using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offices.API.SwaggerExamples.Requests;
using Offices.API.SwaggerExamples.Responses;
using Offices.Business.Interfaces.Services;
using Offices.Data.DTOs;
using Shared.Core.Enums;
using Shared.Models.Request.Offices;
using Shared.Models.Response;
using Shared.Models.Response.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.Controllers
{
    /// <summary>
    /// This controller used to work with office's preview information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class OfficesInformationController : Controller
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public OfficesInformationController(IOfficeService officeService, IMapper mapper) =>
            (_officeService, _mapper) = (officeService, mapper);

        /// <summary>
        /// Get paginated offices
        /// </summary>
        /// <param name="request">Contains paging parameters</param>
        [HttpGet]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(GetOfficesResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(GetOfficesRequestModel), typeof(GetOfficesRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOfficesResponseExample))]
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficesRequestModel request)
        {
            var offices = await _officeService.GetOfficesAsync(_mapper.Map<GetPagedOfficesDTO>(request));
            return Ok(offices);
        }
    }
}
