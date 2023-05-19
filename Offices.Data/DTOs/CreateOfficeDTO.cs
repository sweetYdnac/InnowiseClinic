namespace Offices.Data.DTOs
{
    public class CreateOfficeDTO
    {
        public Guid Id { get; set; }
        public Guid? PhotoId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
