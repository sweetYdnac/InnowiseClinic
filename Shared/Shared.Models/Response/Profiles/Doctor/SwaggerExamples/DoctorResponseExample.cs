using Shared.Core.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Doctor.SwaggerExamples
{
    public class DoctorResponseExample : IExamplesProvider<DoctorResponse>
    {
        public DoctorResponse GetExamples() =>
            new()
            {
                FirstName = "Jenna",
                LastName = "Ortega",
                MiddleName = "Some middle name",
                DateOfBirth = new DateOnly(2002, 07, 16),
                SpecializationName = "Dantist",
                OfficeAddress = "Homel Barisenko 15 6",
                CareerStartYear = 2010,
                PhotoId = Guid.NewGuid(),
                Status = AccountStatuses.AtWork
            };
    }
}
