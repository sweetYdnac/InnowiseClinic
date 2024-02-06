using System.Net;

namespace Shared.Models.Request.LogsAPI
{
    public class GetLogsRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public DateOnly? Date { get; set; }
        public string ApiName { get; set; }
        public HttpStatusCode? Code { get; set; }
    }
}
