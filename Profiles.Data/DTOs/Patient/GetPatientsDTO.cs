namespace Profiles.Data.DTOs.Patient
{
    public class GetPatientsDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FullName { get; set; }
    }
}
