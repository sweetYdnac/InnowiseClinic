using Authorization.API.Models.Request;
using Authorization.API.Models.Responce;
using Authorization.Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Sign up new Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SignInRequestModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel request)
        {
            if (request is null)
            {
                var message = "Request is empty";
                Log.Error(message);
                return BadRequest(new ErrorModel() { Message = message });
            }

            var result = await _accountService.SignUp(request.Email, request.Password);

            if (!result.Succeeded)
            {
                var message = $"User is not created. { result.Errors.FirstOrDefault().Description }";
                Log.Error(message);
                return StatusCode(500, new ErrorModel() { Message = message });
            }

            return CreatedAtAction(nameof(SignIn), 
            new SignInRequestModel {
                        Email = request.Email, 
                        Password = request.Password 
                    });
        }

        /// <summary>
        /// Sign in account/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [HttpGet("SignIn")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignIn([FromQuery] SignInRequestModel request)
        {
            if (request is null)
            {
                var message = "Request is empty";
                Log.Error(message);
                return BadRequest(new ErrorModel() { Message = message });
            }

            var result = await _accountService.SignIn(request.Email, request.Password);

            return Ok();
        }
    }
}
