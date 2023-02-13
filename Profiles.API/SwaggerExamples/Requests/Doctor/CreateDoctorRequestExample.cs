using Shared.Core.Enums;
using Shared.Models.Request.Profiles.Doctor;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Doctor
{
    public class CreateDoctorRequestExample : IExamplesProvider<CreateDoctorRequestModel>
    {
        public CreateDoctorRequestModel GetExamples() =>
            new()
            {
                PhotoId = Guid.NewGuid(),
                FirstName = "Mark",
                LastName = "Cello",
                MiddleName = "Something",
                DateOfBirth = new DateTime(2000, 04,15),
                SpecializationId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                CareerStartYear = new DateTime(2010, 10, 26),
                SpecializationName = "Dentist",
                OfficeAddress = "Minesota SomeStreet 22, 2",
                AccountId = Guid.NewGuid(),
                Status = AccountStatuses.AtWork,
            };
    }
}
