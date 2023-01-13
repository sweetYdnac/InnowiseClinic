using Authorization.API.Models.Request;
using Authorization.Business.Abstractions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using Shared.Exceptions.Shared;

namespace Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) => _accountService = accountService;

        /// <summary>
        /// Sign up new Account
        /// </summary>
        /// <param name="request">Contains email and password</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SignInRequestModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel request)
        {
            if (request is null)
            {
                throw new EmptyRequestException();
            }

            await _accountService.SignUp(request.Email, request.Password);

            return CreatedAtAction(
                    nameof(SignIn),
                new SignInRequestModel {
                            Email = request.Email,
                            Password = request.Password
                        });
        }

        /// <summary>
        /// Sign in account/>
        /// </summary>
        /// <param name="request">Contains email and password</param>
        /// <returns></returns>
        [HttpGet("SignIn")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignIn([FromQuery] SignInRequestModel request)
        {
            if (request is null)
            {
                throw new EmptyRequestException();
            }

            var tokenResponce = await _accountService.SignIn(request.Email, request.Password);

            return Ok(tokenResponce);
        }

        /// <summary>
        /// Sign out from account
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignOut")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public new async Task<IActionResult> SignOut()
        {
            await _accountService.SignOut();

            return NoContent();
        }

        /// <summary>
        /// Add role to specific account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddToRole")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToRole([FromBody] AddInRoleRequestModel request)
        {
            if (request is null)
            {
                throw new EmptyRequestException();
            }

            await _accountService.AddToRole(request.Email, request.RoleName);

            return NoContent();
        }

        /// <summary>
        /// Remove specific role from account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RemoveFromRole")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFromRole([FromBody] RemoveFromRoleRequestModel request)
        {
            if (request is null)
            {
                throw new EmptyRequestException();
            }

            await _accountService.RemoveFromRole(request.Email, request.RoleName);

            return NoContent();
        }
    }
}
