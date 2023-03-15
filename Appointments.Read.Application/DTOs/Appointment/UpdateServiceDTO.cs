namespace Appointments.Read.Application.DTOs.Appointment
{
    public class UpdateServiceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TimeSlotSize { get; set; }
    }
}
