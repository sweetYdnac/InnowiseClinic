using MongoDB.Bson;
using System.Net;

namespace Logs.Data.DTOs
{
    public class GetLogsDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public DateTime? Date { get; set; }
        public string ApiName { get; set; }
        public HttpStatusCode? Code { get; set; }
    }
}
