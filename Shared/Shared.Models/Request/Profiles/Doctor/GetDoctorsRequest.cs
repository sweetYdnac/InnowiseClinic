namespace Shared.Models.Request.Profiles.Doctor
{
    public class GetDoctorsRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string FullName { get; set; }
        public bool OnlyAtWork { get; set; }
    }
}
