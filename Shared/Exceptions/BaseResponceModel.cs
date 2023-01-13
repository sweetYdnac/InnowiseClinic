using System.Net;

namespace Shared.Exceptions
{
    public class BaseResponseModel
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public BaseResponseModel(HttpStatusCode code, string message, string details)
        {
            Code = code;
            Message = message;
            Details = details;
        }
    }
}
