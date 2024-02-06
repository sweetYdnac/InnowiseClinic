namespace Services.Data.DTOs.Service
{
    public class UpdateServiceDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public int TimeSlotSize { get; set; }
        public Guid SpecializationId { get; set; }
        public bool IsActive { get; set; }
    }
}
