using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RxCats.GameApi.Extensions;

public static class HttpContextExtensions
{
    public static string GetClientIp(this HttpContext context)
    {
        return context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
               ?? context.Connection.RemoteIpAddress?.ToString()
               ?? string.Empty;
    }

    public static string GetApiPath(this HttpContext context)
    {
        return context.Request.Path.Value ?? string.Empty;
    }
}