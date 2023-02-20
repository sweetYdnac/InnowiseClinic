﻿using Authorization.Business.Abstractions;
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
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper) =>
            (_accountService, _mapper) = (accountService, mapper);

        /// <summary>
        /// Sign up new Account
        /// </summary>
        /// <param name="request">Contains email and password</param>
        [ProducesResponseType(typeof(SignUpResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var id = await _accountService.SignUpAsync(request.Email, request.Password);

            return StatusCode(201, new SignUpResponse { Id = id });
        }

        /// <summary>
        /// Sign in account/>
        /// </summary>
        /// <param name="request">Contains email and password</param>
        [HttpPost("signIn")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var tokenResponce = await _accountService.SignInAsync(request.Email, request.Password);
            var responseModel = _mapper.Map<TokenResponse>(tokenResponce);

            return Ok(responseModel);
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
