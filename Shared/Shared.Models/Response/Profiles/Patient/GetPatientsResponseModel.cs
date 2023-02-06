namespace Shared.Models.Response.Profiles.Patient
{
    public class GetPatientsResponseModel
    {
        public IEnumerable<PatientNameResponse> Patients { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public GetPatientsResponseModel(IEnumerable<PatientNameResponse> patients, int currentPage, int pageSize, int totalCount)
        {
            Patients = patients;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
