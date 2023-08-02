using System.Net;

namespace Shared.Models.Request.LogsAPI
{
    public class UpdateLogRequest
    {
        public string ApiName { get; set; }
        public string Route { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
