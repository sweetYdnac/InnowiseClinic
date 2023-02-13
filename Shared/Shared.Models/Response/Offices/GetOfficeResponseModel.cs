namespace Shared.Models.Response.Offices
{
    public class OfficeDetailsResponse 
    {
        public Guid PhotoId { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string RegistryPhoneNumber { get; set; }
    }
}
