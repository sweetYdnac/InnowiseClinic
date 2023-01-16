using System.ComponentModel.DataAnnotations;

namespace Authorization.API.Models.Request
{
    public class SignUpRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 15, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
