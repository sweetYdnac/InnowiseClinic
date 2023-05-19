namespace Shared.Models.Request.Services.Service
{
    public class UpdateServiceRequest
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public int TimeSlotSize { get; set; }
        public bool IsActive { get; set; }
    }
}
