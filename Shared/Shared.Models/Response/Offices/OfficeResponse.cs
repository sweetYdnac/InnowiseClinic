namespace Shared.Models.Response.Offices
{
    public class OfficeResponse
    {
        public Guid PhotoId { get; set; }
        public string Address { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
