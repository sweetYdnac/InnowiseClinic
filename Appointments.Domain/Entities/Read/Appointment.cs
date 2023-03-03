﻿using Appointments.Domain.Common;

namespace Appointments.Domain.Entities.Read
{
    public class Appointment : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public bool IsApproved { get; set; }

        public AppointmentResult AppointmentResult { get; set; }
    }
}
