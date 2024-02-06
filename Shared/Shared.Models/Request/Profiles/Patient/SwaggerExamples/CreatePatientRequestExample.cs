﻿using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Profiles.Patient.SwaggerExamples
{
    public class CreatePatientRequestExample : IExamplesProvider<CreatePatientRequest>
    {
        public CreatePatientRequest GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Scarlet",
                LastName = "Johansson",
                MiddleName = "nvm",
                DateOfBirth = new DateOnly(1985, 10, 19),
                PhotoId = Guid.NewGuid(),
                PhoneNumber = "123321123",
            };
    }
}
