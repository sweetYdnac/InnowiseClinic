﻿using Shared.Models.Request.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Patient
{
    public class CreatePatientRequestExample : IExamplesProvider<CreatePatientRequestModel>
    {
        public CreatePatientRequestModel GetExamples() =>
            new CreatePatientRequestModel
            {
                FirstName = "Scarlet",
                LastName = "Johansson",
                MiddleName = "nvm",
                DateOfBirth = new DateTime(1985, 10, 19),
                AccountId = Guid.NewGuid(),
                IsLinkedToAccount = true
            };
    }
}
