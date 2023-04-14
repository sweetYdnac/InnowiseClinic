namespace Services.Data.DTOs.Service
{
    public class GetServicesDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public Guid? SpecializationId { get; set; }
        public bool IsActive { get; set; }
    }
}
