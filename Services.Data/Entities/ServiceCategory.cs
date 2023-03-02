namespace Services.Data.Entities
{
    public class ServiceCategory : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TimeSlotSize { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
