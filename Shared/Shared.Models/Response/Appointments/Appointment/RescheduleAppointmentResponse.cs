using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Response.Appointments.Appointment
{
    public class RescheduleAppointmentResponse
    {
        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }

        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; }
        public string DoctorSpecializationName { get; set; }

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }

        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; }
    }
}
