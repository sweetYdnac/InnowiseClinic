namespace Shared.Models.Response.Services.Service
{
    public class ServiceResponse
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
