namespace Shared.Models.Response.Appointments.AppointmentResult
{
    public class PdfResultResponse
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
