using System.Net;

namespace Shared.Messages
{
    public class AddLogMessage
    {
        public string ApiName { get; set; }
        public string Route { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
