using Microsoft.AspNetCore.Identity;
using Shared.Core.Enums;

namespace Authorization.Data.Entities
{
    public class Account : IdentityUser<Guid>
    {
        public AccountStatuses Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
