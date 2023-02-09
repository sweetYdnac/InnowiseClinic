namespace Shared.Models.Response.Profiles.Receptionist
{
    public class GetReceptionistsResponseModel
    {
        public IEnumerable<ReceptionistInformationResponse> Receptionists { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public GetReceptionistsResponseModel(IEnumerable<ReceptionistInformationResponse> receptionists, int currentPage, int pageSize, int totalCount)
        {
            Receptionists = receptionists;
            PageNumber = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
