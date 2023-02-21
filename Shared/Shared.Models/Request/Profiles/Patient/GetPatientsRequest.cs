namespace Shared.Models.Request.Profiles.Patient
{
    public class GetPatientsRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string FullName { get; set; }
    }
}
