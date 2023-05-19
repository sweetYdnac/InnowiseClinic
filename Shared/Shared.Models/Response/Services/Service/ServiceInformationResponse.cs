namespace Shared.Models.Response.Services.Service
{
    public class ServiceInformationResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int Duration { get; set; }
        public Guid SpecializationId { get; set; }
        public bool IsActive { get; set; }
    }
}
