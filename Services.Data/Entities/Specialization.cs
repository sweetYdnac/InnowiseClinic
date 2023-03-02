namespace Services.Data.Entities
{
    public sealed class Specialization : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
