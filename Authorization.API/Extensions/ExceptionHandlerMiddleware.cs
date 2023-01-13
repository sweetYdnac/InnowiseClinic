﻿using Newtonsoft.Json;
using Serilog;
using Shared.Exceptions;
using Shared.Exceptions.Authorization;
using Shared.Exceptions.Shared;
using System.Net;

namespace Authorization.API.Extensions;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public ExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (EmptyRequestException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest, () => Log.Information(ex, ex.Message));
        }
        catch (AccountNotCreatedException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest, () => Log.Information(ex, ex.Message));
        }
        catch (AccountNotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound, () => Log.Information(ex, ex.Message));
        }
        catch (InvalidPasswordException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest, () => Log.Information(ex, ex.Message));
        }
        catch (RoleIsNotExistException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound, () => Log.Information(ex, ex.Message));
        }
        catch (NotAddedToRoleException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, () => Log.Error(ex, ex.Message));
        }
        catch (NotRemovedFromRoleException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, () => Log.Error(ex, ex.Message));
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, () => Log.Error(ex, ex.Message));
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code, Action logAction)
    {
        logAction();

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)code;
        var allMessageText = GetFullMessage(ex);

        var details = _env.IsDevelopment() ? ex.StackTrace : string.Empty;

        await response.WriteAsync(JsonConvert.SerializeObject(
            new BaseResponseModel(
                code,
                allMessageText,
                string.IsNullOrEmpty(details)
                    ? string.Empty
                    : details)
        ));
    }

    private string GetFullMessage(Exception ex)
    {
        return ex.InnerException is not null
            ? ex.Message + "; " + GetFullMessage(ex.InnerException)
            : ex.Message;
    }
}