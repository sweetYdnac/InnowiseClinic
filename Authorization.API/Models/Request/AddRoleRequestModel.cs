using System.ComponentModel.DataAnnotations;

namespace Authorization.API.Models.Request
{
    public class AddInRoleRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
