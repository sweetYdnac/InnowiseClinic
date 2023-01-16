using Authorization.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Authorization.API.Models.Request
{
    public class PatchAccountRequestModel
    {
        [Required]
        public Guid Id { get; set; }

        public AccountStatuses Status { get; set; }
    }
}
