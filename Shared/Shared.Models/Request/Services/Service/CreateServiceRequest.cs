namespace Shared.Models.Request.Services.Service
{
    public class CreateServiceRequest
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
