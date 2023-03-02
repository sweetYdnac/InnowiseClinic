﻿namespace Services.Data.DTOs.Service
{
    public class CreateServiceDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
