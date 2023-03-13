namespace Appointments.Read.Application.DTOs.Appointment
{
    public class UpdatePatientDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
