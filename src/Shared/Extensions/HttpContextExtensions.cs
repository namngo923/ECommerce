using Microsoft.AspNetCore.Http;

namespace SPSVN.Shared.Extensions;

public static class HttpContextExtensions
{
    public static string GetAccessToken(this HttpContext httpContext)
    {
        return $"{httpContext.Request.Headers.Authorization}".Replace("Bearer", "").Replace("null", "").Trim();
    }

    public static string GetGraphToken(this HttpContext httpContext)
    {
        return $"{httpContext.Request.Headers["Graph"]}".Replace("Bearer", "").Replace("null", "").Trim();
    }
}