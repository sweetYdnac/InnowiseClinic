using Shared.Core.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Profiles.Doctor.SwaggerExamples
{
    public class CreateDoctorRequestExample : IExamplesProvider<CreateDoctorRequest>
    {
        public CreateDoctorRequest GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                PhotoId = Guid.NewGuid(),
                FirstName = "Mark",
                LastName = "Cello",
                MiddleName = "Something",
                DateOfBirth = new DateOnly(2000, 04, 15),
                SpecializationId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                CareerStartYear = 2010,
                SpecializationName = "Dentist",
                OfficeAddress = "Minesota SomeStreet 22, 2",
                Status = AccountStatuses.AtWork,
            };
    }
}
