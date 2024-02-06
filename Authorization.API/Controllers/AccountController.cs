using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Models.Request.Authorization;
using Shared.Models.Response;
using Shared.Models.Response.Authorization;

namespace Authorization.API.Controllers
{
    /// <summary>
    /// This controller used to work with accounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper, ITokenService tokenService) =>
            (_accountService, _mapper, _tokenService) = (accountService, mapper, tokenService);

        /// <summary>
        /// Get Account by Id
        /// </summary>
        /// <param name="id">Account's unique identifier</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var response = await _accountService.GetById(id);

            return Ok(response);
        }

        /// <summary>
        /// Sign up new Account
        /// </summary>
        /// <param name="request">Contains email and password</param>
        [HttpPost("signUp")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var id = await _accountService.SignUpAsync(request.Email, request.Password, request.Role);

            return StatusCode(201, new { id });
        }

        /// <summary>
        /// Sign in account/>
        /// </summary>
        /// <param name="request">Contains email and password</param>
        [HttpPost("signIn")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var tokenResponse = await _accountService.SignInAsync(request.Email, request.Password);
            var response = _mapper.Map<TokenResponse>(tokenResponse);

            return Ok(response);
        }

        /// <summary>
        /// Reauthorize by refreshing access token
        /// </summary>
        /// <param name="request">Contain refreshToken</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokenResponse = await _tokenService.RefreshToken(request.RefreshToken);
            var response = _mapper.Map<TokenResponse>(tokenResponse);

            return Ok(response);
        }

        /// <summary>
        /// Sign out from an account
        /// </summary>
        [HttpPost("signOut")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public new async Task<IActionResult> SignOut()
        {
            await _accountService.SignOutAsync();

            return NoContent();
        }

        /// <summary>
        /// Patch roles from specific Account
        /// </summary>
        /// <param name="id">Account's unique identifier</param>
        /// <param name="request">Contains options for work with account's roles</param>
        [HttpPatch("{id}/roles")]
        [Authorize(Roles = nameof(AccountRoles.Admin))]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchRoles(Guid id, [FromBody] PatchRolesRequest request)
        {
            var dto = _mapper.Map<PatchRolesDTO>(request);
            await _accountService.UpdateRolesAsync(id, dto);

            return NoContent();
        }
    }
}
