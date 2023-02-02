﻿namespace Offices.Domain.Entities
{
    public class OfficeEntity
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public Guid PhotoId { get; set; }
        public bool IsActive { get; set; }
    }
}
