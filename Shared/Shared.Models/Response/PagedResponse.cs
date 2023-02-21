namespace Shared.Models.Response
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public PagedResponse(
            IEnumerable<T> items,
            int currentPage,
            int pageSize,
            int totalCount) =>
        (Items, CurrentPage, PageSize, TotalCount, TotalPages) =
        (items, currentPage, pageSize, totalCount, (int)Math.Ceiling(totalCount / (double)pageSize));
    }
}
