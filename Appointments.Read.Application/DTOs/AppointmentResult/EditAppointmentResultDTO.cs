namespace Appointments.Read.Application.DTOs.AppointmentResult
{
    public class EditAppointmentResultDTO
    {
        public Guid Id { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
    }
}
