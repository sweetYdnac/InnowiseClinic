﻿namespace Shared.Models.Request.Profiles.Patient
{
    public class GetMatchedPatientRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
