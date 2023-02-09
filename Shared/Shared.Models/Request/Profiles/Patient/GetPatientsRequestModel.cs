namespace Shared.Models.Request.Profiles.Patient
{
    public class GetPatientsRequestModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FullName { get; set; }
    }
}
