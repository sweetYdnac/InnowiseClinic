namespace Authorization.Core.Entities
{
    public class AccountEntity : IBaseEntity, ITrackable
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmailVerified { get; set; }
        public Guid PhotoId { get; set; }

        //public Guid CreatedBy { get; set; }
        //public Guid UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
