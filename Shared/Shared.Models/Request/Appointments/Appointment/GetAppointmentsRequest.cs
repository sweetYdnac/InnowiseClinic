namespace Shared.Models.Request.Appointments.Appointment
{
    public class GetAppointmentsRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public DateOnly Date { get; set; }
        public string DoctorFullName { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? OfficeId { get; set; }
        public bool? IsApproved { get; set; }
    }
}
