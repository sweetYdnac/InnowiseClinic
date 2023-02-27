namespace Shared.Models.Response.Services.Service
{
    public class ServiceResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string CategoryTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
