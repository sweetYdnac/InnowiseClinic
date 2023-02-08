﻿using AutoMapper;
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
using Shared.Models.SwaggerExamples.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.Controllers
{
    /// <summary>
    /// This controller used to work with office resourse
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public OfficesController(IOfficeService officeService, IMapper mapper) =>
            (_officeService, _mapper) = (officeService, mapper);

        /// <summary>
        /// Get specific office by id
        /// </summary>
        /// <param name="id">Office's unique identifier</param>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(OfficeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOfficeResponseExample))]
        public async Task<IActionResult> GetOffice([FromRoute] Guid id)
        {
            var office = await _officeService.GetByIdAsync(id);
            return Ok(office);
        }

        /// <summary>
        /// Create new office
        /// </summary>
        /// <param name="request">Contains all data that need to create new office</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(Status201Response), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateOfficeRequestModel), typeof(CreateOfficeRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(Status201Example))]
        public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeRequestModel request)
        {
            var response = await _officeService.CreateAsync(_mapper.Map<CreateOfficeDTO>(request));
            return StatusCode(201, response);
        }

        /// <summary>
        /// Change status of specific office
        /// </summary>
        /// <param name="id">Office's unique identifier</param>
        /// <param name="isActive">New Office's status</param>
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromQuery] bool isActive)
        {
            await _officeService.ChangeStatus(new ChangeOfficeStatusDTO { Id = id, IsActive = isActive });
            return NoContent();
        }

        /// <summary>
        /// Update existed office
        /// </summary>
        /// <param name="id">Office's unique identifier</param>
        /// <param name="request">Contains all office's entity properties</param>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Admin)}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(UpdateOfficeRequestModel), typeof(UpdateOfficeRequestExample))]
        public async Task<IActionResult> UpdateOffice([FromRoute] Guid id, [FromBody] UpdateOfficeRequestModel request)
        {
            await _officeService.UpdateAsync(id, _mapper.Map<UpdateOfficeDTO>(request));
            return NoContent();
        }
    }
}
