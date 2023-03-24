using Documents.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
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
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedResponseExample))]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService) => _photoService = photoService;

        /// <summary>
        /// Get photo by it's name
        /// </summary>
        /// <param name="id">Photo's unique identifier</param>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("image/jpeg", "image/png")]
        [Authorize]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPhoto([FromRoute] Guid id)
        {
            var response = await _photoService.GetByIdAsync(id);

            return File(response.Content, response.ContentType, response.FileName);
        }

        /// <summary>
        /// Upload new photo to blob storage
        /// </summary>
        /// <param name="photo">Photo content</param>
        [HttpPost]
        [Authorize(Roles = $"{nameof(AccountRoles.Admin)}, {nameof(AccountRoles.Receptionist)}, {nameof(AccountRoles.Patient)}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePhoto(IFormFile photo)
        {
            var id = await _photoService.CreateAsync(photo);

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Edit existing photo
        /// </summary>
        /// <param name="id">Photo's unique identifier</param>
        /// <param name="photo">New photo content</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationFailedResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditPhoto([FromRoute] Guid id, [FromForm] IFormFile photo)
        {
            await _photoService.UpdateAsync(id, photo);

            return NoContent();
        }
    }
}
