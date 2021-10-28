using RxCats.GameApi.Middleware;

namespace RxCats.GameApi.Extensions;

public static class AppMiddleExtensions
{
    public static IApplicationBuilder UseJsonApiLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JsonLoggingMiddleware>();
    }

    public static IApplicationBuilder UseMessagePackApiLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MessagePackLoggingMiddleware>();
    }
}