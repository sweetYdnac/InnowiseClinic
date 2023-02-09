using Shared.Models.Response.Profiles.Doctor;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Doctor
{
    public class GetDoctorsResponseExample : IExamplesProvider<GetDoctorsResponseModel>
    {
        public GetDoctorsResponseModel GetExamples() =>
            new GetDoctorsResponseModel(
                new[]
                {
                    new DoctorInformationResponse
                    {
                        FullName = "Holly Molly Polly",
                        SpecializationName = "Dantist",
                        OfficeAddress = "Homel Barisenko 15 6",
                        Experience = 15
                    },
                    new DoctorInformationResponse
                    {
                        FullName = "Jeff Besos Arg",
                        SpecializationName = "Therapist",
                        OfficeAddress = "New York Test 10 31",
                        Experience = 4
                    }
                },
                4,
                25,
                200
                );
    }
}
