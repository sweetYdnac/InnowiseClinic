namespace Offices.Data.DTOs
{
    public class GetPagedOfficesDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool? IsActive { get; set; }
    }
}
