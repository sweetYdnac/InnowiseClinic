namespace Shared.Models.Response.Profiles.Doctor
{
    public class GetDoctorsResponse : PagedResponse
    {
        public IEnumerable<DoctorInformationResponse> Doctors { get; init; }

        public GetDoctorsResponse(
            IEnumerable<DoctorInformationResponse> doctors,
            int currentPage,
            int pageSize,
            int totalCount)
            : base(currentPage, pageSize, totalCount) =>
            Doctors = doctors;
    }
}
