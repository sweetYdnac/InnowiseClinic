﻿using Shared.Models.Request.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Receptionist
{
    public class UpdateReceptionistRequestExample : IExamplesProvider<UpdateReceptionistRequestModel>
    {
        public UpdateReceptionistRequestModel GetExamples() =>
            new UpdateReceptionistRequestModel
            {
                FirstName = "Jenna",
                LastName = "Ortega",
                MiddleName = "Some middle name",
                OfficeId = Guid.NewGuid(),
                OfficeAddress = "New York somestreet 10 6"
            };
    }
}
