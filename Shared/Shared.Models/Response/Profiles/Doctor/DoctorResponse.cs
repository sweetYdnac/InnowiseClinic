﻿namespace Shared.Models.Response.Profiles.Doctor
{
    public class DoctorResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
        public DateTime CareerStartYear { get; set; }
    }
}
