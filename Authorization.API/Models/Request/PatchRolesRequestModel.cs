using System.ComponentModel.DataAnnotations;

namespace Authorization.API.Models.Request
{
    public class PatchRolesRequestModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public bool IsAddRole { get; set; }
    }
}
