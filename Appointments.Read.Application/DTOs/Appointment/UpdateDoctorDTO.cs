namespace Appointments.Read.Application.DTOs.Appointment
{
    public class UpdateDoctorDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
        public Guid OfficeId { get; set; }
    }
}
