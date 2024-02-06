namespace Shared.Models.Request.Services.Specialization
{
    public class GetSpecializationsRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
    }
}
