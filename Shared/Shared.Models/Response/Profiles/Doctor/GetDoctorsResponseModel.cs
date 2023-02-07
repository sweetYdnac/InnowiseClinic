namespace Shared.Models.Response.Profiles.Doctor
{
    public class GetDoctorsResponseModel
    {
        public IEnumerable<DoctorPreviewResponse> Doctors { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public GetDoctorsResponseModel(IEnumerable<DoctorPreviewResponse> doctors, int currentPage, int pageSize, int totalCount)
        {
            Doctors = doctors;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
