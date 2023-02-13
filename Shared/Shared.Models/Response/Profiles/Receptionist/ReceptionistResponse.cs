﻿namespace Shared.Models.Response.Profiles.Receptionist
{
    public class ReceptionistResponse
    {
        public Guid? PhotoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string OfficeAddress { get; set; }
    }
}
