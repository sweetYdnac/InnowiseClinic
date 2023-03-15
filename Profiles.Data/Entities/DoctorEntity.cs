namespace Profiles.Data.Entities
{
    public class DoctorEntity : User
    {
        public DateOnly DateOfBirth { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
    }
}
