namespace Services.Data.Entities
{
    public sealed class Specialization : BaseEntity
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
