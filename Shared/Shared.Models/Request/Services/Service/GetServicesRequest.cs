namespace Shared.Models.Request.Services.Service
{
    public class GetServicesRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public Guid? SpecializationId { get; set; }
        public bool IsActive { get; set; }
    }
}
