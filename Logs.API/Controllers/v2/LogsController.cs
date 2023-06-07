using AutoMapper;
using Logs.Business.Interfaces.Services.v2;
using Logs.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shared.Models.Request.Logs;
using Shared.Models.Request.Logs.SwaggerExamples;
using Shared.Models.Response;
using Shared.Models.Response.Logs;
using Shared.Models.Response.Logs.SwaggerExamples;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Logs.API.Controllers
{
    /// <summary>
    /// This controller used to work with logs
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class LogsController: ControllerBase
    {
        private readonly IElasticLogService _logService;
        private readonly IMapper _mapper;

        public LogsController(IElasticLogService logService, IMapper mapper) => (_logService, _mapper) = (logService, mapper);

        /// <summary>
        /// Get specific log by id
        /// </summary>
        /// <param name="id">Log's unique identifier</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LogResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetLogResponseExample))]
        public async Task<IActionResult> GetLog([FromRoute] ObjectId id)
        {
            var respose = await _logService.GetByIdAsync(id);

            return Ok(respose);
        }

        /// <summary>
        /// Get paged logs
        /// </summary>
        /// <param name="request">Contains properties for filtering through logs</param>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<LogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(GetLogsRequest), typeof(GetLogsRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetLogsResponseExample))]
        public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequest request)
        {
            var response = await _logService.GetPagedAsync(_mapper.Map<GetLogsDTO>(request));

            return Ok(response);
        }
    }
}
