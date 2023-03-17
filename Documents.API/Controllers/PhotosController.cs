﻿using Documents.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Response;
using Shared.Models.Response.SwaggerExampes;
using Swashbuckle.AspNetCore.Filters;

namespace Documents.API.Controllers
{
    /// <summary>
    /// This controller used to work with photos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService) => _photoService = photoService;

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
        public async Task<IActionResult> GetPhoto([FromRoute] Guid id)
        {
            var response = await _photoService.GetBlobAsync(id.ToString());

            return File(response.Content, response.ContentType, response.FileName);
        }

        /// <summary>
        /// Upload new photo to blob storage
        /// </summary>
        /// <param name="bytes">Array of bytes converted to string</param>
        [HttpPost]
        //[Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePhoto([FromBody] string bytes)
        {
            var id = await _photoService.UploadBlobAsync(Guid.NewGuid(), bytes);

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Edit existing photo
        /// </summary>
        /// <param name="id">Name of specific photo</param>
        /// <param name="bytes">Array of bytes converted to string</param>
        [HttpPut]
        //[Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditPhoto([FromRoute] Guid id, [FromBody] string bytes)
        {
            await _photoService.UpdateBlobAsync(id, bytes);

            return NoContent();
        }
    }
}
