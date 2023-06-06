using System.Net;

namespace Logs.Data.DTOs
{
    public class UpdateLogDTO
    {
        public string ApiName { get; set; }
        public string Route { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
