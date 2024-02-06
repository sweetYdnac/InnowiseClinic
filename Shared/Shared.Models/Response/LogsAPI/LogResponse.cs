using MongoDB.Bson;
using System.Net;

namespace Shared.Models.Response.LogsAPI
{
    public class LogResponse
    {
        public ObjectId Id { get; set; }
        public DateTime DateTime { get; set; }
        public string ApiName { get; set; }
        public string Route { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
