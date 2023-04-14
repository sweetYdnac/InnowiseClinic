namespace Services.Data.DTOs.Specialization
{
    public class GetSpecializationsDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
