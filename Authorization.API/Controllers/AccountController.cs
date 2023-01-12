using Authorization.API.Models.Request;
using Authorization.API.Models.Responce;
using Authorization.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AccountController(
            SignInManager<Account> signInManager,  
            UserManager<Account> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Create new Identity User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequestModel request)
        {
            if (request is null)
            {
                var message = "Request is empty";
                Log.Error(message);
                return BadRequest(new ErrorModel() { Message = message });
            }

            var user = new Account { Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var message = "User is not created.";
                Log.Error(message);
                return StatusCode(500, new ErrorModel() { Message = message });
            }

            return NoContent();
        }
    }
}
