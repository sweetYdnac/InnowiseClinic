using Microsoft.AspNetCore.Http;
using Shared.Core.Extensions;
using System.Net;

namespace Shared.Messages.Helpers
{
    public static class LoggerHelpers
    {
        public static AddLogMessage GenerateMessage(HttpContext context, Exception ex, HttpStatusCode code, string apiName) =>
            new()
            {
                ApiName = apiName,
                Route = context.Request.Path.Value,
                Code = code,
                Message = ex.GetFullMessage(),
                Details = ex.StackTrace,
            };

    }
}
