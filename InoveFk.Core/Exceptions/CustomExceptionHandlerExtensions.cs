using Microsoft.AspNetCore.Builder;

namespace InoveFk.Core.Exceptions;

public static class CustomExceptionHandlerExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseMiddleware<CustomExceptionHandler>();
    }
}
