using Documents.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Response;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Documents.API.Controllers
{
    /// <summary>
    /// This controller used to work with appointment results
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class AppointmentResultsController : ControllerBase
    {
        private readonly IAppointmentResultsService _appointmentResultsService;

        public AppointmentResultsController(IAppointmentResultsService appointmentResultsService) =>
            _appointmentResultsService = appointmentResultsService;

        /// <summary>
        /// Get photo by it's name
        /// </summary>
        /// <param name="id">Name of specific photo</param>
        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentResult([FromRoute] Guid id)
        {
            var response = await _appointmentResultsService.GetBlobAsync(id.ToString());

            return File(response.Content, response.ContentType, response.FileName);
        }
    }
}
