namespace Shared.Models.Request.Appointments.AppointmentResult
{
    public class CreateAppointmentResultRequest
    {
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
