using Newtonsoft.Json;
using Serilog;
using Services.Business.Interfaces;
using Shared.Core.Extensions;
using Shared.Exceptions;
using Shared.Messages.Helpers;
using Shared.Models.Response;
using System.Net;
using System.Reflection;

namespace Services.API.Middlewares;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public ExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment env) =>
        (_next, _env) = (next, env);

    public async Task InvokeAsync(HttpContext httpContext, IMessageService messageService)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(messageService, httpContext, ex, HttpStatusCode.NotFound, () => Log.Information(ex, ex.Message));
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(messageService, httpContext, ex, HttpStatusCode.InternalServerError, () => Log.Error(ex, ex.Message));
        }
    }

    private async Task HandleExceptionAsync(IMessageService messageService, HttpContext context, Exception ex, HttpStatusCode code, Action logAction)
    {
        await messageService.SendAddLogMessageAsync(LoggerHelpers.GenerateMessage(context, ex, code, Assembly.GetExecutingAssembly().GetName().Name));
        logAction();

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)code;
        var allMessageText = ex.GetFullMessage();

        var details = _env.IsDevelopment() ? ex.StackTrace : string.Empty;

        await response.WriteAsync(JsonConvert.SerializeObject(
            new BaseResponse(
                code,
                allMessageText,
                string.IsNullOrEmpty(details)
                    ? string.Empty
                    : details)
        ));
    }
}