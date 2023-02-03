namespace Profiles.Domain.Entities
{
    public class ReceptionistEntity : User
    {
        public Guid OfficeId { get; set; }
    }
}
