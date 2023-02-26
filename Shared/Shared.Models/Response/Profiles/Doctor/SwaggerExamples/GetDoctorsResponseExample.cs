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
                        SpecializationName = "Dantist",
                        OfficeAddress = "Homel Barisenko 15 6",
                        Experience = 15,
                        Status = AccountStatuses.AtWork,
                        PhotoId = Guid.NewGuid(),
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Jeff Besos Arg",
                        SpecializationName = "Therapist",
                        OfficeAddress = "New York Test 10 31",
                        Experience = 4,
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
