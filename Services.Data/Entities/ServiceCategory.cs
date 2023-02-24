namespace Services.Data.Entities
{
    public class ServiceCategory : BaseEntity
    {
        public string Title { get; set; }
        public int TimeSlotSize { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
