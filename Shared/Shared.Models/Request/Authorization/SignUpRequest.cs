using Shared.Core.Enums;

namespace Shared.Models.Request.Authorization
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
