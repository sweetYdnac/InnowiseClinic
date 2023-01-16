using Authorization.API.Models.Request;
using Authorization.API.Models.Responce;
using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper) =>
            (_accountService, _mapper) = (accountService, mapper);

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

            await _accountService.SignUpAsync(request.Email, request.Password);

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
        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(TokenResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel request)
        {
            if (request is null)
            {
                throw new EmptyRequestException();
            }

            var tokenResponce = await _accountService.SignInAsync(request.Email, request.Password);
            var responseModel = _mapper.Map<TokenResponseModel>(tokenResponce);

            return Ok(responseModel);
        }

        /// <summary>
        /// Sign out from an account
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignOut")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public new async Task<IActionResult> SignOut()
        {
            await _accountService.SignOutAsync();

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

            await _accountService.AddToRoleAsync(request.Email, request.RoleName);

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

            await _accountService.RemoveFromRoleAsync(request.Email, request.RoleName);

            return NoContent();
        }

        /// <summary>
        /// Patch specific account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchAccount([FromBody] PatchAccountRequestModel request)
        {
            if (request is null)
            {
                throw new EmptyRequestException();
            }

            var dto = _mapper.Map<PatchAccountDTO>(request);
            dto.UpdaterClaimsPrincipal = HttpContext.User;

            await _accountService.UpdateAsync(request.Id, dto);

            return NoContent();
        }
    }
}
