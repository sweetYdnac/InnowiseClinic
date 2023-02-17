namespace Shared.Models.Response.Profiles.Patient
{
    public class GetPatientsResponse : PagedResponse
    {
        public IEnumerable<PatientInformationResponse> Patients { get; init; }

        public GetPatientsResponse(
            IEnumerable<PatientInformationResponse> patients,
            int currentPage,
            int pageSize,
            int totalCount)
            : base(currentPage, pageSize, totalCount) =>
            Patients = patients;
    }
}
