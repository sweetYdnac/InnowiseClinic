using Shared.Core.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Doctor.SwaggerExamples
{
    public class GetDoctorsResponseExample : IExamplesProvider<PagedResponse<DoctorInformationResponse>>
    {
        public PagedResponse<DoctorInformationResponse> GetExamples() =>
            new(
                new DoctorInformationResponse[]
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Holly Molly Polly",
                        SpecializationId = Guid.NewGuid(),
                        SpecializationName = "Dantist",
                        OfficeId = Guid.NewGuid(),
                        OfficeAddress = "Homel Barisenko 15 6",
                        Experience = 15,
                        DateOfBirth = new DateOnly(1980, 10, 15),
                        Status = AccountStatuses.AtWork,
                        PhotoId = Guid.NewGuid(),
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Jeff Besos Arg",
                        SpecializationId = Guid.NewGuid(),
                        SpecializationName = "Therapist",
                        OfficeAddress = "New York Test 10 31",
                        OfficeId = Guid.NewGuid(),
                        Experience = 4,
                        DateOfBirth = new DateOnly(1980, 10, 15),
                        Status = AccountStatuses.SickDay,
                        PhotoId = Guid.NewGuid(),
                    }
                },
                4,
                2,
                53
                );
    }
}
