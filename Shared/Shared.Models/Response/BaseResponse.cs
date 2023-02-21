using Newtonsoft.Json;
using System.Net;

namespace Shared.Models.Response
{
    public class BaseResponse
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public BaseResponse(HttpStatusCode code, string message, string details)
        {
            Code = code;
            Message = message;
            Details = details;
        }

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
