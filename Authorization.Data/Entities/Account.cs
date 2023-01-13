using Microsoft.AspNetCore.Identity;

namespace Authorization.Data.Entities
{
    public class Account : IdentityUser<Guid>
    {
        public Guid PhotoId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
