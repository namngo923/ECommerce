using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace Shared.Middleware;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers = new()
    {
        { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
        { typeof(ValidationException), HandleValidationException },
        { typeof(SpServerException), HandleSpServerException }
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (!_exceptionHandlers.TryGetValue(exceptionType, out var handler))
            await HandleServerException(httpContext, exception);
        else
            await handler.Invoke(httpContext, exception);

        return true;
    }


    private static async Task HandleServerException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error - An unhandled error occurred",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = ex.Message
        });
    }

    private static async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1"
        });
    }

    private static async Task HandleSpServerException(HttpContext httpContext, Exception ex)
    {
        var spServerException = (SpServerException)ex;
        var title = "Internal Server Error";
        var status = StatusCodes.Status500InternalServerError;
        var message = ex.Message;
        var type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";

        httpContext.Response.StatusCode = status;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Type = type,
            Detail = message
        });
    }

    private static async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(exception.Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        });
    }

    // private static async Task HandleClientRequestException(HttpContext httpContext, Exception ex)
    // {
    //     var exception = (Microsoft.SharePoint.Client.ClientRequestException)ex;
    //     if (exception.InnerException is WebException webEx && webEx.Response is HttpWebResponse response)
    //     {
    //         if (response.StatusCode == HttpStatusCode.Unauthorized)
    //         {
    //             await HandleUnauthorizedAccessException(httpContext, ex);
    //         }
    //     }
    // }
}