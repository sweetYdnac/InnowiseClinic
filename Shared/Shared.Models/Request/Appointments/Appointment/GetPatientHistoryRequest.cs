namespace Shared.Models.Request.Appointments.Appointment
{
    public class GetPatientHistoryRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool IsFinished { get; set; }
    }
}
