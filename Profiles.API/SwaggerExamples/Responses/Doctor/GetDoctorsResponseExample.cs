using Shared.Core.Enums;
using Shared.Models.Response.Profiles.Doctor;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Doctor
{
    public class GetDoctorsResponseExample : IExamplesProvider<GetDoctorsResponseModel>
    {
        public GetDoctorsResponseModel GetExamples() =>
            new(
                new[]
                {
                    new DoctorInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Holly Molly Polly",
                        SpecializationName = "Dantist",
                        OfficeAddress = "Homel Barisenko 15 6",
                        Experience = 15,
                        Status = AccountStatuses.AtWork,
                        PhotoId = Guid.NewGuid(),
                    },
                    new DoctorInformationResponse
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
