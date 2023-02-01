using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Offices.Application.Features.Office.Queries;
using Shared.Models.Request.Offices;
using Shared.Models.Response;
using Shared.Models.Response.Offices;

namespace Offices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OfficesController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        /// <summary>
        /// Get paginated offices
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Roles = nameof(AccountRoles.Receptionist))]
        [ProducesResponseType(typeof(GetOfficesResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficesRequestModel request)
        {
            var offices = await _mediator.Send(_mapper.Map<GetOfficesQuery>(request));
            return Ok(offices);
        }
    }
}
