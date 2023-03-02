namespace Services.Data.Entities
{
    public sealed class Service : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public Guid CategoryId { get; set; }
        public ServiceCategory Category { get; set; }
    }
}
