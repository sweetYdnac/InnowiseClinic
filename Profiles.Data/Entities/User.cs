namespace Profiles.Data.Entities
{
    public abstract class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid? AccountId { get; set; }
    }
}
