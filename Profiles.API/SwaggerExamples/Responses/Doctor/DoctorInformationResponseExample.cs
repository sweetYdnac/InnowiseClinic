using Shared.Core.Enums;
using Shared.Models.Response.Profiles.Doctor;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Doctor
{
    public class DoctorInformationResponseExample : IExamplesProvider<DoctorResponse>
    {
        public DoctorResponse GetExamples() =>
            new()
            {
                FirstName = "Jenna",
                LastName = "Ortega",
                MiddleName = "Some middle name",
                DateOfBirth = new DateTime(2002, 07,16),
                SpecializationName = "Dantist",
                OfficeAddress = "Homel Barisenko 15 6",
                CareerStartYear = new DateTime(2010, 08,28),
                PhotoId = Guid.NewGuid(),
                Status = AccountStatuses.AtWork
            };
    }
}
