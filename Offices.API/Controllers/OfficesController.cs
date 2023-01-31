using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Request.Offices;

namespace Offices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Authorize(Roles = nameof(AccountRoles.Receptionist))]
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficesRequestModel request)
        {
            return Ok();
        }
    }
}
