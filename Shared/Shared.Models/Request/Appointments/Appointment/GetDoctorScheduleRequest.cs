namespace Shared.Models.Request.Appointments.Appointment
{
    public class GetDoctorScheduleRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public DateOnly Date { get; set; }
    }
}
